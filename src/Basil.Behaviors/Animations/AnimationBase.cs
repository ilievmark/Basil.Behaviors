using Basil.Behaviors.Events.HandlerBase;
using Xamarin.Forms;

namespace Basil.Behaviors.Animations
{
    public abstract class AnimationBase : AnimationBase<VisualElement>
    {
    }
    
    public abstract class AnimationBase<TVisual> : BaseAsyncHandler
        where TVisual : VisualElement
    {
        public AnimationBase()
        {
            WaitResult = true;
        }
        
        #region Properties

        #region Target property

        public static readonly BindableProperty TargetProperty =
            BindableProperty.Create(
                propertyName: nameof(Target),
                returnType: typeof(TVisual),
                declaringType: typeof(AnimationBase<TVisual>),
                defaultValue: default(TVisual));

        public TVisual Target
        {
            get => (TVisual)GetValue(TargetProperty);
            set => SetValue(TargetProperty, value);
        }

        #endregion
        
        #region Easing property
        
        public static readonly BindableProperty EasingProperty =
            BindableProperty.Create(
                propertyName: nameof(Easing),
                returnType: typeof(Easing),
                declaringType: typeof(AnimationBase<TVisual>),
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
                declaringType: typeof(AnimationBase<TVisual>),
                defaultValue: 250U);

        public uint Length
        {
            get => (uint)GetValue(LengthProperty);
            set => SetValue(LengthProperty, value);
        }
        
        #endregion

        #endregion

        protected TVisual GetAnimationTargetVisualElement()
            => Target ?? (TVisual)AssociatedObject;
    }
}