using System.Diagnostics;
using System.Windows.Input;
using Xamarin.Forms;

namespace BehaviorsSample.ViewModels
{
    public class EventPageViewModel : BaseViewModel
    {
        public ICommand Sample1Command => new Command(OnSample1Command);

        private void OnSample1Command()
        {
            Debug.WriteLine("Hello from Sample1Command");
        }


        public ICommand Sample2Command => new Command(OnSample2Command);

        private void OnSample2Command()
        {
            Debug.WriteLine("Hello from Sample2Command");
        }
    }
}
