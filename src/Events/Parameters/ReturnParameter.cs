using System;
using Xamarin.Forms;

namespace Basil.Behaviors.Events.Parameters
{
    [ContentProperty(nameof(Value))]
    public sealed class ReturnParameter : Parameter
    {
        public object Value { get; set; }

        public override Type GetParamType() => Value.GetType();
        public override object GetValue() => Value;
        public void SetValue(object value) => Value = value;
    }
}