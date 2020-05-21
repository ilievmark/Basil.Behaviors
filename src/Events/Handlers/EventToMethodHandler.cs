using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Reflection;
using Basil.Behaviors.Events.Parameters;
using Xamarin.Forms;

namespace Basil.Behaviors.Events.Handlers
{
    [ContentProperty(nameof(Parameters))]
    public class EventToMethodHandler : BaseHandler
    {
        public EventToMethodHandler()
        {
            _parameters = new ObservableCollection<Parameter>();
            _parameters.CollectionChanged += OnParametersChanged;
        }

        #region Properties
        
        #region MethodName property
        
        public static readonly BindableProperty MethodNameProperty =
            BindableProperty.Create(
                propertyName: nameof(MethodName),
                returnType: typeof(string),
                declaringType: typeof(EventToMethodHandler),
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
                declaringType: typeof(EventToMethodHandler),
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
        
        
        public override void Rise(object sender, object eventArgs)
        {
            if (AssociatedObject == null)
                return;
            
            if (AssociatedObject.BindingContext == null)
                return;
            
            if (string.IsNullOrEmpty(MethodName))
                return;

            var target = TargetObject;
            if (target == null)
                target = AssociatedObject.BindingContext;

            var targetType = target.GetType();
            var parameterTypes = _parameters.Select(i => i.GetParamType()).ToArray();
            var methodInfo = targetType.GetRuntimeMethod(MethodName, parameterTypes);
            if (methodInfo == null)
                throw new ArgumentException($"{nameof(EventToMethodBehavior)}: Cant invoke method {MethodName}." +
                                            $" There is no method {MethodName} in type {targetType.FullName}");

            methodInfo.Invoke(target, _parameters.Select(i => i.GetValue()).ToArray());
        }

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
    }
}