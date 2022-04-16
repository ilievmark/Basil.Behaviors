using System;
using System.Linq;
using Basil.Behaviors.Core;
using Basil.Behaviors.Rules.Validation;
using Xamarin.Forms;

namespace Basil.Behaviors.Validations
{
    public class ValidationWithRulesBehavior : PropertyRulesBehaviorBase<string, ValidationRuleBase>
    {
        #region Properties
        
        #region Command property
        
        public static readonly BindableProperty CommandProperty =
            BindableProperty.Create(
                propertyName: nameof(Command),
                returnType: typeof(Command),
                declaringType: typeof(ValidationWithRulesBehavior),
                defaultValue: default(Command));

        public Command Command
        {
            get => (Command)GetValue(CommandProperty);
            set => SetValue(CommandProperty, value);
        }
        
        #endregion

        #endregion
        
        #region Events
        
        #region Validated

        public event Result<string> Validated = delegate { };
        
        #endregion
        
        #endregion

        protected override bool ProcessProperty(string newValue, Func<string> getValueDelegate,
            Func<string, string> setValueDelegate)
        {
            var result = Rules.All(r => r.Verify(newValue));
            Validated(result, newValue);
            Command?.Execute(new CommandParams.ValidationResultArgs<string>(newValue, result));
            return result;
        }
    }
}