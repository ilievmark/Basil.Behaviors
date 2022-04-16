using System.Threading.Tasks;
using Xamarin.Forms;

namespace Basil.Behaviors.Animations.Standart
{
    public class RotateXAnimation : AnimationBase
    {
        #region Properties

        #region Rotation property

        public static readonly BindableProperty RotationXProperty =
            BindableProperty.Create(
                propertyName: nameof(RotationX),
                returnType: typeof(double),
                declaringType: typeof(RotateXAnimation),
                defaultValue: default(double));

        public double RotationX
        {
            get => (double)GetValue(RotationXProperty);
            set => SetValue(RotationXProperty, value);
        }

        #endregion

        #endregion

        public override Task RiseAsync(object sender, object eventArgs)
            => GetAnimationTargetVisualElement().RotateXTo(RotationX, Length, Easing);
    }
}