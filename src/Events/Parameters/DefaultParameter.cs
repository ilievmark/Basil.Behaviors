using Type = System.Type;

namespace Basil.Behaviors.Events.Parameters
{
    public class DefaultParameter<T> : Parameter
    {
        public override Type GetParamType() => typeof(T);
        public override object GetValue() => default(T);
    }
}