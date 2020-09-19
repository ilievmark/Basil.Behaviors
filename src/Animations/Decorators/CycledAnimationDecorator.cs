using System.Collections.Generic;
using System.Threading.Tasks;
using Basil.Behaviors.Events.HandlerAbstract;
using Basil.Behaviors.Events.HandlerBase;
using Basil.Behaviors.Extensions;
using Xamarin.Forms;

namespace Basil.Behaviors.Animations.Decorators
{
    [ContentProperty(nameof(Animation))]
    public class CycledAnimationDecorator : AnimationBase, ICompositeHandler
    {
        #region Properties

        #region Cycles

        public static readonly BindableProperty CyclesProperty =
            BindableProperty.Create(
                propertyName: nameof(Cycles),
                returnType: typeof(int),
                declaringType: typeof(CycledAnimationDecorator),
                defaultValue: 1);

        public int Cycles
        {
            get => (int)GetValue(CyclesProperty);
            set => SetValue(CyclesProperty, value);
        }

        #endregion

        #region Animation

        public static readonly BindableProperty AnimationProperty =
            BindableProperty.Create(
                propertyName: nameof(Animation),
                returnType: typeof(BaseHandler),
                declaringType: typeof(CycledAnimationDecorator),
                defaultValue: default(BaseHandler));

        public BaseHandler Animation
        {
            get => (BaseHandler)GetValue(AnimationProperty);
            set => SetValue(AnimationProperty, value);
        }

        #endregion

        #endregion

        #region Overrides
        
        protected override void OnAttachedTo(BindableObject bindable)
        {
            base.OnAttachedTo(bindable);
            
            Animation?.AttachToBindableObject(bindable);
        }

        protected override void OnDetachingFrom(BindableObject bindable)
        {
            base.OnDetachingFrom(bindable);
            
            Animation?.DetachFromBindableObject(bindable);
        }
        
        public override async Task RiseAsync(object sender, object eventArgs)
        {
            for (int i = 0; i < Cycles; i++)
                await Animation.RiseAsAsync(sender, eventArgs);
        }
        
        #endregion

        public IList<BaseHandler> GetInnerHandlers()
            => new List<BaseHandler> { Animation };
    }
}