using Basil.Behaviors.Core;
using Xamarin.Forms;

namespace Basil.Behaviors.Validations
{
    public abstract class ValidationBehaviorBase<TProperty> : PropertyChangedBehaviorBase<TProperty>
    {
        #region Properties
        
        #region Command property
        
        public static readonly BindableProperty CommandProperty =
            BindableProperty.Create(
                propertyName: nameof(Command),
                returnType: typeof(Command),
                declaringType: typeof(ValidationBehaviorBase<TProperty>),
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

        public event Result<TProperty> Validated = delegate { };
        
        #endregion
        
        #endregion

        protected void OnValidated(bool isValid, TProperty val) => Validated(isValid, val);
    }
}