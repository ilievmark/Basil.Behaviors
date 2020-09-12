using Xamarin.Forms;

namespace Basil.Behaviors.Events.HandlerBase
{
    public abstract class BaseHandler : BaseBehavior
    {
        public void AttachToBindableObject(BindableObject bindable) => OnAttachedTo(bindable);
        
        public void DetachFromBindableObject(BindableObject bindable) => OnDetachingFrom(bindable);
    }
}
