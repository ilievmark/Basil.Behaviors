using System;
using System.Reflection;

namespace Basil.Behaviors.Extensions
{
    public static class TypeExtension
    {
        internal static object GetDefaultValue(this Type type)
        {
            var typeInfo = type.GetTypeInfo();

            if (typeInfo.IsEnum)
                return Enum.GetValues(type).GetValue(0);
            if (typeInfo.IsValueType)
                return 0;
            
            return null;
        }
    }
}