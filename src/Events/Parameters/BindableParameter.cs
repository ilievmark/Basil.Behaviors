using System;
using Xamarin.Forms;

namespace Basil.Behaviors.Events.Parameters
{
    public class BindableParameter<T> : Parameter
    {
        public static readonly BindableProperty ValueProperty =
            BindableProperty.Create(
                propertyName: nameof(Value),
                returnType: typeof(T),
                declaringType: typeof(BindableParameter<T>),
                defaultValue: default(T));

        public T Value
        {
            get => (T)GetValue(ValueProperty);
            set => SetValue(ValueProperty, value);
        }
        
        public override Type GetParamType() => typeof(T);
        public override object GetValue() => Value;
    }
}