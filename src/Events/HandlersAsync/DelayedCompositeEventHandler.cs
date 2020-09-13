using System.Collections.Generic;
using System.Threading.Tasks;
using Basil.Behaviors.Events.HandlerAbstract;
using Basil.Behaviors.Events.HandlerBase;
using Basil.Behaviors.Extensions;
using Xamarin.Forms;

namespace Basil.Behaviors.Events.HandlersAsync
{
    [ContentProperty(nameof(Handler))]
    public class DelayedCompositeEventHandler : BaseAsyncHandler, ICompositeHandler
    {
        #region Properties
        
        #region Handler property
        
        public static readonly BindableProperty HandlerProperty =
            BindableProperty.Create(
                propertyName: nameof(Handler),
                returnType: typeof(BaseHandler),
                declaringType: typeof(DelayedCompositeEventHandler));

        public BaseHandler Handler
        {
            get => (BaseHandler)GetValue(HandlerProperty);
            set => SetValue(HandlerProperty, value);
        }
        
        #endregion
        
        #region DelayMilliseconds property
        
        public static readonly BindableProperty DelayMillisecondsProperty =
            BindableProperty.Create(
                propertyName: nameof(DelayMilliseconds),
                returnType: typeof(int),
                declaringType: typeof(DelayedCompositeEventHandler),
                defaultValue: default(int));

        public int DelayMilliseconds
        {
            get => (int)GetValue(DelayMillisecondsProperty);
            set => SetValue(DelayMillisecondsProperty, value);
        }
        
        #endregion

        #endregion
        
        #region Overrides

        public override async Task RiseAsync(object sender, object eventArgs)
        {
            await Task.Delay(DelayMilliseconds);
            await Handler.RiseAsAsync(sender, eventArgs);
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