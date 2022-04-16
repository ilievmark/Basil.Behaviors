using System.Text.RegularExpressions;

namespace Basil.Behaviors.Rules.Validation
{
    public class PositionedRegexValidationRule : ValidationRuleBase
    {
        public int Index { get; set; }
        public int Length { get; set; }
        
        public override bool Verify(string str)
            => IsIndexValid &&
               IsLengthValid &&
               IsStringValid(str) &&
               Regex.IsMatch(str.Substring(Index, Length), Rule);

        private bool IsIndexValid => Index >= 0;
        private bool IsLengthValid => Length >= 1;
        private bool IsStringValid(string str) => str.Length >= Index + Length;
    }
}