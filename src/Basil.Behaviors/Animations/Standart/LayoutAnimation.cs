using System.Threading.Tasks;
using Xamarin.Forms;

namespace Basil.Behaviors.Animations.Standart
{
    public class LayoutAnimation : AnimationBase
    {
        #region Properties

        #region OffsetRectangle property

        public static readonly BindableProperty OffsetRectangleProperty =
            BindableProperty.Create(
                propertyName: nameof(OffsetRectangle),
                returnType: typeof(Rectangle),
                declaringType: typeof(LayoutAnimation),
                defaultValue: Rectangle.Zero);

        public Rectangle OffsetRectangle
        {
            get => (Rectangle)GetValue(OffsetRectangleProperty);
            set => SetValue(OffsetRectangleProperty, value);
        }

        #endregion

        #endregion

        public override Task RiseAsync(object sender, object eventArgs)
        {
            var ve = GetAnimationTargetVisualElement();
            var rec = new Rectangle(
                ve.Bounds.X + OffsetRectangle.X,
                ve.Bounds.Y + OffsetRectangle.Y,
                ve.Bounds.Width + OffsetRectangle.Width,
                ve.Bounds.Height + OffsetRectangle.Height
                );
            
            return ve.LayoutTo(rec, Length, Easing);
        }
    }
}