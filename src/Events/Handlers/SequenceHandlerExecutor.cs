namespace Basil.Behaviors.Events.Handlers
{
    public class SequenceHandlerExecutor : CollectionHandler
    {
        public override void Rise(object sender, object eventArgs)
        {
            foreach (var handler in Handlers)
                handler.Rise(sender, eventArgs);
        }
    }
}