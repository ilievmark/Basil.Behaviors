using System;
using System.Reflection;
using Basil.Behaviors.Events.HandlerAbstract;
using Basil.Behaviors.Events.HandlerBase;
using Basil.Behaviors.Extensions;
using Xamarin.Forms;

namespace Basil.Behaviors.Events.Handlers
{
    public class EventToSetPropertyHandler<T> : BaseHandler, IRisible
    {
        #region Properties
        
        #region PropertyName property
        
        public static readonly BindableProperty PropertyNameProperty =
            BindableProperty.Create(
                propertyName: nameof(PropertyName),
                returnType: typeof(string),
                declaringType: typeof(EventToSetPropertyHandler<T>),
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
                declaringType: typeof(EventToSetPropertyHandler<T>),
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
                declaringType: typeof(EventToSetPropertyHandler<T>));

        public object TargetExecuteObject
        {
            get => GetValue(TargetExecuteObjectProperty);
            set => SetValue(TargetExecuteObjectProperty, value);
        }

        #endregion

        #endregion
        
        public void Rise(object sender, object eventArgs)
        {
            var propertyInfo = GetPropertyInfo(PropertyName);
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