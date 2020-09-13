using System.Threading.Tasks;

namespace Basil.Behaviors.Events.HandlerAbstract
{
    public interface IAsyncGenericRisible
    {
        bool WaitResult { get; }

        Task<T> RiseAsync<T>(object sender, object eventArgs);
    }
}