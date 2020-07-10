using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Threading.Tasks;
using Basil.Behaviors.Events.HandlerAbstract;
using Basil.Behaviors.Extensions;
using Basil.Behaviors.Events.Parameters;
using Xamarin.Forms;

namespace Basil.Behaviors.Events.Handlers
{
    [ContentProperty(nameof(Parameters))]
    public abstract class EventToMethodHandlerBase : BaseHandler, IParametrised
    {
        public EventToMethodHandlerBase()
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
                declaringType: typeof(EventToMethodHandlerBase),
                defaultValue: string.Empty);

        public string MethodName
        {
            get => (string)GetValue(MethodNameProperty);
            set => SetValue(MethodNameProperty, value);
        }
        
        #endregion
        
        #region TargetObject property

        public static readonly BindableProperty TargetObjectProperty =
            BindableProperty.Create(
                propertyName: nameof(TargetObject),
                returnType: typeof(object),
                declaringType: typeof(EventToMethodHandlerBase),
                defaultValue: default);

        public object TargetObject
        {
            get => GetValue(TargetObjectProperty);
            set => SetValue(TargetObjectProperty, value);
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

        public IEnumerable<Parameter> GetParameters() => Parameters;
    }

    public class EventToMethodHandler : EventToMethodHandlerBase
    {
        public override void Rise(object sender, object eventArgs) => this.ExecuteMethod();
    }

    public class EventToMethodHandler<T> : EventToMethodHandler, IGenericRisible
    {
        public T Rise<T>(object sender, object eventArgs) => (T) this.ExecuteMethod();
    }
    
    public class EventToAsyncMethodHandler : EventToMethodHandler, IAsyncRisible
    {
        #region Properties
        
        #region WaitResult property
        
        public static readonly BindableProperty WaitResultProperty =
            BindableProperty.Create(
                propertyName: nameof(WaitResult),
                returnType: typeof(bool),
                declaringType: typeof(EventToAsyncMethodHandler),
                defaultValue: default(bool));

        public bool WaitResult
        {
            get => (bool)GetValue(WaitResultProperty);
            set => SetValue(WaitResultProperty, value);
        }
        
        #endregion

        #endregion
        
        public Task RiseAsync(object sender, object eventArgs) => this.ExecuteAsyncMethod();
    }
    
    public class EventToAsyncMethodHandler<T> : EventToAsyncMethodHandler, IAsyncGenericRisible
    {
        public Task<T> RiseAsync<T>(object sender, object eventArgs) => this.ExecuteAsyncMethod<T>();
    }
}