using System.Collections.Generic;
using System.Linq;
using Basil.Behaviors.Events.HandlerAbstract;
using Basil.Behaviors.Events.Handlers;
using Basil.Behaviors.Events.Parameters;
using Xamarin.Forms.Internals;

namespace Basil.Behaviors.Events
{
    public class EventMultipleHandlerBehavior : EventHandlerBehaviorBase
    {
        #region Overrides

        protected override void HandleEvent(object sender, object eventArgs)
        {
            if (Handlers != null && Handlers.Any())
                HandleEventHandlers(Handlers, sender, eventArgs);
        }

        #endregion

        private async void HandleEventHandlers(IEnumerable<BaseHandler> handlers, object sender, object eventArgs)
        {
            object previousResult;

            for (int i = 0; i < Handlers.Count; i++)
            {
                var handler = Handlers[i];
                var nextHandler = (i + 1 >= Handlers.Count) ? null : Handlers[i + 1];
                previousResult = default;
                
                if (handler is IAsyncGenericRisible castedAsyncGenericHandler)
                {
                    if (castedAsyncGenericHandler.WaitResult)
                        previousResult = await castedAsyncGenericHandler.RiseAsync<object>(sender, eventArgs);
                    else
                        previousResult = castedAsyncGenericHandler.RiseAsync<object>(sender, eventArgs);
                }
                else if (handler is IAsyncRisible castedAsyncHandler)
                {
                    if (castedAsyncHandler.WaitResult)
                        await castedAsyncHandler.RiseAsync(sender, eventArgs);
                    else
                        previousResult = castedAsyncHandler.RiseAsync(sender, eventArgs);
                }
                else if (handler is IGenericRisible castedGenericHandler)
                {
                    previousResult = castedGenericHandler.Rise<object>(sender, eventArgs);
                }
                else
                {
                    handler.Rise(sender, eventArgs);
                }

                if (nextHandler is IParametrised castedParametrisedHandler)
                {
                    castedParametrisedHandler
                        .GetParameters()
                        .OfType<ReturnParameter>()
                        .ForEach(p => p.SetValue(previousResult));
                }
            }
        }
    }
}
