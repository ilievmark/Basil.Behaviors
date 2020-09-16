using System.Threading.Tasks;
using Xamarin.Forms;

namespace Basil.Behaviors.Animations.Standart
{
    public class ScaleAnimation : AnimationBase
    {
        #region Properties

        #region ScaleTo property

        public static readonly BindableProperty ScaleToProperty =
            BindableProperty.Create(
                propertyName: nameof(ScaleTo),
                returnType: typeof(double),
                declaringType: typeof(ScaleAnimation),
                defaultValue: default(double));

        public double ScaleTo
        {
            get => (double)GetValue(ScaleToProperty);
            set => SetValue(ScaleToProperty, value);
        }

        #endregion

        #endregion

        public override Task RiseAsync(object sender, object eventArgs)
            => GetAnimationTargetVisualElement().ScaleTo(ScaleTo, Length, Easing);
    }
}