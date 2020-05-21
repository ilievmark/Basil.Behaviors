using System;
using Xamarin.Forms;

namespace Basil.Behaviors.Events.Parameters
{
    public abstract class Parameter : BindableObject
    {
        protected BindableObject AssociatedObject { get; set; }
        
        public abstract object GetValue();
        public abstract Type GetParamType();
        
        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();
            BindingContext = AssociatedObject.BindingContext;
        }
        
        public void AttachBindableObject(BindableObject bindable)
        {
            AssociatedObject = bindable;

            if (bindable.BindingContext != null)
            {
                BindingContext = bindable.BindingContext;
            }

            bindable.BindingContextChanged += OnBindingContextChanged;
        }

        public void DetachBindableObject(BindableObject bindable)
        {
            bindable.BindingContextChanged -= OnBindingContextChanged;
            AssociatedObject = null;
        }
        
        private void OnBindingContextChanged(object sender, EventArgs e)
        {
            OnBindingContextChanged();
        }
    }
}