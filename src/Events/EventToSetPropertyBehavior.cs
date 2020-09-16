using System;
using System.Reflection;
using Basil.Behaviors.Extensions;
using Xamarin.Forms;

namespace Basil.Behaviors.Events
{
    public class EventToSetPropertyBehavior<T> : EventBehaviorBase
    {
        #region Properties
        
        #region PropertyName property
        
        public static readonly BindableProperty PropertyNameProperty =
            BindableProperty.Create(
                propertyName: nameof(PropertyName),
                returnType: typeof(string),
                declaringType: typeof(EventToSetPropertyBehavior<T>),
                defaultValue: string.Empty);

        public string PropertyName
        {
            get => (string)GetValue(PropertyNameProperty);
            set => SetValue(PropertyNameProperty, value);
        }
        
        #endregion
        
        #region Value property
        
        public static readonly BindableProperty ValueProperty =
            BindableProperty.Create(
                propertyName: nameof(Value),
                returnType: typeof(T),
                declaringType: typeof(EventToSetPropertyBehavior<T>),
                defaultValue: default(T));

        public T Value
        {
            get => (T)GetValue(ValueProperty);
            set => SetValue(ValueProperty, value);
        }
        
        #endregion
        
        #region TargetExecuteObject property

        public static readonly BindableProperty TargetExecuteObjectProperty =
            BindableProperty.Create(
                propertyName: nameof(TargetExecuteObject),
                returnType: typeof(object),
                declaringType: typeof(EventToSetPropertyBehavior<T>));

        public object TargetExecuteObject
        {
            get => GetValue(TargetExecuteObjectProperty);
            set => SetValue(TargetExecuteObjectProperty, value);
        }

        #endregion

        #endregion
        
        protected override void HandleEvent(object sender, object eventArgs)
        {
            var propertyInfo = GetPropertyInfo(PropertyName);
            propertyInfo.ValidateMember(PropertyName);
            if (!propertyInfo.CanWrite)
                throw new InvalidOperationException("Target property do not have set method or cant be write");
            var target = GetTargetExecuteObject();
            propertyInfo.SetValue(target, Value);
        }

        private PropertyInfo GetPropertyInfo(string propertyName)
        {
            if (string.IsNullOrEmpty(propertyName))
                throw new ArgumentNullException(nameof(PropertyName));

            var target = GetTargetExecuteObject();
            if (target == null)
                throw new InvalidOperationException("There is no attached or target property object (null)");
            
            var propertyInfo = target.GetPropertyInfo(propertyName);
            if (propertyInfo == null)
                throw new ArgumentException($"Property {propertyName} was not found in type {target.GetType().AssemblyQualifiedName}");

            return propertyInfo;
        }
        
        private object GetTargetExecuteObject()
            => TargetExecuteObject ?? AssociatedObject?.BindingContext;
    }
}