using System;
using Basil.Behaviors.Core;

namespace Basil.Behaviors.Events.Parameters
{
    public abstract class Parameter : AttachableBindableObject
    {
        public abstract object GetValue();
        public abstract Type GetParamType();
    }
}