using System.Threading.Tasks;

namespace Basil.Behaviors.Events.HandlerAbstract
{
    public interface IAsyncRisible
    {
        Task RiseAsync(object sender, object eventArgs);
    }
}