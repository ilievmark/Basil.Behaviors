using System.Diagnostics;
using System.Threading.Tasks;

namespace BehaviorsSample.Pages.Animations
{
    public class AnimationsPageViewModel : BaseViewModel
    {
        public async Task MethodAsync()
        {
            Debug.WriteLine("Begin long async operation");
            await Task.Delay(4000);
            Debug.WriteLine("Complete long async operation");
        }
    }
}