using System;
using System.Linq;

namespace Basil.Behaviors.Rules.Mask
{
    public class IgnoreCaseCharMaskRule : MaskRuleBase
    {
        private string _allowedChars = string.Empty;
        public String AllowedChars
        {
            get => _allowedChars;
            set => _allowedChars = value.ToUpper();
        }
        
        public override bool Verify(string substring)
            => substring.Length == 1 && substring.ToUpper().Cast<char>().All(c => AllowedChars.Cast<char>().Contains(c));
    }
}