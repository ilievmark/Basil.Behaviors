using System;
using System.Reflection;
using Xamarin.Forms;

namespace Basil.Behaviors.Events
{

    public abstract class EventBehaviorBase : BaseBehavior
    {
        private Delegate _eventHandler;

        private static void OnEventNameChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (bindable is EventBehaviorBase behavior)
            {
                var oldEventName = (string)oldValue;
                var newEventName = (string)newValue;

                behavior.Unsubscribe(oldEventName);
                behavior.Subscribe(newEventName);
            }
        }

        #region Properties

        #region EventName property

        public static readonly BindableProperty EventNameProperty =
            BindableProperty.Create(
                propertyName: nameof(EventName),
                returnType: typeof(string),
                declaringType: typeof(EventBehaviorBase),
                defaultValue: string.Empty,
                propertyChanged: OnEventNameChanged);

        public string EventName
        {
            get => (string)GetValue(EventNameProperty);
            set => SetValue(EventNameProperty, value);
        }

        #endregion

        #endregion

        #region Overrides

        protected override void OnAttachedTo(BindableObject bindable)
        {
            base.OnAttachedTo(bindable);

            Subscribe(EventName);
        }

        protected override void OnDetachingFrom(BindableObject bindable)
        {
            Unsubscribe(EventName);

            base.OnDetachingFrom(bindable);
        }

        #endregion

        #region Abstract

        protected abstract void HandleEvent(object sender, object eventArgs);

        #endregion

        private void Subscribe(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return;

            if (AssociatedObject == null)
                return;

            var eventInfo = AssociatedObject.GetType().GetRuntimeEvent(name);
            if (eventInfo == null)
                throw new ArgumentException($"{nameof(EventBehaviorBase)}: Can't register the '{EventName}' event.");

            var methodInfo = typeof(EventBehaviorBase).GetTypeInfo().GetDeclaredMethod(nameof(HandleEvent));
            _eventHandler = methodInfo.CreateDelegate(eventInfo.EventHandlerType, this);
            eventInfo.AddEventHandler(AssociatedObject, _eventHandler);
        }

        private void Unsubscribe(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return;

            if (_eventHandler == null)
                return;
            
            if (AssociatedObject == null)
                return;

            var eventInfo = AssociatedObject.GetType().GetRuntimeEvent(name);
            if (eventInfo == null)
                throw new ArgumentException($"{nameof(EventBehaviorBase)}: Can't de-register the '{EventName}' event.");

            eventInfo.RemoveEventHandler(AssociatedObject, _eventHandler);
            _eventHandler = null;
        }
    }
}
