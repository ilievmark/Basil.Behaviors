using Basil.Behaviors.Core;

namespace Basil.Behaviors.Rules
{
    public abstract class BaseRule : AttachableBindableObject
    {
        public virtual string Rule { get; set; }

        public abstract bool Verify(string substring);
    }
}