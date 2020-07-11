using Xamarin.Forms;

namespace BehaviorsSample.Pages.Masks
{
    public partial class MaskPage : ContentPage
    {
        public MaskPage()
        {
            InitializeComponent();
            BindingContext = new MaskPageViewModel();
        }
    }
}