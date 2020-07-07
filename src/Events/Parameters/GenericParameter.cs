using System;
using Xamarin.Forms;

namespace Basil.Behaviors.Events.Parameters
{
    [ContentProperty(nameof(Value))]
    public class GenericParameter<T> : Parameter
    {
        public T Value { get; set; }

        public override Type GetParamType() => typeof(T);
        public override object GetValue() => Value;
    }
}