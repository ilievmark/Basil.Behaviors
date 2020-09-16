using Basil.Behaviors.Events.HandlerAbstract;

namespace Basil.Behaviors.Events.HandlerBase
{
    public abstract class BaseRiseHandler : BaseHandler, IRisible
    {
        public abstract void Rise(object sender, object eventArgs);
    }

    public abstract class BaseRiseHandler<T> : BaseHandler, IGenericRisible
    {
        public abstract T Rise<T>(object sender, object eventArgs);
    }
}