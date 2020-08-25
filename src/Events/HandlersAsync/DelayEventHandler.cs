using System.Threading.Tasks;
using Basil.Behaviors.Events.HandlerAbstract;
using Basil.Behaviors.Events.HandlerBase;
using Xamarin.Forms;

namespace Basil.Behaviors.Events.HandlersAsync
{
    public class DelayEventHandler : BaseAsyncHandler, ISkipReturnable
    {
        #region Properties
        
        #region Delay property
        
        public static readonly BindableProperty DelayMillisecondsProperty =
            BindableProperty.Create(
                propertyName: nameof(DelayMilliseconds),
                returnType: typeof(int),
                declaringType: typeof(DelayEventHandler),
                defaultValue: default(int));

        public int DelayMilliseconds
        {
            get => (int)GetValue(DelayMillisecondsProperty);
            set => SetValue(DelayMillisecondsProperty, value);
        }
        
        #endregion

        #endregion

        public override async void Rise(object sender, object eventArgs) => await Task.Delay(DelayMilliseconds);

        public override Task RiseAsync(object sender, object eventArgs) => Task.Delay(DelayMilliseconds);
    }
}