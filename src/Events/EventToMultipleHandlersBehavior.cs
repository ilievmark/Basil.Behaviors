using System;
using System.Linq;
using Basil.Behaviors.Extensions.Internal;

namespace Basil.Behaviors.Events
{
    public class EventToMultipleHandlersBehavior : EventHandlersBehaviorBase
    {
        #region Overrides

        protected override async void HandleEvent(object sender, object eventArgs)
        {
            try
            {
                if (Handlers != null && Handlers.Any())
                    await Handlers.RunSequentiallyAsync(sender, eventArgs);
            }
            catch (OperationCanceledException e)
            {
            }
        }

        #endregion
    }
}
