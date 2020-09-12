using System;
using Basil.Behaviors.Events.Parameters.Abstract;
using Xamarin.Forms;

namespace Basil.Behaviors.Events.Parameters
{
    [ContentProperty(nameof(Value))]
    public abstract class ReturnParameter : Parameter, IReturnable
    {
        public object Value { get; set; }
        
        public override object GetValue()
            => Value;
        
        public void SetValue(object value)
            => Value = value;
    }
    
    public class ReturnParameter<T> : ReturnParameter
    {
        public override Type GetParamType()
            => typeof(T);
    }
}