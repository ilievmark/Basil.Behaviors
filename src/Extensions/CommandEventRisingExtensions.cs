using Basil.Behaviors.Events;
using Basil.Behaviors.Events.Handlers;
using Basil.Behaviors.Extensions.Internal;

namespace Basil.Behaviors.Extensions
{
    public static class CommandEventRisingExtensions
    {
        public static void ExecuteCommand(this EventToCommandHandler handler, object args)
            => handler.Command.RunCommand(
                handler.CommandParameter,
                handler.Converter,
                handler.ConverterParameter,
                args);
        
        public static void ExecuteCommand(this EventToCommandBehavior behavior, object args)
            => behavior.Command.RunCommand(
                behavior.CommandParameter,
                behavior.Converter,
                behavior.ConverterParameter,
                args);
    }
}