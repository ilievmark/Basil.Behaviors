using Basil.Behaviors.Events.HandlerAbstract;
using Basil.Behaviors.Extensions.Internal;

namespace Basil.Behaviors.Events.Handlers.Conditional
{
    public class ConditionalCompositeHandler : ConditionalCompositeHandlerBase, IRisible
    {
        public void Rise(object sender, object eventArgs)
        {
            if (Condition)
                Handler.RiseByDefault(sender, eventArgs);
        }
    }
    
    public class ConditionalCompositeHandler<T> : ConditionalCompositeHandlerBase, IGenericRisible
    {
        public T Rise<T>(object sender, object eventArgs)
        {
            if (Condition)
                return (T) Handler.RiseAsGeneric(sender, eventArgs);
            return default;
        }
    }
}