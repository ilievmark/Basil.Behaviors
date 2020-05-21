using BehaviorsSample.ViewModels;
using Xamarin.Forms;

namespace BehaviorsSample.Views
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

            BindingContext = new MainPageViewModel();
        }
    }
}
