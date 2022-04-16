using System;
namespace Basil.Behaviors.Core.Rules
{
    public class BaseRule : AttachableBindableObject
    {
        public virtual string Rule { get; set; }

        public abstract bool Verify(string substring);
    }
}
