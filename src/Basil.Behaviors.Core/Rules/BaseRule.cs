using Basil.Behaviors.Core.Abstract;

namespace Basil.Behaviors.Core.Rules
{
    public abstract class BaseRule : AttachableBindableObject
    {
        public virtual string Rule { get; set; }

        public abstract bool Verify(string substring);
    }
}
