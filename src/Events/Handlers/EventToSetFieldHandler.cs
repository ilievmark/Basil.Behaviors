using System;
using System.Reflection;
using Basil.Behaviors.Events.HandlerAbstract;
using Basil.Behaviors.Events.HandlerBase;
using Basil.Behaviors.Extensions.Internal;
using Xamarin.Forms;

namespace Basil.Behaviors.Events.Handlers
{
    public class EventToSetFieldHandler<T> : BaseHandler, IRisible
    {
        #region Properties
        
        #region FieldName property
        
        public static readonly BindableProperty FieldNameProperty =
            BindableProperty.Create(
                propertyName: nameof(FieldName),
                returnType: typeof(string),
                declaringType: typeof(EventToSetFieldHandler<T>),
                defaultValue: string.Empty);

        public string FieldName
        {
            get => (string)GetValue(FieldNameProperty);
            set => SetValue(FieldNameProperty, value);
        }
        
        #endregion
        
        #region Value property
        
        public static readonly BindableProperty ValueProperty =
            BindableProperty.Create(
                propertyName: nameof(Value),
                returnType: typeof(T),
                declaringType: typeof(EventToSetFieldHandler<T>),
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
                declaringType: typeof(EventToSetFieldHandler<T>));

        public object TargetExecuteObject
        {
            get => GetValue(TargetExecuteObjectProperty);
            set => SetValue(TargetExecuteObjectProperty, value);
        }

        #endregion

        #endregion
        
        public void Rise(object sender, object eventArgs)
        {
            var fieldInfo = GetFieldInfo(FieldName);
            var target = GetTargetExecuteObject();
            
            fieldInfo.SetValue(target, Value);
        }

        private FieldInfo GetFieldInfo(string fieldName)
        {
            if (string.IsNullOrEmpty(fieldName))
                throw new ArgumentNullException(nameof(FieldName));

            var target = GetTargetExecuteObject();
            if (target == null)
                throw new InvalidOperationException("There is no attached or target property object (null)");
            
            var fieldInfo = target.GetFieldInfo(fieldName);
            if (fieldInfo == null)
                throw new ArgumentException($"Field {fieldName} was not found in type {target.GetType().AssemblyQualifiedName}");

            return fieldInfo;
        }
        
        private object GetTargetExecuteObject()
            => TargetExecuteObject ?? AssociatedObject?.BindingContext;
    }
}