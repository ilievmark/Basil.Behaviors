using System.Text.RegularExpressions;

namespace Basil.Behaviors.Rules.Validation
{
    public class StartRegexValidationRule : ValidationRuleBase
    {
        public int StartIndex { get; set; }
        
        public override bool Verify(string str)
            => IsIndexValid &&
               IsStringValid(str) &&
               Regex.IsMatch(str.Substring(StartIndex), Rule);

        private bool IsIndexValid => StartIndex >= 0;
        private bool IsStringValid(string str) => str.Length >= StartIndex;
    }
}