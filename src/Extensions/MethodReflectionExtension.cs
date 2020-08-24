using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Basil.Behaviors.Events.Parameters;
using Basil.Behaviors.Extensions.Internal;

namespace Basil.Behaviors.Extensions
{
    public static class MethodReflectionExtension
    {
        private const int MethodPositionOffset = 1;
        
        public static object RunMethodPath(
            this object targetObject,
            string methodPath,
            IList<Parameter> parameters)
        {
            if (targetObject == null)
                throw new ArgumentNullException(nameof(targetObject));
            
            if (string.IsNullOrEmpty(methodPath))
                throw new ArgumentNullException(nameof(methodPath));
            
            if (!methodPath.ContainsChar('.'))
                throw new ArgumentException($"{nameof(methodPath)} == {methodPath}: doesnt contains . and not represent path to method");
            
            parameters ??= new List<Parameter>();

            var parts = methodPath.Split('.');
            var realTarget = GetRealTarget(targetObject, parts);
            var methodName = parts[parts.Length - MethodPositionOffset];

            return realTarget.RunMethod(methodName, parameters);
        }
        
        private static object GetRealTarget(object target, string[] parts)
        {
            var actualParts = parts.Take(parts.Length - MethodPositionOffset);
            var result = target;
            
            foreach (var part in actualParts)
            {
                if (string.IsNullOrWhiteSpace(part))
                    throw new ArgumentException($"Cant find object part = '{part}'.Part cant be null or whitespace. Path == {string.Concat(parts, '.')}");
                var searchResult = result.GetPropertyValue(part);
                if (searchResult == null)
                    searchResult = result.GetFieldValue(part);
                result = searchResult;
                if (result == null)
                    throw new ArgumentException($"Cant find object '{part}'. Path == {string.Concat(parts, '.')}");
            }

            return result;
        }
        
        public static object RunMethod(
            this object targetObject,
            string methodName,
            IList<Parameter> parameters)
        {
            if (targetObject == null)
                throw new ArgumentNullException(nameof(targetObject));
            
            if (string.IsNullOrEmpty(methodName))
                throw new ArgumentNullException(nameof(methodName));
            
            parameters ??= new List<Parameter>();

            var methodInfo = targetObject.GetType().GetMethodInfo(methodName, parameters);
            var sortedParams = CalculateParameters(methodInfo, parameters);
            
            return methodInfo.Invoke(targetObject, sortedParams);
        }

        public static MethodInfo GetMethodInfo(this Type targetType, string methodName, IList<Parameter> parameters)
        {
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

            return methodInfo;
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