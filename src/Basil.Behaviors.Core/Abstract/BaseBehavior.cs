using System;
using System.ComponentModel;
using Xamarin.Forms;

namespace Basil.Behaviors.Core.Abstract
{
    public abstract class BaseBehavior : Behavior
    {
        private BindableObject _associatedObject;
        public BindableObject AssociatedObject
        {
            get => _associatedObject;
            private set => SetAssociatedObject(value);
        }

        #region Overrides

        protected override void OnAttachedTo(BindableObject bindable)
        {
            base.OnAttachedTo(bindable);
            AssociatedObject = bindable;

            if (bindable.BindingContext != null)
            {
                BindingContext = bindable.BindingContext;
            }

            bindable.BindingContextChanged += OnBindingContextChanged;
            bindable.PropertyChanged += OnAssociatedObjectPropertyChanged;
        }

        protected override void OnDetachingFrom(BindableObject bindable)
        {
            base.OnDetachingFrom(bindable);
            bindable.BindingContextChanged -= OnBindingContextChanged;
            bindable.PropertyChanged -= OnAssociatedObjectPropertyChanged;
            AssociatedObject = null;
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();
            BindingContext = AssociatedObject.BindingContext;
        }

        #endregion

        #region Virtual members

        protected virtual void OnAssociatedObjectChanged(BindableObject oldValue, BindableObject newValue)
        {
        }

        protected virtual void OnAssociatedObjectChanging(BindableObject oldValue, BindableObject newValue)
        {
        }

        protected virtual void OnAssociatedObjectPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
        }

        #endregion

        private void OnBindingContextChanged(object sender, EventArgs e)
        {
            OnBindingContextChanged();
        }

        private void SetAssociatedObject(BindableObject newValue)
        {
            var oldValue = _associatedObject;
            OnAssociatedObjectChanging(_associatedObject, newValue);
            _associatedObject = newValue;
            OnAssociatedObjectChanged(oldValue, _associatedObject);
        }

        protected bool IsAttached()
            => AssociatedObject != null;
    }
}
