using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Basil.Behaviors.Events.HandlerAbstract;
using Basil.Behaviors.Events.HandlerBase;
using Basil.Behaviors.Events.Parameters.Abstract;
using Xamarin.Forms.Internals;

namespace Basil.Behaviors.Extensions
{
    public static class BaseHandlerExtensions
    {
        #region Multiple rising handlers

        public static List<Task> RunParallel(
            this IList<BaseHandler> handlers,
            object sender,
            object eventArgs)
        {
            var list = new List<Task>();
            
            for (int i = 0; i < handlers.Count; i++)
            {
                var handler = handlers[i];

                if (handler.IsAsyncGenericRisible())
                    list.Add(Task.Run(async () => await handler.RiseAsAsyncGeneric<dynamic>(sender, eventArgs)));
                
                if (handler.IsAsyncRisible())
                    list.Add(Task.Run(async () => await handler.RiseAsAsync(sender, eventArgs)));

                if (handler.IsGenericRisible())
                    list.Add(Task.Run(() => handler.RiseAsGeneric<dynamic>(sender, eventArgs)));
                
                if (handler.IsRisible())
                    list.Add(Task.Run(() => handler.RiseByDefault(sender, eventArgs)));
            }

            return list;
        }
        
        public static async Task RunSequentiallyAsync(
            this IList<BaseHandler> handlers,
            object sender,
            object eventArgs)
        {
            object previousResult;

            for (int i = 0; i < handlers.Count; i++)
            {
                var handler = handlers[i];
                previousResult = default;

                if (handler.Cast<IAsyncGenericRisible>(out var castedAsyncGenericHandler))
                {
                    if (castedAsyncGenericHandler.WaitResult)
                    {
                        var t = castedAsyncGenericHandler.RiseAsync(sender, eventArgs);
                        await t;
                        previousResult = t.GetPropertyValue(nameof(Task<object>.Result));
                    }
                    else
                        previousResult = castedAsyncGenericHandler.RiseAsync(sender, eventArgs);
                }
                
                if (handler.Cast<IAsyncRisible>(out var castedAsyncHandler))
                {
                    if (castedAsyncHandler.WaitResult)
                        await castedAsyncHandler.RiseAsync(sender, eventArgs);
                    else
                        previousResult = castedAsyncHandler.RiseAsync(sender, eventArgs);
                }

                if (handler.IsGenericRisible())
                    previousResult = handler.RiseAsGeneric<dynamic>(sender, eventArgs);
                
                if (handler.IsRisible())
                    handler.RiseByDefault(sender, eventArgs);

                if (previousResult != default)
                    handlers.GetNextParametrizedHandler(i)?.SetReturnValueToNext(previousResult);
            }
        }
        
        internal static BaseHandler GetNextParametrizedHandler(
            this IList<BaseHandler> handlers,
            int index)
        {
            BaseHandler handler;
            do handler = ++index >= handlers.Count ? null : handlers[index];
            while (handler.IsSkipReturnable() &&
                  !handler.IsParametrised() &&
                  !handler.IsAnyComposite() &&
                   index < handlers.Count);
            return handler;
        }
        
        #endregion
        
        #region Rising handlers

        public static void RiseByDefault(
            this BaseHandler handler,
            object sender,
            object eventArgs)
        {
            if (handler.Cast<IRisible>(out var castedRisible))
                castedRisible.Rise(sender, eventArgs);
        }
        
        public static T RiseAsGeneric<T>(
            this BaseHandler handler,
            object sender,
            object eventArgs)
        {
            T returnValue = default;

            if (handler.Cast<IGenericRisible>(out var castedGenericHandler))
                returnValue = castedGenericHandler.Rise<T>(sender, eventArgs);

            return returnValue;
        }

        public static Task RiseAsAsync(
            this BaseHandler handler,
            object sender,
            object eventArgs)
        {
            Task task = Task.CompletedTask;

            if (handler.Cast<IAsyncRisible>(out var castedAsyncHandler))
                task = castedAsyncHandler.RiseAsync(sender, eventArgs);

            return task;
        }
        
        public static Task<T> RiseAsAsyncGeneric<T>(
            this BaseHandler handler,
            object sender,
            object eventArgs)
        {
            Task<T> task = Task<T>.FromResult(default(T));
            
            if (handler.Cast<IAsyncGenericRisible>(out var castedAsyncGenericHandler))
            {
                var tsc = new TaskCompletionSource<T>();
                var innerTask = castedAsyncGenericHandler.RiseAsync(sender, eventArgs);
                innerTask.ContinueWith(t =>
                {
                    tsc.SetResult((T)innerTask.GetPropertyValue(nameof(Task<T>.Result)));
                }, TaskContinuationOptions.OnlyOnRanToCompletion);
                task = tsc.Task;
            }

            return task;
        }
        
        #endregion
        
        #region Return parameter managing
        
        public static void SetReturnValueToNext(this BaseHandler handler, object returnValue)
        {
            handler.SetReturnValueIfParametrizedHandler(returnValue);
            handler.SetReturnValueIfCompositeHandler(returnValue);
            handler.SetReturnValueIfCompositeParallelHandler(returnValue);
        }

        public static void SetReturnValueIfCompositeParallelHandler(this BaseHandler handler, object returnValue)
        {
            if (handler.Cast<ICompositeParallelHandler>(out var castedHandler))
                foreach (var innerHandler in castedHandler.GetInnerHandlers())
                    innerHandler.SetReturnValueToNext(returnValue);
        }
        
        public static void SetReturnValueIfCompositeHandler(this BaseHandler handler, object returnValue)
        {
            if (handler.Cast<ICompositeHandler>(out var castedHandler))
                castedHandler.GetNextNotSkipReturnable().SetReturnValueToNext(returnValue);
        }

        public static void SetReturnValueIfParametrizedHandler(this BaseHandler handler, object returnValue)
        {
            if (handler.Cast<IParameterContainer>(out var castedHandler))
                castedHandler.SetReturnValue(returnValue);
        }

        #endregion
        
        #region Interface logic
        
        public static void SetReturnValue(this IParameterContainer handler, object returnValue)
        {
            handler?
                .GetParameters()
                .OfType<IReturnable>()
                .ForEach(
                    p => p.SetReturnValue(returnValue));
        }

        public static BaseHandler GetNextNotSkipReturnable(this ICompositeHandler compositeHandler)
        {
            var handlers = compositeHandler.GetInnerHandlers();
            var innerHandler = handlers.FirstOrDefault();

            while (innerHandler.IsSkipReturnable())
                innerHandler = handlers.GetNextFrom(innerHandler);

            return innerHandler;
        }

        #endregion
        
        #region Check type and cast
        
        public static bool IsRisible(this BaseHandler handler)
            => handler is IRisible;

        public static bool IsAsyncRisible(this BaseHandler handler)
            => handler is IAsyncRisible;

        public static bool IsGenericRisible(this BaseHandler handler)
            => handler is IGenericRisible;

        public static bool IsAsyncGenericRisible(this BaseHandler handler)
            => handler is IAsyncGenericRisible;
        
        public static bool IsParametrised(this BaseHandler handler)
            => handler is IParameterContainer;

        public static bool IsComposite(this BaseHandler handler)
            => handler is ICompositeHandler;

        public static bool IsCompositeParallel(this BaseHandler handler)
            => handler is ICompositeParallelHandler;

        public static bool IsAnyComposite(this BaseHandler handler)
            => handler.IsComposite() || handler.IsCompositeParallel();

        public static bool IsSkipReturnable(this BaseHandler handler)
            => handler is ISkipReturnable;
        
        #endregion
    }
}