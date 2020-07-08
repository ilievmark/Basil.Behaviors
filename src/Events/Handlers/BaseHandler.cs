using Basil.Behaviors.Events.HandlerAbstract;
using Xamarin.Forms;

namespace Basil.Behaviors.Events.Handlers
{
    public abstract class BaseHandler : BaseBehavior, IRisible
    {
        public abstract void Rise(object sender, object eventArgs);

        public void AttachBindableObject(BindableObject bindable) => OnAttachedTo(bindable);
        
        public void DetachBindableObject(BindableObject bindable) => OnDetachingFrom(bindable);
    }
}
