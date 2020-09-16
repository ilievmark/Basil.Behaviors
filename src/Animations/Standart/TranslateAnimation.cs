using System.Threading.Tasks;
using Xamarin.Forms;

namespace Basil.Behaviors.Animations.Standart
{
    public class TranslateAnimation : AnimationBase
    {
        #region Properties

        #region XOffset property

        public static readonly BindableProperty XOffsetProperty =
            BindableProperty.Create(
                propertyName: nameof(XOffset),
                returnType: typeof(double),
                declaringType: typeof(TranslateAnimation),
                defaultValue: default(double));

        public double XOffset
        {
            get => (double)GetValue(XOffsetProperty);
            set => SetValue(XOffsetProperty, value);
        }

        #endregion

        #region YOffset property

        public static readonly BindableProperty YOffsetProperty =
            BindableProperty.Create(
                propertyName: nameof(YOffset),
                returnType: typeof(double),
                declaringType: typeof(TranslateAnimation),
                defaultValue: default(double));

        public double YOffset
        {
            get => (double)GetValue(YOffsetProperty);
            set => SetValue(YOffsetProperty, value);
        }

        #endregion
        
        #endregion
        
        public override Task RiseAsync(object sender, object eventArgs)
        {
            var targetView = GetAnimationTargetVisualElement();

            return targetView.TranslateTo(
                targetView.TranslationX + XOffset,
                targetView.TranslationY + YOffset,
                Length,
                Easing);
        }
    }
}