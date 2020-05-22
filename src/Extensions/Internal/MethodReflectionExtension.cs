using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Basil.Behaviors.Events;
using Basil.Behaviors.Events.Parameters;
using Xamarin.Forms;

namespace Basil.Behaviors.Extensions.Internal
{
    public static class MethodReflectionExtension
    {
        internal static void RunMethod(
            this object targetObject,
            BindableObject associatedObject,
            string methodName,
            IEnumerable<Parameter> parameters)
        {
            if (associatedObject == null)
                return;
            
            if (associatedObject.BindingContext == null)
                return;
            
            if (string.IsNullOrEmpty(methodName))
                return;

            var target = targetObject;
            if (target == null)
                target = associatedObject.BindingContext;

            var targetType = target.GetType();
            var parameterTypes = parameters.Select(i => i.GetParamType()).ToArray();
            var methodInfo = targetType.GetRuntimeMethod(methodName, parameterTypes);
            if (methodInfo == null)
                throw new ArgumentException($"{nameof(MethodReflectionExtension.RunMethod)}: Cant invoke method {methodName}." +
                                            $" There is no method public {methodName} in object of type {targetType.FullName}");

            methodInfo.Invoke(target, parameters.Select(i => i.GetValue()).ToArray());
        }
    }
}