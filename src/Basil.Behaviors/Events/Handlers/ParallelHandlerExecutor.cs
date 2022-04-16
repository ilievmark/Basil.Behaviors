using System.Linq;
using System.Threading.Tasks;
using Basil.Behaviors.Events.HandlerAbstract;
using Basil.Behaviors.Events.HandlerBase;
using Basil.Behaviors.Extensions;
using Xamarin.Forms;

namespace Basil.Behaviors.Events.Handlers
{
    public class ParallelHandlerExecutor : BaseCollectionHandler, ICompositeParallelHandler, IAsyncRisible
    {
        #region Properties

        #region WaitResult property

        public static readonly BindableProperty WaitResultProperty =
            BindableProperty.Create(
                propertyName: nameof(WaitResult),
                returnType: typeof(bool),
                declaringType: typeof(ParallelHandlerExecutor),
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
            if (Handlers != null && Handlers.Any())
            {
                var taskList = Handlers.RunParallel(sender, eventArgs);
                if (WaitResult)
                    await Task.WhenAll(taskList);
            }
        }
    }
}