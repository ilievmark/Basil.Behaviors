using Basil.Behaviors.Tests.Environment.MVVM.View.Handlers;
using Basil.Behaviors.Tests.Environment.MVVM.ViewModel.Handlers;
using Xamarin.Forms;

namespace Basil.Behaviors.Tests.Environment.MVVM
{
    public static class EnvironmentManager
    {
        public static Page CreatePageWithButton()
            => new ViewWithButton {BindingContext = new ViewModelSimleHandling()};
    }
}