using System.Threading.Tasks;

namespace Basil.Behaviors.Events.HandlerAbstract
{
    public interface IAsyncRisible
    {
        bool WaitResult { get; }
        
        Task RiseAsync(object sender, object eventArgs);
    }
}