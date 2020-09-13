using System.Threading.Tasks;
using Basil.Behaviors.Events.HandlerAbstract;
using Basil.Behaviors.Events.HandlerBase;
using Basil.Behaviors.Extensions;
using Xamarin.Forms;

namespace Basil.Behaviors.Events.HandlersAsync
{
    public class EventToAsyncMethodHandler : BaseEventToMethodHandler, IAsyncRisible
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
        
        public Task RiseAsync(object sender, object eventArgs)
            => this.ExecuteAsyncMethod();
    }
    
    public class EventToAsyncMethodHandler<T> : BaseEventToMethodHandler, IAsyncGenericRisible
    {
        #region Properties
        
        #region WaitResult property
        
        public static readonly BindableProperty WaitResultProperty =
            BindableProperty.Create(
                propertyName: nameof(WaitResult),
                returnType: typeof(bool),
                declaringType: typeof(EventToAsyncMethodHandler<T>),
                defaultValue: default(bool));

        public bool WaitResult
        {
            get => (bool)GetValue(WaitResultProperty);
            set => SetValue(WaitResultProperty, value);
        }

        #endregion

        #endregion

        public async Task<object> RiseAsync(object sender, object eventArgs)
            => await this.ExecuteAsyncMethod<T>();
    }
}