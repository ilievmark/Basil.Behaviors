using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Basil.Behaviors.Events.HandlerAbstract;
using Basil.Behaviors.Events.Parameters;

namespace Basil.Behaviors.Extensions
{
    public static class MethodEventRisingExtensions
    {
        public static Task ExecuteAsyncMethod(this IMethodExecutable executable)
            => (Task) executable.ExecuteMethod();
        
        public static Task<T> ExecuteAsyncMethod<T>(this IMethodExecutable executable)
        { 
            var task = (Task) executable.ExecuteMethod();
            var tcs = new TaskCompletionSource<T>();
            task.ContinueWith(t =>
            {
                var res = task.GetPropertyValue(nameof(Task<T>.Result));
                tcs.SetResult((T) res);
            });
            return tcs.Task;
        }
        
        public static object ExecuteMethod(this IMethodExecutable executable)
        {
            var target = executable.GetTargetMethodRiseObject();
            if (target == null)
                throw new InvalidDataException();

            var parameters = new List<Parameter>();
            if (executable is IParameterContainer castedParametrizedExecutable)
                parameters = castedParametrizedExecutable.GetParameters().ToList();
            
            if (!string.IsNullOrEmpty(executable.MethodName) && executable.MethodName.ContainsChar('.'))
                return target.RunMethodPath(executable.MethodName, parameters);
            
            return target.RunMethod(executable.MethodName, parameters);
        }
    }
}