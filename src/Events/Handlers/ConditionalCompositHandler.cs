using System.Collections.Generic;
using Basil.Behaviors.Events.HandlerAbstract;
using Basil.Behaviors.Events.HandlerBase;
using Xamarin.Forms;

namespace Basil.Behaviors.Events.Handlers
{
    [ContentProperty(nameof(Handler))]
    public class ConditionalCompositHandler : BaseHandler, ICompositeHandler
    {
        #region Properties
        
        #region Handler property
        
        public static readonly BindableProperty HandlerProperty =
            BindableProperty.Create(
                propertyName: nameof(Handler),
                returnType: typeof(BaseHandler),
                declaringType: typeof(ConditionalCompositHandler));

        public BaseHandler Handler
        {
            get => (BaseHandler)GetValue(HandlerProperty);
            set => SetValue(HandlerProperty, value);
        }
        
        #endregion
        
        #region Condition property
        
        public static readonly BindableProperty ConditionProperty =
            BindableProperty.Create(
                propertyName: nameof(Condition),
                returnType: typeof(bool),
                declaringType: typeof(ConditionalCompositHandler));

        public bool Condition
        {
            get => (bool)GetValue(ConditionProperty);
            set => SetValue(ConditionProperty, value);
        }
        
        #endregion
        
        #endregion

        #region Overrides
        
        public override void Rise(object sender, object eventArgs)
        {
            if (Condition)
                Handler.Rise(sender, eventArgs);
        }
        
        protected override void OnAttachedTo(BindableObject bindable)
        {
            base.OnAttachedTo(bindable);
            
            Handler?.AttachToBindableObject(bindable);
        }

        protected override void OnDetachingFrom(BindableObject bindable)
        {
            base.OnDetachingFrom(bindable);
            
            Handler?.DetachFromBindableObject(bindable);
        }

        #endregion

        public IList<BaseHandler> GetInnerHandlers()
            => new List<BaseHandler> { Handler };
    }
}