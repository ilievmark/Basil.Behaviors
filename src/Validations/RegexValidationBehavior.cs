using System;
using System.Text.RegularExpressions;
using Basil.Behaviors.Core;

namespace Basil.Behaviors.Validations
{
    public class RegexValidationBehavior : ValidationBehaviorBase<string>
    {   
        #region Overrides

        protected override bool ProcessProperty(string newValue, Func<string> getValueDelegate, Func<string, string> setValueDelegate)
        {
            var result = Regex.IsMatch(newValue, Pattern);
            OnValidated(result, newValue);
            Command.Execute(new CommandParams.ValidationResultArgs<string>(newValue, result));
            return result;
        }

        #endregion
    }
}