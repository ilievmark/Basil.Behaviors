using System.Threading.Tasks;

namespace Basil.Behaviors.Events.Handlers
{
    public class ParallelHandlerExecutor : CollectionHandler
    {
        public override void Rise(object sender, object eventArgs)
        {
            foreach (var handler in Handlers)
                Task.Run(() => handler.Rise(sender, eventArgs));
        }
    }
}