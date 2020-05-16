using BehaviorsSample.ViewModels;
using Xamarin.Forms;

namespace BehaviorsSample.Views
{
    public partial class EventPage : ContentPage
    {
        public EventPage()
        {
            BindingContext = new EventPageViewModel();
            InitializeComponent();

        }
    }
}
