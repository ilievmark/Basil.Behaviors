using System;
using Basil.Behaviors.Core;
using Basil.Behaviors.Events.Parameters.Abstract;

namespace Basil.Behaviors.Events.Parameters
{
    public abstract class Parameter : AttachableBindableObject, IParameter
    {
        public abstract object GetValue();
        public abstract Type GetParamType();
    }
}