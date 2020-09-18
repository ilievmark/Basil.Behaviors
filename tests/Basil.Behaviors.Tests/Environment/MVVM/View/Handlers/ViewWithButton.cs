using Xamarin.Forms;

namespace Basil.Behaviors.Tests.Environment.MVVM.View.Handlers
{
    public class ViewWithButton : Page
    {
        public Button Button { get; private set; }
        
        public ViewWithButton()
        {
            Button = new Button();
        }
    }
}