using Xamarin.Forms;

namespace BehaviorsSample.Pages.Validations
{
    public partial class ValidationPage : ContentPage
    {
        public ValidationPage()
        {
            InitializeComponent();
            BindingContext = new ValidationPageViewModel();
        }
    }
}