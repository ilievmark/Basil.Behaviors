using Xamarin.Forms;

namespace Basil.Behaviors.Events.Handlers
{
    public abstract class BaseHandler : BindableObject
    {
        public abstract void Handle(BindableObject bindable);
    }
}
