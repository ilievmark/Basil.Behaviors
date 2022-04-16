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
        
        public Task RiseAsync(object sender, object eventArgs)
        {
            var task = Task.CompletedTask;
            
            if (Condition)
            {
                task = Handler.RiseAsAsync(sender, eventArgs);
                
                if (WaitResult)
                {
                    var tcs = new TaskCompletionSource<bool>();
                    task.ContinueWith(t =>
                    {
                        tcs.SetResult(true);
                    }, TaskContinuationOptions.OnlyOnRanToCompletion);
                    task = tcs.Task;
                }
            }

            return task;
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
        
        public Task RiseAsync(object sender, object eventArgs)
        {
            Task<T> task = Task<T>.FromResult(default(T));

            if (Condition)
            {
                task = Handler.RiseAsAsyncGeneric<T>(sender, eventArgs);
                
                if (WaitResult)
                {
                    var tcs = new TaskCompletionSource<T>();
                    task.ContinueWith(t =>
                    {
                        tcs.SetResult(t.Result);
                    }, TaskContinuationOptions.OnlyOnRanToCompletion);
                    task = tcs.Task;
                }
            }

            return task;
        }
    }
}