using System.Threading;

namespace Basil.Behaviors.Events.Parameters.Abstract
{
    public interface ICancellation
    {
        void SetCancellationToken(CancellationToken token);
    }
}