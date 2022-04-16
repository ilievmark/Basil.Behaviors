using System.Globalization;
using System.Windows.Input;
using Basil.Behaviors.Events.HandlerAbstract;
using Xamarin.Forms;

namespace Basil.Behaviors.Extensions
{
    public static class CommandExtensions
    {
        public static void ExecuteCommand(
            this ICommandExecutable executable,
            object args)
            => executable.Command.RunCommand(
                executable.CommandParameter,
                executable.Converter,
                executable.ConverterParameter,
                args);

        internal static void RunCommand(
            this ICommand command,
            object commandParameter,
            IValueConverter converter,
            object converterParameter,
            object args)
        {
            if (command == null)
                return;

            object resolvedParameter;

            if (commandParameter != null)
            {
                resolvedParameter = commandParameter;
            }
            else if (converter != null)
            {
                resolvedParameter = converter.Convert(
                    args,
                    typeof(object),
                    converterParameter,
                    CultureInfo.InvariantCulture);
            }
            else
            {
                resolvedParameter = args;
            }

            if (command.CanExecute(resolvedParameter))
                command.Execute(resolvedParameter);
        }
    }
}