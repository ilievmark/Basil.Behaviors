using System.Diagnostics;
using System.Windows.Input;
using Basil.Behaviors.Core;
using Xamarin.Forms;

namespace BehaviorsSample.ViewModels
{
    public class ValidationPageViewModel : BaseViewModel
    {
        #region Regex validated command

        public ICommand ValidatedCommand => new Command<CommandParams.ValidationResultArgs<string>>(OnValidatedCommand);

        private void OnValidatedCommand(CommandParams.ValidationResultArgs<string> parameters)
        {
            Debug.WriteLine($"Value validated - {parameters.Value}, result is - {parameters.Valid}");
        }

        #endregion
        
        #region Email validated command

        public ICommand EmailValidatedCommand => new Command<CommandParams.ValidationResultArgs<string>>(OnEmailValidatedCommand);

        private void OnEmailValidatedCommand(CommandParams.ValidationResultArgs<string> parameters)
        {
            Debug.WriteLine($"Value validated - {parameters.Value}, result is - {parameters.Valid}");
        }

        #endregion
    }
}