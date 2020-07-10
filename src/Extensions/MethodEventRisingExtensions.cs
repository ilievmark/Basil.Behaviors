using System.IO;
using System.Threading.Tasks;
using Basil.Behaviors.Events;
using Basil.Behaviors.Events.Handlers;
using Basil.Behaviors.Extensions.Internal;

namespace Basil.Behaviors.Extensions
{
    public static class MethodEventRisingExtensions
    {
        public static Task ExecuteAsyncMethod(this EventToMethodHandler handler)
            => (Task) handler.ExecuteMethod();
        
        public static Task<T> ExecuteAsyncMethod<T>(this EventToMethodHandler handler)
            => (Task<T>) handler.ExecuteMethod();
        
        public static object ExecuteMethod(this EventToMethodHandler handler)
        {
            var target = handler.TargetObject ?? handler.AssociatedObject?.BindingContext ?? handler.AssociatedObject;

            if (target == null)
                throw new InvalidDataException();

            if (!string.IsNullOrEmpty(handler.MethodName) && handler.MethodName.ContainsChar('.'))
                return target.RunMethodPath(handler.MethodName, handler.Parameters);
            
            return target.RunMethod(handler.MethodName, handler.Parameters);
        }
        
        public static Task ExecuteAsyncMethod(this EventToMethodBehavior behavior)
            => (Task) behavior.ExecuteMethod();
        
        public static Task<T> ExecuteAsyncMethod<T>(this EventToMethodBehavior behavior)
            => (Task<T>) behavior.ExecuteMethod();

        public static object ExecuteMethod(this EventToMethodBehavior behavior)
        {
            var target = behavior.TargetObject ?? behavior.AssociatedObject?.BindingContext ?? behavior.AssociatedObject;

            if (target == null)
                throw new InvalidDataException();
            
            
            if (!string.IsNullOrEmpty(behavior.MethodName) && behavior.MethodName.ContainsChar('.'))
                return target.RunMethodPath(behavior.MethodName, behavior.Parameters);
            
            return target.RunMethod(behavior.MethodName, behavior.Parameters);
        }
    }
}