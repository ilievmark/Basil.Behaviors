using System.Collections.Generic;
using System.Linq;
using Basil.Behaviors.Events.HandlerAbstract;
using Basil.Behaviors.Events.HandlerBase;
using Basil.Behaviors.Events.Parameters;
using Xamarin.Forms.Internals;

namespace Basil.Behaviors.Extensions.Internal
{
    public static class BaseHandlerExtensions
    {
        internal static void SetReturnParameter(this BaseHandler handler, object returnValue)
        {
            handler.SetReturnParameterToParametrizedHandler(returnValue);
            handler.SetReturnParameterToCompositeHandler(returnValue);
            handler.SetReturnParameterToCompositeParallelHandler(returnValue);
        }

        private static void SetReturnParameterToCompositeParallelHandler(this BaseHandler handler, object returnValue)
        {
            if (handler is ICompositeParallelHandler castedHandler)
                foreach (var innerHandler in castedHandler.GetInnerHandlers())
                    innerHandler.SetReturnParameter(returnValue);
        }
        
        private static void SetReturnParameterToCompositeHandler(this BaseHandler handler, object returnValue)
        {
            if (handler is ICompositeHandler castedHandler)
            {
                var innerHandler = castedHandler.GetInnerHandlers().FirstOrDefault();

                while (innerHandler.IsSkipReturnable())
                {
                    innerHandler = castedHandler.GetInnerHandlers().GetNextElement(innerHandler);
                }
                
                innerHandler.SetReturnParameterToParametrizedHandler(returnValue);
                innerHandler.SetReturnParameterToCompositeHandler(returnValue);
                innerHandler.SetReturnParameterToCompositeParallelHandler(returnValue);
            }
        }

        private static void SetReturnParameterToParametrizedHandler(this BaseHandler handler, object returnValue)
        {
            if (handler is IParametrisedHandler castedHandler)
                castedHandler.SetReturnParameterToParametrizedHandler(returnValue);
        }

        private static void SetReturnParameterToParametrizedHandler(this IParametrisedHandler handler, object returnValue)
        {
            handler?
                .GetParameters()
                .OfType<ReturnParameter>()
                .ForEach(
                    p => p.SetValue(returnValue));
        }
        
        private static T GetNextElement<T>(this IList<T> collection, T current)
        {
            var index = collection.IndexOf(current);
            if (index + 1 < collection.Count)
                return collection[index + 1];
            return default;
        }
        
        public static bool IsParametrised(this BaseHandler handler)
            => handler is IParametrisedHandler;

        public static bool IsComposite(this BaseHandler handler)
            => handler is ICompositeHandler;

        public static bool IsCompositeParallel(this BaseHandler handler)
            => handler is ICompositeParallelHandler;

        public static bool IsSkipReturnable(this BaseHandler handler)
            => handler is ISkipReturnable;
    }
}