using System.Collections.Generic;
using Basil.Behaviors.Events.HandlerBase;

namespace Basil.Behaviors.Events.HandlerAbstract
{
    public interface ICompositeParallelHandler
    {
        IList<BaseHandler> GetInnerHandlers();
    }
}