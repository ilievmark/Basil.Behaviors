using System;
using System.IO;
using System.Reflection;
using Xamarin.Forms;

namespace Basil.Behaviors.Events
{
    public abstract class EventBehaviorBase : BaseBehavior
    {
        private Delegate _eventHandler;
        private EventInfo _eventInfo;

        private static void OnEventNameChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (bindable is EventBehaviorBase behavior)
            {
                var newEventName = (string)newValue;

                behavior.Unsubscribe();
                behavior.Subscribe(newEventName);
            }
        }

        #region Properties

        #region TargetObject property

        public static readonly BindableProperty TargetObjectProperty =
            BindableProperty.Create(
                propertyName: nameof(TargetObject),
                returnType: typeof(object),
                declaringType: typeof(EventBehaviorBase));

        public object TargetObject
        {
            get => GetValue(TargetObjectProperty);
            set => SetValue(TargetObjectProperty, value);
        }

        #endregion
        
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
            Unsubscribe();

            base.OnDetachingFrom(bindable);
        }

        #endregion

        #region Abstract

        protected abstract void HandleEvent(object sender, object eventArgs);

        #endregion

        private void Subscribe(string eventName)
        {
            if (string.IsNullOrWhiteSpace(eventName))
                throw new ArgumentNullException(nameof(EventName));

            var target = TargetObject ?? AssociatedObject;
            if (target == null)
                throw new InvalidDataException("Associated object can not be null");

            _eventInfo = target.GetType().GetRuntimeEvent(eventName);
            if (_eventInfo == null)
                throw new ArgumentException($"{nameof(EventBehaviorBase)}: Can't register the '{EventName}' event.");

            var methodInfo = typeof(EventBehaviorBase).GetTypeInfo().GetDeclaredMethod(nameof(HandleEvent));
            _eventHandler = methodInfo.CreateDelegate(_eventInfo.EventHandlerType, this);
            _eventInfo.AddEventHandler(AssociatedObject, _eventHandler);
        }

        private void Unsubscribe()
        {
            if (_eventHandler == null)
                throw new InvalidOperationException("Cached event handler info is invalid");
            
            if (_eventInfo == null)
                throw new ArgumentException($"{nameof(EventBehaviorBase)}: Can't de-register the '{EventName}' event.");
            
            var target = TargetObject ?? AssociatedObject;
            if (target == null)
                throw new InvalidDataException("Associated object can not be null");

            _eventInfo.RemoveEventHandler(target, _eventHandler);
            _eventInfo = null;
            _eventHandler = null;
        }
    }
}
