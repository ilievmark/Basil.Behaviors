using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Basil.Behaviors.Events.HandlerAbstract;
using Basil.Behaviors.Events.HandlerBase;
using Basil.Behaviors.Events.Parameters.Abstract;
using Xamarin.Forms.Internals;

namespace Basil.Behaviors.Extensions.Internal
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
                    list.Add(Task.Run(async () => await handler.RiseAsAsyncGeneric(sender, eventArgs)));
                
                if (handler.IsAsyncRisible())
                    list.Add(Task.Run(async () => await handler.RiseAsAsync(sender, eventArgs)));

                if (handler.IsGenericRisible())
                    list.Add(Task.Run(() => handler.RiseAsGeneric(sender, eventArgs)));
                
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

                if (handler.IsAsyncGenericRisible())
                    previousResult = await handler.RiseAsAsyncGeneric(sender, eventArgs);
                
                if (handler.IsAsyncRisible())
                    previousResult = await handler.RiseAsAsync(sender, eventArgs);

                if (handler.IsGenericRisible())
                    previousResult = handler.RiseAsGeneric(sender, eventArgs);
                
                if (handler.IsRisible())
                    handler.RiseByDefault(sender, eventArgs);

                if (previousResult != default)
                    handlers.GetNextParametrizedHandler(i)?.SetReturnValueToNext(previousResult);
            }
        }
        
        public static BaseHandler GetNextParametrizedHandler(
            this IList<BaseHandler> handlers,
            int index)
        {
            BaseHandler handler;
            do handler = ++index >= handlers.Count ? null : handlers[index];
            while (handler.IsSkipReturnable() || !handler.IsParametrised());
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
        
        public static object RiseAsGeneric(
            this BaseHandler handler,
            object sender,
            object eventArgs)
        {
            object returnValue = default;

            if (handler.Cast<IGenericRisible>(out var castedGenericHandler))
                returnValue = castedGenericHandler.Rise<object>(sender, eventArgs);

            return returnValue;
        }
        
        public static async Task<object> RiseAsAsync(
            this BaseHandler handler,
            object sender,
            object eventArgs)
        {
            object returnValue = default;

            if (handler.Cast<IAsyncRisible>(out var castedAsyncHandler))
            {
                if (castedAsyncHandler.WaitResult)
                    await castedAsyncHandler.RiseAsync(sender, eventArgs);
                else
                    returnValue = castedAsyncHandler.RiseAsync(sender, eventArgs);
            }

            return returnValue;
        }
        
        public static async Task<object> RiseAsAsyncGeneric(
            this BaseHandler handler,
            object sender,
            object eventArgs)
        {
            object returnValue = default;
            
            if (handler.Cast<IAsyncGenericRisible>(out var castedAsyncGenericHandler))
            {
                if (castedAsyncGenericHandler.WaitResult)
                    returnValue = await castedAsyncGenericHandler.RiseAsync(sender, eventArgs);
                else
                    returnValue = castedAsyncGenericHandler.RiseAsync(sender, eventArgs);
            }

            return returnValue;
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
            if (handler.Cast<IParametrisedHandler>(out var castedHandler))
                castedHandler.SetReturnValue(returnValue);
        }

        #endregion
        
        #region Interface logic
        
        public static void SetReturnValue(this IParametrisedHandler handler, object returnValue)
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
            => handler is IParametrisedHandler;

        public static bool IsComposite(this BaseHandler handler)
            => handler is ICompositeHandler;

        public static bool IsCompositeParallel(this BaseHandler handler)
            => handler is ICompositeParallelHandler;

        public static bool IsSkipReturnable(this BaseHandler handler)
            => handler is ISkipReturnable;
        
        #endregion
    }
}