using BehaviorsSample.ViewModels;
using Xamarin.Forms;

namespace BehaviorsSample.Views
{
    public partial class MaskPage : ContentPage
    {
        public MaskPage()
        {
            BindingContext = new MaskPageViewModel();
            InitializeComponent();
        }
    }
}