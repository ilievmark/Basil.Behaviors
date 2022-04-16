using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using Xamarin.Forms;

namespace Basil.Behaviors.Core.Abstract
{
    [ContentProperty(nameof(Rules))]
    public abstract class PropertyRulesBehaviorBase<TProperty, TRule> : PropertyChangedBehaviorBase<TProperty> where TRule : BaseRule
    {
        public PropertyRulesBehaviorBase()
        {
            _items = new ObservableCollection<TRule>();
            _items.CollectionChanged += OnItemsCollectionChanged;
        }

        protected ObservableCollection<TRule> _items;
        public IList<TRule> Rules => _items;

        protected virtual void OnItemsCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    AddedItems(e.NewItems.Cast<TRule>());
                    break;
                case NotifyCollectionChangedAction.Remove:
                    RemovedItems(e.NewItems.Cast<TRule>());
                    break;
                case NotifyCollectionChangedAction.Reset:
                    ResetItems(e.NewItems.Cast<TRule>());
                    break;
            }
        }

        protected virtual void AddedItems(IEnumerable<TRule> items)
        {
            foreach (var item in items)
                if (AssociatedObject != null)
                    item.AttachBindableObject(AssociatedObject);

        }

        protected virtual void RemovedItems(IEnumerable<TRule> items)
        {
            foreach (var item in items)
                if (AssociatedObject != null)
                    item.DetachBindableObject(AssociatedObject);
        }

        protected virtual void ResetItems(IEnumerable<TRule> items)
            => RemovedItems(items);

        protected IEnumerable<string> GetRules()
            => _items.Select(r => r.Rule);
    }
}