using System.Threading.Tasks;
using Xamarin.Forms;

namespace Basil.Behaviors.Animations.Standart
{
    public class RelativeScaleAnimation: AnimationBase
    {
        #region Properties

        #region ScaleTo property

        public static readonly BindableProperty ScaleProperty =
            BindableProperty.Create(
                propertyName: nameof(Scale),
                returnType: typeof(double),
                declaringType: typeof(RelativeScaleAnimation),
                defaultValue: default(double));

        public double Scale
        {
            get => (double)GetValue(ScaleProperty);
            set => SetValue(ScaleProperty, value);
        }

        #endregion

        #endregion

        public override Task RiseAsync(object sender, object eventArgs)
            => GetAnimationTargetVisualElement().RelScaleTo(Scale, Length, Easing);
    }
}