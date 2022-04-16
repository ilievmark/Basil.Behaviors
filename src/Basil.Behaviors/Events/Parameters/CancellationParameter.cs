using System;
using System.Threading;
using Basil.Behaviors.Events.Parameters.Abstract;

namespace Basil.Behaviors.Events.Parameters
{
    public class CancellationParameter : Parameter, ICancellation
    {
        private CancellationToken _token;

        public void SetCancellationToken(CancellationToken token)
            => _token = token;

        public override object GetValue()
            => _token;

        public override Type GetParamType()
            => typeof(CancellationToken);
    }
}