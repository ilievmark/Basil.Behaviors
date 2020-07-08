using System;
using Xamarin.Forms;

namespace Basil.Behaviors.Events.Parameters
{
    [ContentProperty(nameof(Value))]
    public abstract class ReturnParameter : Parameter
    {
        public object Value { get; set; }
        
        public override object GetValue() => Value;
        public void SetValue(object value) => Value = value;
    }
    
    public sealed class ReturnParameter<T> : ReturnParameter
    {
        public override Type GetParamType() => typeof(T);
    }
}