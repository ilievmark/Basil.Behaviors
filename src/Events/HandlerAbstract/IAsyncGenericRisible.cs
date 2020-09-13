using System.Threading.Tasks;

namespace Basil.Behaviors.Events.HandlerAbstract
{
    public interface IAsyncGenericRisible
    {
        bool WaitResult { get; }

        Task<object> RiseAsync(object sender, object eventArgs);
    }
}