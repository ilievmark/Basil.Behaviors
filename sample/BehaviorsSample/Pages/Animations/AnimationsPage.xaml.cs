using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BehaviorsSample.Pages.Animations
{
    public partial class AnimationsPage : ContentPage
    {
        public AnimationsPage()
        {
            BindingContext = new AnimationsPageViewModel();
            InitializeComponent();
        }
    }
}