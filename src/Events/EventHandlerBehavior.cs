using System.Collections.Generic;
using System.Linq;
using Basil.Behaviors.Events.Handlers;
using Xamarin.Forms;

namespace Basil.Behaviors.Events
{
    [ContentProperty(nameof(Handlers))]
    public class EventHandlerBehavior : EventBehaviorBase
    {
        public EventHandlerBehavior()
        {
        }

        #region Properties

        public static readonly BindableProperty HandlersProperty =
            BindableProperty.Create(
                propertyName: nameof(Handlers),
                returnType: typeof(IEnumerable<BaseHandler>),
                declaringType: typeof(EventBehaviorBase),
                defaultValue: default(IEnumerable<BaseHandler>));

        public IEnumerable<BaseHandler> Handlers
        {
            get => (IEnumerable<BaseHandler>)GetValue(HandlersProperty);
            set => SetValue(HandlersProperty, value);
        }

        #endregion

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
            foreach(var handler in handlers)
            {
                handler.Handle(AssociatedObject);
            }
        }
    }
}
