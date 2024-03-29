using Basil.Behaviors.Events.HandlerAbstract;
using Basil.Behaviors.Events.HandlerBase;
using Basil.Behaviors.Extensions;

namespace Basil.Behaviors.Events.Handlers
{
    public class EventToMethodHandler : BaseEventToMethodHandler, IRisible
    {
        public void Rise(object sender, object eventArgs) => this.ExecuteMethod();
    }

    public class EventToMethodHandler<T> : BaseEventToMethodHandler, IGenericRisible
    {
        public T Rise<T>(object sender, object eventArgs) => (T) this.ExecuteMethod();
    }
}