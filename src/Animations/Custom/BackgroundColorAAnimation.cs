using Xamarin.Forms;

namespace Basil.Behaviors.Animations.Custom
{
    public class BackgroundColorAAnimation : CustomAnimationBase
    {
        protected override void Tick(VisualElement visualElement, double currentValue)
        {
            visualElement.BackgroundColor = new Color(
                visualElement.BackgroundColor.R,
                visualElement.BackgroundColor.G,
                visualElement.BackgroundColor.B,
                currentValue);
        }
    }
}