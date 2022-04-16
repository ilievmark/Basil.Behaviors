using System;
using System.Threading.Tasks;

namespace Basil.Behaviors.Events.Parameters
{
    public class GenericTaskReturnParameter<T> : ReturnParameter<T>
    {
        public override Type GetParamType() => typeof(Task<T>);
    }
}