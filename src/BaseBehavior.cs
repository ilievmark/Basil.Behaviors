using Xamarin.Forms;

namespace Basil.Behaviors
{
    public abstract class BaseBehavior : Behavior
    {
        protected BindableObject AssociatedObject { get; private set; }

        #region Overrides

        protected override void OnAttachedTo(BindableObject bindable)
        {
            base.OnAttachedTo(bindable);

            AssociatedObject = bindable;
        }

        protected override void OnDetachingFrom(BindableObject bindable)
        {
            base.OnDetachingFrom(bindable);

            AssociatedObject = null;
        }

        #endregion
    }
}
