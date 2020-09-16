using System.Threading.Tasks;
using Xamarin.Forms;

namespace Basil.Behaviors.Animations.Standart
{
    public class FadeAnimation : AnimationBase
    {
        #region Properties

        #region Opacity property

        public static readonly BindableProperty OpacityProperty =
            BindableProperty.Create(
                propertyName: nameof(Opacity),
                returnType: typeof(double),
                declaringType: typeof(FadeAnimation),
                defaultValue: default(double));

        public double Opacity
        {
            get => (double)GetValue(OpacityProperty);
            set => SetValue(OpacityProperty, value);
        }

        #endregion

        #endregion

        public override Task RiseAsync(object sender, object eventArgs)
            => GetAnimationTargetVisualElement().FadeTo(Opacity, Length, Easing);
    }
}