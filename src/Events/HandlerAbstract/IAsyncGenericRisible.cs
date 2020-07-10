using System.Threading.Tasks;

namespace Basil.Behaviors.Events.HandlerAbstract
{
    public interface IAsyncGenericRisible : IAsyncRisible
    {
        Task<T> RiseAsync<T>(object sender, object eventArgs);
    }
}