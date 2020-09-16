using System;
using System.Linq;
using System.Threading.Tasks;
using Basil.Behaviors.Events.HandlerAbstract;
using Basil.Behaviors.Events.HandlerBase;
using Basil.Behaviors.Extensions;
using Xamarin.Forms;

namespace Basil.Behaviors.Events.Handlers
{
    public class SequenceHandlerExecutor : BaseCollectionHandler, ICompositeHandler, IAsyncRisible
    {
        #region Properties

        #region WaitResult property

        public static readonly BindableProperty WaitResultProperty =
            BindableProperty.Create(
                propertyName: nameof(WaitResult),
                returnType: typeof(bool),
                declaringType: typeof(SequenceHandlerExecutor),
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
            try
            {
                if (Handlers != null && Handlers.Any())
                {
                    if (WaitResult)
                        await Handlers.RunSequentiallyAsync(sender, eventArgs);
                    else
                        Task.Run(async () => await Handlers.RunSequentiallyAsync(sender, eventArgs));
                }
            }
            catch (OperationCanceledException e)
            {
            }
        }
    }
}