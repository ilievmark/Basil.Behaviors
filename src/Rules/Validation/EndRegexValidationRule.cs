using System.Text.RegularExpressions;

namespace Basil.Behaviors.Rules.Validation
{
    public class EndRegexValidationRule: ValidationRuleBase
    {
        public int Length { get; set; }
        
        public override bool Verify(string str)
            => IsLengthValid &&
               IsStringValid(str) &&
               Regex.IsMatch(str.Substring(0, Length), Rule);

        private bool IsLengthValid => Length >= 1;
        private bool IsStringValid(string str) => str.Length >= Length;
    }
}