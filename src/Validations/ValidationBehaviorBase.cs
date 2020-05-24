using System;
using System.ComponentModel;
using System.Reflection;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace Basil.Behaviors.Validations
{
    public abstract class ValidationBehaviorBase<TProperty> : BaseBehavior
    {
        #region Properties
        
        #region Pattern property
        
        public static readonly BindableProperty PatternProperty =
            BindableProperty.Create(
                propertyName: nameof(Pattern),
                returnType: typeof(string),
                declaringType: typeof(ValidationBehaviorBase<TProperty>),
                defaultValue: string.Empty);

        public string Pattern
        {
            get => (string)GetValue(PatternProperty);
            set => SetValue(PatternProperty, value);
        }
        
        #endregion
        
        #region PropertyName property
        
        public static readonly BindableProperty PropertyNameProperty =
            BindableProperty.Create(
                propertyName: nameof(PropertyName),
                returnType: typeof(string),
                declaringType: typeof(ValidationBehaviorBase<TProperty>),
                defaultValue: string.Empty);

        public string PropertyName
        {
            get => (string)GetValue(PropertyNameProperty);
            set => SetValue(PropertyNameProperty, value);
        }
        
        #endregion
        
        #endregion

        #region Overrides

        protected override void OnAssociatedObjectPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == PropertyName)
                OnTargetPropertyChanged(sender, e);
        }

        protected virtual void OnTargetPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            Validate(GetValue(), GetValue, SetValue);
        }

        protected abstract void Validate(TProperty newValue, Func<TProperty> getValueDelegate, Func<TProperty, TProperty> setValueDelegate);

        #endregion

        private PropertyInfo GetPropertyInfo()
        {
            var propertyInfo = AssociatedObject.GetType().GetProperty(PropertyName);
            if (propertyInfo == null)
                throw new ArgumentException(
                    $"There is no property with name {PropertyName} in type {AssociatedObject.GetType().FullName}");

            return propertyInfo;
        }
        
        private TProperty GetValue()
        {
            var propertyInfo = GetPropertyInfo();
            
            return (TProperty)propertyInfo.GetValue(AssociatedObject);
        }
        
        private TProperty SetValue(TProperty newValue)
        {
            var propertyInfo = GetPropertyInfo();
            
            propertyInfo.SetValue(AssociatedObject, newValue);
            
            return (TProperty)propertyInfo.GetValue(AssociatedObject);
        }
    }
}