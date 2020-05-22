using Basil.Behaviors.Events;
using Basil.Behaviors.Events.Handlers;
using Basil.Behaviors.Extensions.Internal;

namespace Basil.Behaviors.Extensions
{
    public static class MethodEventRisingExtensions
    {
        public static void ExecuteMethod(this EventToMethodHandler handler)
            => handler.TargetObject.RunMethod(
                handler.AssociatedObject,
                handler.MethodName,
                handler.Parameters);
        
        public static void ExecuteMethod(this EventToMethodBehavior behavior)
            => behavior.TargetObject.RunMethod(
                behavior.AssociatedObject,
                behavior.MethodName,
                behavior.Parameters);
    }
}