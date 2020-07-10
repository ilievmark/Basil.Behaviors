using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using BehaviorsSample.Models;
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
        
        public string ReturnStringMethod()
        {
            return "Hello string from ReturnStringMethod";
        }
        
        protected void JustAMethodNoMore(string param)
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
            Debug.WriteLine($"Its not so crazy hard... {someParamName}");
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

        public int GetInt()
        {
            return 23;
        }

        public string GetString(int result)
        {
            return $"string with previous result int = {result}";
        }

        public async Task RunResultActions(string result)
        {
            await Task.Delay(4000);
            Debug.WriteLine(result);
        }
        
        public async Task<string> RunResultActionsAndReturn(string result)
        {
            await Task.Delay(4000);
            Debug.WriteLine(result);
            return result;
        }
        
        public async Task RunResultActionsWithPrevTask(Task task)
        {
            await task;
            await Task.Delay(3000);
            Debug.WriteLine("Done");
        }
        
        public async Task RunResultActionsWithPrevTaskWithResult(Task<string> task)
        {
            var result = await task;
            await Task.Delay(3000);
            Debug.WriteLine(result);
        }

        public InjectedModel Model => new InjectedModel(new InjectedModel(new InjectedModel()));
    }
}
