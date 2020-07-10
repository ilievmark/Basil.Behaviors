using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using Basil.Behaviors.Events.HandlerBase;
using Xamarin.Forms;

namespace Basil.Behaviors.Events
{
    [ContentProperty(nameof(Handlers))]
    public abstract class EventHandlerBehaviorBase : EventBehaviorBase
    {
        public EventHandlerBehaviorBase()
        {
            _handlers = new ObservableCollection<BaseHandler>();
            _handlers.CollectionChanged += OnHandlersCollectionChanged;
        }

        #region Properties

        #region Handlers property

        private ObservableCollection<BaseHandler> _handlers;
        public IList<BaseHandler> Handlers => _handlers;

        #endregion

        #endregion
        
        #region Overrides

        protected override void OnAssociatedObjectChanged(BindableObject oldValue, BindableObject newValue)
        {
            if (oldValue != newValue)
            {
                ResetHandlers(Handlers);
                AddedHandlers(Handlers);
            }
            
            base.OnAssociatedObjectChanged(oldValue, newValue);
        }

        #endregion

        private void OnHandlersCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    AddedHandlers(e.NewItems.Cast<BaseHandler>());
                    break;
                case NotifyCollectionChangedAction.Remove:
                    RemovedHandlers(e.NewItems.Cast<BaseHandler>());
                    break;
                case NotifyCollectionChangedAction.Reset:
                    ResetHandlers(e.NewItems.Cast<BaseHandler>());
                    break;
            }
        }

        private void AddedHandlers(IEnumerable<BaseHandler> handlers)
        {
            foreach (var handler in handlers)
                if (AssociatedObject != null)
                    handler.AttachBindableObject(AssociatedObject);
        }

        private void RemovedHandlers(IEnumerable<BaseHandler> handlers)
        {
            foreach (var handler in handlers)
                if (AssociatedObject != null)
                    handler.DetachBindableObject(AssociatedObject);
        }

        private void ResetHandlers(IEnumerable<BaseHandler> handlers)
        {
            RemovedHandlers(handlers);
        }
    }
}
