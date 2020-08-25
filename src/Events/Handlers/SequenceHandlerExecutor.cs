using System.Threading.Tasks;
using Basil.Behaviors.Events.HandlerAbstract;
using Basil.Behaviors.Events.HandlerBase;
using Basil.Behaviors.Extensions.Internal;
using Xamarin.Forms.Internals;

namespace Basil.Behaviors.Events.Handlers
{
    public class SequenceHandlerExecutor : BaseCollectionHandler, ICompositeHandler
    {
        public override async void Rise(object sender, object eventArgs)
        {
            object previousResult;

            for (int i = 0; i < Handlers.Count; i++)
            {
                var handler = Handlers[i];
                previousResult = default;
                
                if (handler is IAsyncGenericRisible castedAsyncGenericHandler)
                {
                    if (castedAsyncGenericHandler.WaitResult)
                    {
                        var task = castedAsyncGenericHandler.RiseAsync(sender, eventArgs);
                        await task;
                        previousResult = task.GetType().GetProperty(nameof(Task<object>.Result)).GetValue(task);
                    }
                    else
                        previousResult = castedAsyncGenericHandler.RiseAsync(sender, eventArgs);
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

                if (previousResult != default)
                    GetNextParametrizedHandler(i)?.SetReturnParameter(previousResult);
            }
        }

        private BaseHandler GetNextParametrizedHandler(int index)
        {
            BaseHandler handler;
            
            do
            {
                handler = ++index >= Handlers.Count ? null : Handlers[index];
            } while (handler.IsSkipReturnable());

            return handler;
        }
    }
}