using System.Threading.Tasks;
using Basil.Behaviors.Events.HandlerAbstract;
using Basil.Behaviors.Events.HandlerBase;
using Xamarin.Forms;

namespace Basil.Behaviors.Events.HandlersAsync
{
    [ContentProperty(nameof(Handler))]
    public class DelayedCompositEventHandler : DelayEventHandler
    {
        #region Properties
        
        #region Handler property
        
        public static readonly BindableProperty HandlerProperty =
            BindableProperty.Create(
                propertyName: nameof(Handler),
                returnType: typeof(BaseHandler),
                declaringType: typeof(DelayedCompositEventHandler));

        public BaseHandler Handler
        {
            get => (BaseHandler)GetValue(HandlerProperty);
            set => SetValue(HandlerProperty, value);
        }
        
        #endregion

        #endregion
        
        #region Overrides

        public override void Rise(object sender, object eventArgs)
        {
            base.Rise(sender, eventArgs);
            HandleRising(sender, eventArgs);
        }

        public override async Task RiseAsync(object sender, object eventArgs)
        {
            await base.RiseAsync(sender, eventArgs);
            HandleRisingAsync(sender, eventArgs);
        }

        protected virtual void HandleRising(object sender, object eventArgs)
        {
            Handler.Rise(sender, eventArgs);
        }
        
        protected virtual async Task HandleRisingAsync(object sender, object eventArgs)
        {
            if (Handler is IAsyncRisible castedHandler)
            {
                if (castedHandler.WaitResult)
                    await castedHandler.RiseAsync(sender, eventArgs);
                else
                    castedHandler.RiseAsync(sender, eventArgs);
            }
        }
        
        #endregion
    }
}