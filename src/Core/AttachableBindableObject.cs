using System;
using Xamarin.Forms;

namespace Basil.Behaviors.Core
{
    public class AttachableBindableObject : BindableObject
    {
        protected BindableObject AssociatedObject { get; set; }
        
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