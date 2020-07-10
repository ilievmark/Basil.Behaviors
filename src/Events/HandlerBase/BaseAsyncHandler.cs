using System.Threading.Tasks;
using Basil.Behaviors.Events.HandlerAbstract;
using Xamarin.Forms;

namespace Basil.Behaviors.Events.HandlerBase
{
    public abstract class BaseAsyncHandler : BaseHandler, IAsyncRisible
    {
        #region Properties

        #region MethodName property
        
        public static readonly BindableProperty WaitResultProperty =
            BindableProperty.Create(
                propertyName: nameof(WaitResult),
                returnType: typeof(bool),
                declaringType: typeof(BaseAsyncHandler),
                defaultValue: default(bool));

        public bool WaitResult
        {
            get => (bool)GetValue(WaitResultProperty);
            set => SetValue(WaitResultProperty, value);
        }
        
        #endregion

        #endregion
        
        #region Abstract

        public abstract Task RiseAsync(object sender, object eventArgs);

        #endregion
    }
}