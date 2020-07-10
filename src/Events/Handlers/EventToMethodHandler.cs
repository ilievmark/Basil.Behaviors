using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Threading.Tasks;
using Basil.Behaviors.Events.HandlerAbstract;
using Basil.Behaviors.Events.HandlerBase;
using Basil.Behaviors.Extensions;
using Basil.Behaviors.Events.Parameters;
using Xamarin.Forms;

namespace Basil.Behaviors.Events.Handlers
{
    public class EventToMethodHandler : BaseEventToMethodHandler
    {
        public override void Rise(object sender, object eventArgs) => this.ExecuteMethod();
    }

    public class EventToMethodHandler<T> : EventToMethodHandler, IGenericRisible
    {
        public T Rise<T>(object sender, object eventArgs) => (T) this.ExecuteMethod();
    }
}