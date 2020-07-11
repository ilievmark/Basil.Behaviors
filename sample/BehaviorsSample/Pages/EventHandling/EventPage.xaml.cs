using Xamarin.Forms;

namespace BehaviorsSample.Pages.EventHandling
{
    public partial class EventPage : ContentPage
    {
        public EventPage()
        {
            InitializeComponent();
            BindingContext = new EventPageViewModel();
        }
    }
}