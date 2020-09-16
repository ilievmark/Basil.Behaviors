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
        {
            var task = this.ExecuteAsyncMethod();
            
            if (WaitResult)
            {
                var tcs = new TaskCompletionSource<bool>();
                task.ContinueWith(t =>
                {
                    tcs.SetResult(true);
                });
                task = tcs.Task;
            }

            return task;
        }
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

        public Task RiseAsync(object sender, object eventArgs)
        {
            var task = this.ExecuteAsyncMethod<T>();
            
            if (WaitResult)
            {
                var tcs = new TaskCompletionSource<T>();
                task.ContinueWith(t =>
                {
                    tcs.SetResult(t.Result);
                });
                task = tcs.Task;
            }

            return task;
        }
    }
}