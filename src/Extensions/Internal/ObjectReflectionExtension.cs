using System;
using System.Linq;
using System.Reflection;
using Xamarin.Forms.Internals;

namespace Basil.Behaviors.Extensions.Internal
{
    public static class ObjectReflectionExtension
    {
        public static TProperty GetPropertyValue<TProperty>(this object target, string propertyName)
            => (TProperty) target?.GetPropertyInfo(propertyName)?.GetValue(target);
        
        public static object GetPropertyValue(this object target, string propertyName)
            => target?.GetPropertyInfo(propertyName)?.GetValue(target);
        
        public static TField GetFieldValue<TField>(this object target, string fieldName)
            => (TField) target?.GetFieldInfo(fieldName)?.GetValue(target);
        
        public static object GetFieldValue(this object target, string fieldName)
            => target?.GetFieldInfo(fieldName)?.GetValue(target);

        public static FieldInfo GetFieldInfo(this object target, string fieldName)
        {
            ValidateTargets(target, fieldName);

            return target.GetType().GetRuntimeFields().FirstOrDefault(f => f.Name == fieldName);
        }
        
        public static PropertyInfo GetPropertyInfo(this object target, string propertyName)
        {
            ValidateTargets(target, propertyName);

            return target.GetType().GetProperties().FirstOrDefault(p => p.Name == propertyName);
        }

        private static void ValidateTargets(object target, string name)
        {
            if (target == null)
                throw new ArgumentNullException(nameof(target));

            if (string.IsNullOrEmpty(name))
                throw new ArgumentNullException(nameof(name));
        }
    }
}