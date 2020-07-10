using System.Threading.Tasks;
using Basil.Behaviors.Events.HandlerAbstract;
using Basil.Behaviors.Events.Handlers;
using Basil.Behaviors.Extensions;
using Xamarin.Forms;

namespace Basil.Behaviors.Events.HandlersAsync
{
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