using BehaviorsSample.ViewModels;
using Xamarin.Forms;

namespace BehaviorsSample.Views
{
    public partial class ValidationPage : ContentPage
    {
        public ValidationPage()
        {
            BindingContext = new ValidationPageViewModel();
            InitializeComponent();
        }
    }
}