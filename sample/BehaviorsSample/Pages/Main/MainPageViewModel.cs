using System.Windows.Input;
using BehaviorsSample.Pages.Animations;
using BehaviorsSample.Pages.EventHandling;
using BehaviorsSample.Pages.Masks;
using BehaviorsSample.Pages.Validations;
using Xamarin.Forms;

namespace BehaviorsSample.Pages.Main
{
    public class MainPageViewModel : BaseViewModel
    {
        #region Go to EventPage command

        public ICommand GoToEventPage => new Command(OnGoToEventPage);

        private async void OnGoToEventPage()
            => await ((NavigationPage)App.Current.MainPage).PushAsync(new EventPage());

        #endregion
        
        #region Go to ValidationPage command

        public ICommand GoToValidationPage => new Command(OnGoToValidationPage);

        private async void OnGoToValidationPage()
            => await ((NavigationPage)App.Current.MainPage).PushAsync(new ValidationPage());

        #endregion
        
        #region Go to MaskPage command

        public ICommand GoToMaskPage => new Command(OnGoToMaskPage);

        private async void OnGoToMaskPage()
            => await ((NavigationPage)App.Current.MainPage).PushAsync(new MaskPage());

        #endregion
        
        #region Go to AnimationsPage command
        
        public ICommand GoToAnimationsPage => new Command(OnGoToAnimationsPage);

        private async void OnGoToAnimationsPage()
            => await ((NavigationPage)App.Current.MainPage).PushAsync(new AnimationsPage());
        
        #endregion
    }
}
