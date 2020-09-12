using System;
using Basil.Behaviors.Extensions.Internal;
using Xamarin.Forms;

namespace Basil.Behaviors.Events.Parameters
{
    public class ReflectionParameter<T> : Parameter
    {
        public static readonly BindableProperty TargetProperty =
            BindableProperty.Create(
                propertyName: nameof(Target),
                returnType: typeof(object),
                declaringType: typeof(ReflectionParameter<T>),
                defaultValue: null);

        public object Target
        {
            get => GetValue(TargetProperty);
            set => SetValue(TargetProperty, value);
        }
        
        public static readonly BindableProperty SourceNameProperty =
            BindableProperty.Create(
                propertyName: nameof(SourceName),
                returnType: typeof(string),
                declaringType: typeof(ReflectionParameter<T>),
                defaultValue: string.Empty);

        public string SourceName
        {
            get => (string)GetValue(SourceNameProperty);
            set => SetValue(SourceNameProperty, value);
        }

        public override object GetValue()
            => GetFromProperty() ?? GetFromField();

        public override Type GetParamType()
            => typeof(T);

        private object GetTargetObject()
            => Target ?? BindingContext;

        private object GetFromProperty()
            => GetTargetObject().GetPropertyValue(SourceName);
        
        private object GetFromField()
            => GetTargetObject().GetFieldValue(SourceName);
    }
}