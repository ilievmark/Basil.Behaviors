using System;
using Xamarin.Forms;

namespace Basil.Behaviors.Events.Parameters
{
    [ContentProperty(nameof(Value))]
    public class NamedParameter<T> : NamedParameter
    {
        public T Value { get; set; }

        public override Type GetParamType() => typeof(T);
        public override object GetValue() => Value;
    }

    public abstract class NamedParameter : Parameter
    {
        public string Name { get; set; }
    }
}