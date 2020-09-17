using System;
using System.Linq;
using System.Threading.Tasks;
using Basil.Behaviors.Extensions;
using Xamarin.Forms;

namespace Basil.Behaviors.Events
{
    public class EventToMultipleHandlersBehavior : EventHandlersBehaviorBase
    {
        private Task _cashedTask;

        public EventToMultipleHandlersBehavior()
        {
            _cashedTask = Task.CompletedTask;
        }
        
        #region Properties
        
        #region SingleRun property
        
        public static readonly BindableProperty SingleRunProperty =
            BindableProperty.Create(
                propertyName: nameof(SingleRun),
                returnType: typeof(bool),
                declaringType: typeof(EventToMultipleHandlersBehavior),
                defaultValue: default(bool));

        public bool SingleRun
        {
            get => (bool)GetValue(SingleRunProperty);
            set => SetValue(SingleRunProperty, value);
        }
        
        #endregion
        
        #endregion
        
        #region Overrides

        protected override async void HandleEvent(object sender, object eventArgs)
        {
            try
            {
                if (SingleRun && _cashedTask != null && _cashedTask.IsNotComplited())
                    return;

                if (Handlers != null && Handlers.Any())
                {
                    _cashedTask = Handlers.RunSequentiallyAsync(sender, eventArgs);
                    await _cashedTask;
                }
            }
            catch (OperationCanceledException e)
            {
            }
        }

        #endregion
    }
}
