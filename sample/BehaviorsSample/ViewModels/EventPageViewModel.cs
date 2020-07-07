using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
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
        
        public void JustAMethodNoMore(string param)
        {
            Debug.WriteLine($"Hello from JustAMethodNoMore {param}");
        }
        
        public void JustAMethodTooButWithParameterBinding(ICommand commandParam, string stringParam)
        {
            Debug.WriteLine($"Mvvm??? Ha-ha!");
            Debug.WriteLine(stringParam);
            commandParam.Execute(null);
        }
        
        public void MethodWithNamedParam(ICommand commandParam, string stringParam, int defaultInt = 0, float g = 4.4f, string someParamName = "Default value", object d = null)
        {
            Debug.WriteLine($"Its not so crasy hard... {someParamName}");
            Debug.WriteLine(stringParam);
            commandParam.Execute(null);
        }

        public void Method1()
        {
            Thread.Sleep(1000);
        }
        
        public async Task MethodAsync2()
        {
            await Task.Delay(1000);
        }
    }
}
