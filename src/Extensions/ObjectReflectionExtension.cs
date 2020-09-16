using System;
using System.Linq;
using System.Reflection;
using Xamarin.Forms.Internals;

namespace Basil.Behaviors.Extensions
{
    public static class ObjectReflectionExtension
    {
        public static bool Cast<TCastType>(this object castable, out TCastType castedValue)
            where TCastType : class
            => (castedValue = castable as TCastType) != null;
        
        public static TProperty GetPropertyValue<TProperty>(this object target, string propertyName)
            => (TProperty) target.GetPropertyValue(propertyName);
        
        public static object GetPropertyValue(this object target, string propertyName)
            => target?.GetPropertyInfo(propertyName)?.GetValue(target);

        public static TField GetFieldValue<TField>(this object target, string fieldName)
            => (TField) target.GetFieldValue(fieldName);
        
        public static object GetFieldValue(this object target, string fieldName)
            => target?.GetFieldInfo(fieldName)?.GetValue(target);

        public static FieldInfo GetFieldInfo(this object target, string fieldName)
        {
            target.ValidateObject(nameof(target));
            fieldName.ValidateString(nameof(fieldName));

            return target.GetType().GetRuntimeFields().FirstOrDefault(f => f.Name == fieldName);
        }
        
        public static PropertyInfo GetPropertyInfo(this object target, string propertyName)
        {
            target.ValidateObject(nameof(target));
            propertyName.ValidateString(nameof(propertyName));

            return target.GetType().GetProperties().FirstOrDefault(p => p.Name == propertyName);
        }

        internal static void ValidateObject(this object obj, string valueName = null)
        {
            if (obj == null)
                throw new ArgumentNullException(valueName);
        }

        internal static void ValidateString(this string str, string valueName = null)
        {
            if (string.IsNullOrEmpty(str))
                throw new ArgumentNullException(valueName);
        }
        
        internal static void ValidateMember(this MemberInfo member, string memberName)
        {
            if(member == null)
                throw new MissingMemberException(memberName);
        }
    }
}