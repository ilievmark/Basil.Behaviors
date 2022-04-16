using System.Threading;

namespace Basil.Behaviors.Core.Cancellation
{
    public interface ICancellationProvider
    {
        CancellationToken GetCancellationToken();
    }
}