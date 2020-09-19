using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Basil.Behaviors.Tests.Environment.MVVM.ViewModel.Handlers
{
    public class ViewModelSimleHandling
    {
        private int _parameterValue;
        
        public ICommand Command { get; }

        public int ExecuteCommandCount;
        public int ExecuteMethodCount;
        public int ExecuteAsyncMethodCount;

        public ViewModelSimleHandling()
        {
            Command = new Command(() => ExecuteCommandCount++);
        }

        public void Method() => ExecuteMethodCount++;

        public async Task MethodAsync() => ExecuteAsyncMethodCount++;
        
        
        public int MethodWithReturnInt() => 9;
        
        public void MethodWithParameterInt(int parameter) => _parameterValue = parameter;
    }
}