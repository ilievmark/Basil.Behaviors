using System;
using System.Text.RegularExpressions;
using Basil.Behaviors.Core;

namespace Basil.Behaviors.Validations
{
    public class EmailValidationBehavior : ValidationBehaviorBase<string>
    {
        private const string EmailPattern = 
            "^(([^<>()\\[\\]\\.,;:\\s@\"]+(\\.[^<>()\\[\\]\\.,;:\\s@\"]+)*)|(\".+\"))@((\\[[0-9]{1,3}\\.[0-9]{1,3}\\.[0-9]{1,3}\\.[0-9]{1,3}\\])|(([a-zA-Z\\-0-9]+\\.)+[a-zA-Z]{1,}))$";
        
        #region Overrides

        protected override bool ProcessProperty(string newValue, Func<string> getValueDelegate, Func<string, string> setValueDelegate)
        {
            var result = Regex.IsMatch(newValue, EmailPattern);
            OnValidated(result, newValue);
            Command.Execute(new CommandParams.ValidationResultArgs<string>(newValue, result));
            return result;
        }

        #endregion
    }
}