using System;

namespace Basil.Behaviors.Events.Parameters.Abstract
{
    public interface IParameter
    {
        object GetValue();
        Type GetParamType();
    }
}