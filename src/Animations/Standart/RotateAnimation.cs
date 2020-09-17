using System.Threading.Tasks;
using Xamarin.Forms;

namespace Basil.Behaviors.Animations.Standart
{
    public class RotateAnimation : AnimationBase
    {
        #region Properties

        #region Rotation property

        public static readonly BindableProperty RotationProperty =
            BindableProperty.Create(
                propertyName: nameof(Rotation),
                returnType: typeof(double),
                declaringType: typeof(RotateAnimation),
                defaultValue: default(double));

        public double Rotation
        {
            get => (double)GetValue(RotationProperty);
            set => SetValue(RotationProperty, value);
        }

        #endregion

        #endregion

        public override Task RiseAsync(object sender, object eventArgs)
            => GetAnimationTargetVisualElement().RotateTo(Rotation, Length, Easing);
    }
}