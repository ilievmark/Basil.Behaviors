using Basil.Behaviors.Events.HandlerAbstract;
using Xamarin.Forms;

namespace Basil.Behaviors.Events.HandlerBase
{
    public abstract class BaseHandler : BaseBehavior, IRisible
    {
        public abstract void Rise(object sender, object eventArgs);

        public void AttachToBindableObject(BindableObject bindable) => OnAttachedTo(bindable);
        
        public void DetachFromBindableObject(BindableObject bindable) => OnDetachingFrom(bindable);
    }
}
