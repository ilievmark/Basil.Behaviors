using System;
using System.Text.RegularExpressions;
using Basil.Behaviors.Core;
using Xamarin.Forms;

namespace Basil.Behaviors.Validations
{
    public class RegexValidationBehavior : ValidationBehaviorBase<string>
    {
        #region Properties
        
        #region Pattern property
        
        public static readonly BindableProperty PatternProperty =
            BindableProperty.Create(
                propertyName: nameof(Pattern),
                returnType: typeof(string),
                declaringType: typeof(RegexValidationBehavior),
                defaultValue: string.Empty);

        public string Pattern
        {
            get => (string)GetValue(PatternProperty);
            set => SetValue(PatternProperty, value);
        }

        #endregion
        
        #endregion
        
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