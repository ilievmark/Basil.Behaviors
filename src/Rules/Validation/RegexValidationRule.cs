using System.Text.RegularExpressions;

namespace Basil.Behaviors.Rules.Validation
{
    public class RegexValidationRule : ValidationRuleBase
    {
        public override bool Verify(string str) => Regex.IsMatch(str, Rule);
    }
}