using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using Basil.Behaviors.Events.HandlerAbstract;
using Basil.Behaviors.Events.Parameters;
using Xamarin.Forms;

namespace Basil.Behaviors.Events.HandlerBase
{
    [ContentProperty(nameof(Parameters))]
    public abstract class BaseEventToMethodHandler : BaseHandler, IParameterContainer, IMethodExecutable
    {
        public BaseEventToMethodHandler()
        {
            _parameters = new ObservableCollection<Parameter>();
            //TODO: Remove subscribing or add unsubscribe
            _parameters.CollectionChanged += OnParametersChanged;
        }

        #region Properties
        
        #region MethodName property
        
        public static readonly BindableProperty MethodNameProperty =
            BindableProperty.Create(
                propertyName: nameof(MethodName),
                returnType: typeof(string),
                declaringType: typeof(BaseEventToMethodHandler),
                defaultValue: string.Empty);

        public string MethodName
        {
            get => (string)GetValue(MethodNameProperty);
            set => SetValue(MethodNameProperty, value);
        }
        
        #endregion
        
        #region TargetObject property

        public static readonly BindableProperty TargetMethodCallObjectProperty =
            BindableProperty.Create(
                propertyName: nameof(TargetMethodCallObject),
                returnType: typeof(object),
                declaringType: typeof(BaseEventToMethodHandler),
                defaultValue: default);

        public object TargetMethodCallObject
        {
            get => GetValue(TargetMethodCallObjectProperty);
            set => SetValue(TargetMethodCallObjectProperty, value);
        }

        #endregion
        
        #region Parameters property
        
        private ObservableCollection<Parameter> _parameters;
        public IList<Parameter> Parameters => _parameters;
        
        #endregion

        #endregion
        
        #region Overrides

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();

            ResetParams(_parameters);
            AddedParams(_parameters);
        }

        public object GetTargetMethodRiseObject()
            => TargetMethodCallObject ?? AssociatedObject?.BindingContext;

        #endregion
        
        private void OnParametersChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    AddedParams(e.NewItems.Cast<Parameter>());
                    break;
                case NotifyCollectionChangedAction.Remove:
                    RemovedParams(e.NewItems.Cast<Parameter>());
                    break;
                case NotifyCollectionChangedAction.Reset:
                    ResetParams(e.NewItems.Cast<Parameter>());
                    break;
            }
        }

        private void AddedParams(IEnumerable<Parameter> parameters)
        {
            foreach (var parameter in parameters)
                if (AssociatedObject != null)
                    parameter.AttachBindableObject(AssociatedObject);
        }

        private void RemovedParams(IEnumerable<Parameter> parameters)
        {
            foreach (var parameter in parameters)
                if (AssociatedObject != null)
                    parameter.DetachBindableObject(AssociatedObject);
        }

        private void ResetParams(IEnumerable<Parameter> parameters)
        {
            RemovedParams(parameters);
        }

        public IEnumerable<Parameter> GetParameters()
            => Parameters;
    }
}