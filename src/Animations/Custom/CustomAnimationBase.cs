using System.Threading.Tasks;
using Xamarin.Forms;

namespace Basil.Behaviors.Animations.Custom
{
    public abstract class CustomAnimationBase : CustomAnimationBase<VisualElement>
    {
    }

    public abstract class CustomAnimationBase<TVisual> : AnimationBase<TVisual>
        where TVisual : VisualElement
    {
        #region Properties

        #region StartValue property

        public static readonly BindableProperty StartValueProperty =
            BindableProperty.Create(
                propertyName: nameof(StartValue),
                returnType: typeof(double),
                declaringType: typeof(CustomAnimationBase<TVisual>),
                defaultValue: 0.0);

        public double StartValue
        {
            get => (double)GetValue(StartValueProperty);
            set => SetValue(StartValueProperty, value);
        }

        #endregion

        #region EndValue property

        public static readonly BindableProperty EndValueProperty =
            BindableProperty.Create(
                propertyName: nameof(EndValue),
                returnType: typeof(double),
                declaringType: typeof(CustomAnimationBase<TVisual>),
                defaultValue: 0.0);

        public double EndValue
        {
            get => (double)GetValue(EndValueProperty);
            set => SetValue(EndValueProperty, value);
        }

        #endregion

        #region Rate property

        public static readonly BindableProperty RateProperty =
            BindableProperty.Create(
                propertyName: nameof(Rate),
                returnType: typeof(uint),
                declaringType: typeof(CustomAnimationBase<TVisual>),
                defaultValue: 16U);

        public uint Rate
        {
            get => (uint)GetValue(RateProperty);
            set => SetValue(RateProperty, value);
        }

        #endregion
        
        #endregion
        
        public sealed override Task RiseAsync(object sender, object eventArgs)
        {
            var tcs = new TaskCompletionSource<bool>();
            var ve = GetAnimationTargetVisualElement();
            new Animation(d => Tick(ve, d), StartValue, EndValue, Easing)
                .Commit(ve, GetType().Name, Rate, Length, Easing, (d, b) => tcs.SetResult(b));
            return tcs.Task;
        }

        protected abstract void Tick(TVisual visualElement, double currentValue);
    }
}