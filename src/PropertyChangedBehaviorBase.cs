using System;
using System.ComponentModel;
using System.Reflection;
using Basil.Behaviors.Core;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace Basil.Behaviors
{
    public abstract class PropertyChangedBehaviorBase<TProperty> : BaseBehavior
    {
        private TProperty _lastHandleValue;
        
        #region Properties
        
        #region PropertyName property
        
        public static readonly BindableProperty PropertyNameProperty =
            BindableProperty.Create(
                propertyName: nameof(PropertyName),
                returnType: typeof(string),
                declaringType: typeof(PropertyChangedBehaviorBase<TProperty>),
                defaultValue: string.Empty);

        public string PropertyName
        {
            get => (string)GetValue(PropertyNameProperty);
            set => SetValue(PropertyNameProperty, value);
        }
        
        #endregion
        
        #endregion
        
        #region Overrides
        
        protected override void OnAssociatedObjectChanged(BindableObject oldValue, BindableObject newValue)
        {
            base.OnAssociatedObjectChanged(oldValue, newValue);

            VerifyTargetTypes();
        }

        protected override void OnPropertyChanged(string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);
            
            if (propertyName == PropertyNameProperty.PropertyName)
                VerifyTargetTypes();
        }

        protected override void OnAssociatedObjectPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == PropertyName)
                OnTargetPropertyChanged(sender, e);
        }

        protected virtual void OnTargetPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (IsEmptyCache() || IsOldCahce())
                ProcessProperty(GetValue(), GetValue, SetValue);
            _lastHandleValue = GetValue();
        }

        private bool IsEmptyCache()
            => _lastHandleValue == null;
        
        private bool IsOldCahce()
            => _lastHandleValue != null && !_lastHandleValue.Equals(GetValue());
        
        protected abstract bool ProcessProperty(TProperty newValue, Func<TProperty> getValueDelegate, Func<TProperty, TProperty> setValueDelegate);

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
            return IsAssignable() ? (TProperty)GetPropertyInfo().GetValue(AssociatedObject) : default(TProperty);
        }
        
        private TProperty SetValue(TProperty newValue)
        {
            if (IsAssignable())
            {
                var propertyInfo = GetPropertyInfo();
                _lastHandleValue = newValue;
                propertyInfo.SetValue(AssociatedObject, newValue);
            }
            
            return GetValue();
        }

        private void VerifyTargetTypes()
        {
            if (IsAssignable() && GetPropertyInfo().PropertyType != typeof(TProperty))
                throw new NotSupportedException($"Target property {PropertyName} is not {typeof(TProperty).FullName}");
        }

        protected bool IsAssignable()
            => IsAttached() && IsPropertyNameProvided();

        protected bool IsPropertyNameProvided()
            => !string.IsNullOrEmpty(PropertyName);
    }
}