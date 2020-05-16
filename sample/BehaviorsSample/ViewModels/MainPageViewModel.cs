using System.Windows.Input;
using BehaviorsSample.Views;
using Xamarin.Forms;

namespace BehaviorsSample.ViewModels
{
    public class MainPageViewModel : BaseViewModel
    {
        #region Go to EventPage command

        public ICommand GoToEventPage => new Command(OnGoToEventPage);

        private void OnGoToEventPage()
            => App.Current.MainPage = new EventPage();

        #endregion
    }
}
