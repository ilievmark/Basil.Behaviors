using System.Threading;

namespace Basil.Behaviors.Core.Cancellation
{
    public class CancellationProvider : ICancellationProvider
    {
        private readonly CancellationToken _token;

        public CancellationProvider(CancellationToken token)
        {
            _token = token;
        }
        
        public CancellationToken GetCancellationToken()
            => _token;
    }
}