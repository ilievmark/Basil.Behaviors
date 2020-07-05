using System;
using System.Linq;

namespace Basil.Behaviors.Rules.Mask
{
    public abstract class MaskRuleBase : BaseRule
    {
        public String AllowedChars { get; set; } = string.Empty;
        
        public override bool Verify(string substring)
            => substring.Cast<char>().All(c => AllowedChars.Cast<char>().Contains(c));
    }
}