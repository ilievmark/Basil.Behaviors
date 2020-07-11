using System;
using System.Linq;

namespace Basil.Behaviors.Rules.Mask
{
    public class CharMaskRule : MaskRuleBase
    {
        public String AllowedChars { get; set; } = string.Empty;
        
        public override bool Verify(string substring)
            => substring.Length == 1 && substring.Cast<char>().All(c => AllowedChars.Cast<char>().Contains(c));
    }
}