using Xamarin.Forms;

namespace Basil.Behaviors.Tests.Environment.MVVM.View.Handlers
{
    public class ViewWithButton : ContentPage
    {
        private int _parameterValue;
        
        public Button Button { get; private set; }
        
        public ViewWithButton()
        {
            Button = new Button();
            Content = Button;
        }
    }
}