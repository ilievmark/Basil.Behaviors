using System.Collections.Generic;
using System.Threading.Tasks;
using Basil.Behaviors.Events.HandlerAbstract;
using Basil.Behaviors.Events.HandlerBase;
using Basil.Behaviors.Events.Parameters;
using Basil.Behaviors.Extensions;
using Xamarin.Forms;

namespace Basil.Behaviors.Animations.Decorators
{
    [ContentProperty(nameof(Animation))]
    public class CycledWaitAnimationDecorator : AnimationBase, ICompositeHandler, IParameterContainer
    {
        private ReturnParameter<Task> _taskWaitResultParameter = new ReturnParameter<Task>();
        
        #region Properties

        #region Cycles

        public static readonly BindableProperty MinCyclesProperty =
            BindableProperty.Create(
                propertyName: nameof(MinCycles),
                returnType: typeof(int),
                declaringType: typeof(CycledWaitAnimationDecorator),
                defaultValue: 0);

        public int MinCycles
        {
            get => (int)GetValue(MinCyclesProperty);
            set => SetValue(MinCyclesProperty, value);
        }

        #endregion

        #region Animation

        public static readonly BindableProperty AnimationProperty =
            BindableProperty.Create(
                propertyName: nameof(Animation),
                returnType: typeof(BaseHandler),
                declaringType: typeof(CycledWaitAnimationDecorator),
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
            for (int i = 0; i < MinCycles; i++)
                await Animation.RiseAsAsync(sender, eventArgs);

            if (_taskWaitResultParameter.GetValue() is Task task)
                while (task.IsNotCompleted())
                    await Animation.RiseAsAsync(sender, eventArgs);
        }
        
        #endregion

        public IList<BaseHandler> GetInnerHandlers()
            => new List<BaseHandler> { Animation };

        public IEnumerable<Parameter> GetParameters()
            => new List<Parameter> { _taskWaitResultParameter };
    }
}