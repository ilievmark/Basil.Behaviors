using Basil.Behaviors.Core;
using Xamarin.Forms;

namespace Basil.Behaviors.Validations
{
    public abstract class ValidationBehaviorBase<TProperty> : PropertyChangedBehaviorBase<TProperty>
    {
        #region Properties
        
        #region Pattern property
        
        public static readonly BindableProperty PatternProperty =
            BindableProperty.Create(
                propertyName: nameof(Pattern),
                returnType: typeof(string),
                declaringType: typeof(ValidationBehaviorBase<TProperty>),
                defaultValue: string.Empty);

        public string Pattern
        {
            get => (string)GetValue(PatternProperty);
            set => SetValue(PatternProperty, value);
        }
        
        #endregion

        #endregion
        
        #region Events
        
        #region Validated

        public event Result<TProperty> Validated = delegate { };
        
        #endregion
        
        #endregion
        
        
    }
}