using System.Threading.Tasks;
using Xamarin.Forms;

namespace Basil.Behaviors.Animations.Standart
{
    public class RelativeRotateXAnimation : AnimationBase
    {
        #region Properties

        #region Rotation property

        public static readonly BindableProperty RotationXProperty =
            BindableProperty.Create(
                propertyName: nameof(RotationX),
                returnType: typeof(double),
                declaringType: typeof(RelativeRotateXAnimation),
                defaultValue: default(double));

        public double RotationX
        {
            get => (double)GetValue(RotationXProperty);
            set => SetValue(RotationXProperty, value);
        }

        #endregion

        #endregion

        public override Task RiseAsync(object sender, object eventArgs)
        {
            var ve = GetAnimationTargetVisualElement();
            return ve.RotateXTo(ve.RotationX + RotationX, Length, Easing);
        }
    }
}