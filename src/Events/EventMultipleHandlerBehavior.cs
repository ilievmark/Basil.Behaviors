using System.Collections.Generic;
using System.Linq;
using Basil.Behaviors.Events.Handlers;

namespace Basil.Behaviors.Events
{
    public class EventMultipleHandlerBehavior : EventHandlerBehaviorBase
    {
        #region Overrides

        protected override void HandleEvent(object sender, object eventArgs)
        {
            if (Handlers != null && Handlers.Any())
                HandleEventHandlers(Handlers, sender, eventArgs);
        }

        #endregion

        private void HandleEventHandlers(IEnumerable<BaseHandler> handlers, object sender, object eventArgs)
        {
            foreach (var handler in handlers)
                handler.Rise(sender, eventArgs);
        }
    }
}
