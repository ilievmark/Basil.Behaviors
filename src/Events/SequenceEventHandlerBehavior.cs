using System.Collections.Generic;
using System.Linq;
using Basil.Behaviors.Events.Handlers;

namespace Basil.Behaviors.Events
{
    public class SequenceEventHandlerBehavior : EventHandlerBehaviorBase
    {
        #region Overrides

        protected override void HandleEvent(object sender, object eventArgs)
        {
            if (Handlers != null &&
                Handlers.Any())
            {
                HandleEventHandlers(Handlers);
            }
        }

        #endregion

        private void HandleEventHandlers(IEnumerable<BaseHandler> handlers)
        {
            foreach (var handler in handlers)
            {
                handler.Rise(this, new object());
            }
        }
    }
}
