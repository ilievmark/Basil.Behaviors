using System.Threading.Tasks;
using Basil.Behaviors.Events.HandlerAbstract;
using Basil.Behaviors.Extensions;
using Xamarin.Forms;

namespace Basil.Behaviors.Events.Handlers.Conditional
{
    public class ConditionalCompositeHandlerAsync : ConditionalCompositeHandlerBase, IAsyncRisible
    {
        #region Properties

        #region WaitResult property
        
        public static readonly BindableProperty WaitResultProperty =
            BindableProperty.Create(
                propertyName: nameof(WaitResult),
                returnType: typeof(bool),
                declaringType: typeof(ConditionalCompositeHandlerAsync),
                defaultValue: default(bool));

        public bool WaitResult
        {
            get => (bool)GetValue(WaitResultProperty);
            set => SetValue(WaitResultProperty, value);
        }
        
        #endregion

        #endregion
        
        public async Task RiseAsync(object sender, object eventArgs)
        {
            if (Condition)
            {
                if (WaitResult)
                    await Handler.RiseAsAsync(sender, eventArgs);
                else
                    Handler.RiseAsAsync(sender, eventArgs);
            }
        }
    }
    
    public class ConditionalCompositeHandlerAsync<T> : ConditionalCompositeHandlerBase, IAsyncGenericRisible
    {
        #region Properties

        #region WaitResult property
        
        public static readonly BindableProperty WaitResultProperty =
            BindableProperty.Create(
                propertyName: nameof(WaitResult),
                returnType: typeof(bool),
                declaringType: typeof(ConditionalCompositeHandlerAsync<T>),
                defaultValue: default(bool));

        public bool WaitResult
        {
            get => (bool)GetValue(WaitResultProperty);
            set => SetValue(WaitResultProperty, value);
        }
        
        #endregion

        #endregion
        
        public async Task<object> RiseAsync(object sender, object eventArgs)
        {
            object result = null;
            
            if (Condition)
            {
                if (WaitResult)
                    result = await Handler.RiseAsAsyncGeneric(sender, eventArgs);
                else
                    result = Handler.RiseAsAsync(sender, eventArgs);
            }

            return result;
        }
    }
}