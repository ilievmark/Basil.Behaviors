using System.Threading.Tasks;

namespace Basil.Behaviors.Events.HandlerAbstract
{
    public interface IAsyncRisible : IAsyncBase
    {
        Task RiseAsync(object sender, object eventArgs);
    }
}