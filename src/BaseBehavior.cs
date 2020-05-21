using System;
using Xamarin.Forms;

namespace Basil.Behaviors
{
    public abstract class BaseBehavior : Behavior
    {
        private BindableObject _associatedObject;
        protected BindableObject AssociatedObject
        {
            get => _associatedObject;
            set => SetAssociatedObject(value);
        }

        private void SetAssociatedObject(BindableObject newValue)
        {
            var oldValue = _associatedObject;
            _associatedObject = newValue;
            OnAssociatedObjectChanged(oldValue, _associatedObject);
        }

        protected virtual void OnAssociatedObjectChanged(BindableObject oldValue, BindableObject newValue)
        {
        }
        
        #region Overrides

        protected override void OnAttachedTo(BindableObject bindable)
        {
            base.OnAttachedTo(bindable);
            AssociatedObject = bindable;

            if (bindable.BindingContext != null)
                BindingContext = bindable.BindingContext;

            bindable.BindingContextChanged += OnBindingContextChanged;
        }

        protected override void OnDetachingFrom(BindableObject bindable)
        {
            base.OnDetachingFrom(bindable);
            bindable.BindingContextChanged -= OnBindingContextChanged;
            AssociatedObject = null;
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();
            BindingContext = AssociatedObject.BindingContext;
        }

        #endregion

        private void OnBindingContextChanged(object sender, EventArgs e)
        {
            OnBindingContextChanged();
        }
    }
}
