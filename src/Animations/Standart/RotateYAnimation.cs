using System.Threading.Tasks;
using Xamarin.Forms;

namespace Basil.Behaviors.Animations.Standart
{
    public class RotateYAnimation : AnimationBase
    {
        #region Properties

        #region Rotation property

        public static readonly BindableProperty RotationYProperty =
            BindableProperty.Create(
                propertyName: nameof(RotationY),
                returnType: typeof(double),
                declaringType: typeof(RotateYAnimation),
                defaultValue: default(double));

        public double RotationY
        {
            get => (double)GetValue(RotationYProperty);
            set => SetValue(RotationYProperty, value);
        }

        #endregion

        #endregion

        public override Task RiseAsync(object sender, object eventArgs)
            => GetAnimationTargetVisualElement().RotateYTo(RotationY, Length, Easing);
    }
}