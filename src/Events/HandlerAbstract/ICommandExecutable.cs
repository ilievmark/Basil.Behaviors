using System.Windows.Input;
using Xamarin.Forms;

namespace Basil.Behaviors.Events.HandlerAbstract
{
    public interface ICommandExecutable
    {
        ICommand Command { get; }
        
        object ConverterParameter { get; }
        
        IValueConverter Converter { get; }
        
        object CommandParameter { get; }
    }
}