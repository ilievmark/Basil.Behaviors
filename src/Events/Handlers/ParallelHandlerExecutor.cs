using System.Threading.Tasks;
using Basil.Behaviors.Events.HandlerAbstract;
using Basil.Behaviors.Events.HandlerBase;

namespace Basil.Behaviors.Events.Handlers
{
    public class ParallelHandlerExecutor : BaseCollectionHandler, ICompositeParallelHandler
    {
        public override void Rise(object sender, object eventArgs)
        {
            foreach (var handler in Handlers)
                Task.Run(() => handler.Rise(sender, eventArgs));
        }
    }
}