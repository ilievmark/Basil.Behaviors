using Basil.Behaviors.Events.HandlerBase;
using Xamarin.Forms;

namespace Basil.Behaviors.Animations
{
    public abstract class AnimationBase : BaseAsyncHandler
    {
        #region Properties

        #region Target property

        public static readonly BindableProperty TargetProperty =
            BindableProperty.Create(
                propertyName: nameof(Target),
                returnType: typeof(VisualElement),
                declaringType: typeof(AnimationBase),
                defaultValue: default(VisualElement));

        public VisualElement Target
        {
            get => (VisualElement)GetValue(TargetProperty);
            set => SetValue(TargetProperty, value);
        }

        #endregion
        
        #region Easing property
        
        public static readonly BindableProperty EasingProperty =
            BindableProperty.Create(
                propertyName: nameof(Easing),
                returnType: typeof(Easing),
                declaringType: typeof(AnimationBase),
                defaultValue: Easing.Linear);

        public Easing Easing
        {
            get => (Easing)GetValue(EasingProperty);
            set => SetValue(EasingProperty, value);
        }
        
        #endregion
        
        #region Length property
        
        public static readonly BindableProperty LengthProperty =
            BindableProperty.Create(
                propertyName: nameof(Length),
                returnType: typeof(uint),
                declaringType: typeof(AnimationBase),
                defaultValue: 250U);

        public uint Length
        {
            get => (uint)GetValue(LengthProperty);
            set => SetValue(LengthProperty, value);
        }
        
        #endregion

        #endregion

        protected VisualElement GetAnimationTargetVisualElement()
            => Target ?? (VisualElement)AssociatedObject;
    }
}