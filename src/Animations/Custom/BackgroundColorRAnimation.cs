using Xamarin.Forms;

namespace Basil.Behaviors.Animations.Custom
{
    public class BackgroundColorRAnimation : CustomAnimationBase
    {
        protected override void Tick(VisualElement visualElement, double currentValue)
        {
            visualElement.BackgroundColor = new Color(
                currentValue,
                visualElement.BackgroundColor.G,
                visualElement.BackgroundColor.B);
        }
    }
}