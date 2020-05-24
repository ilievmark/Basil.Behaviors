using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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
            IList<Parameter> parameters)
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
            var methodInfo = targetType
                .GetRuntimeMethods()
                .Where(m => m.Name == methodName)
                .FirstOrDefault(m => m
                    .GetParameters()
                    .Where(p => !p.IsOptional)
                    .Select(p => p.ParameterType)
                    .SequenceEqual(parameters
                        .Where(p=> !p.IsOptional())
                        .Select(p => p.GetParamType())
                    )
                );
            
            if (methodInfo == null)
                throw new ArgumentException($"{nameof(MethodReflectionExtension.RunMethod)}: Cant invoke method {methodName}." +
                                            $" There is no method {methodName} in object of type {targetType.FullName}");

            var sortedParams = CalculateParameters(methodInfo, parameters);
            
            methodInfo.Invoke(target, sortedParams);
        }
        
        private static object[] CalculateParameters(MethodInfo usedMethod, IList<Parameter> parameters)
        {
            var methodParamsInfo = usedMethod.GetParameters();
            var notOptionalMethodParamsInfo = methodParamsInfo.Where(p => !p.IsOptional).ToList();
            var parametersList = new List<object>(methodParamsInfo.Length);

            for (var i = 0; i < notOptionalMethodParamsInfo.Count; i++)
            {
                if (notOptionalMethodParamsInfo[i].ParameterType == parameters[i].GetParamType())
                {
                    parametersList.Add(parameters[i].GetValue());
                }
                else
                {
                    throw new ArgumentException($"Given parameters not matched types for method {usedMethod.Name}");
                }
            }
            
            var namedParameters = parameters.Where(p => p is NamedParameter).Cast<NamedParameter>();
            
            foreach (var parameter in methodParamsInfo.Where(p => p.IsOptional))
            {
                var namedParameter = namedParameters.FirstOrDefault(p => p.Name == parameter.Name);
                if (namedParameter != null)
                {
                    parametersList.Add(namedParameter.GetValue());
                }
                else
                {
                    parametersList.Add(parameter.HasDefaultValue
                        ? parameter.DefaultValue
                        : parameter.ParameterType.GetDefaultValue()
                    );
                }
            }
            
            return parametersList.ToArray();
        }
    }
}