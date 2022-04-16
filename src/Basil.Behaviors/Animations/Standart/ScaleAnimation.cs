using System.Threading.Tasks;
using Xamarin.Forms;

namespace Basil.Behaviors.Animations.Standart
{
    public class ScaleAnimation : AnimationBase
    {
        #region Properties

        #region ScaleTo property

        public static readonly BindableProperty ScaleProperty =
            BindableProperty.Create(
                propertyName: nameof(Scale),
                returnType: typeof(double),
                declaringType: typeof(ScaleAnimation),
                defaultValue: default(double));

        public double Scale
        {
            get => (double)GetValue(ScaleProperty);
            set => SetValue(ScaleProperty, value);
        }

        #endregion

        #endregion

        public override Task RiseAsync(object sender, object eventArgs)
            => GetAnimationTargetVisualElement().ScaleTo(Scale, Length, Easing);
    }
}