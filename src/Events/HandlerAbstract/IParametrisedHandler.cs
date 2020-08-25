using System.Collections.Generic;
using Basil.Behaviors.Events.Parameters;

namespace Basil.Behaviors.Events.HandlerAbstract
{
    public interface IParametrisedHandler
    {
        IEnumerable<Parameter> GetParameters();
    }
}