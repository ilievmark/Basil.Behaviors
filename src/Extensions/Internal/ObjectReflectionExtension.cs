
using System.Linq;
using System.Reflection;
using Xamarin.Forms.Internals;

namespace Basil.Behaviors.Extensions.Internal
{
    public static class ObjectReflectionExtension
    {
        public static TProperty GetPropertyValue<TProperty>(this object target, string propertyName)
            => (TProperty) target.GetType().GetProperties().FirstOrDefault(p => p.Name == propertyName)?.GetValue(target);
        
        public static object GetPropertyValue(this object target, string propertyName)
            => target.GetType().GetProperties().FirstOrDefault(p => p.Name == propertyName)?.GetValue(target);
        
        public static TField GetFieldValue<TField>(this object target, string fieldName)
            => (TField) target.GetType().GetRuntimeFields().FirstOrDefault(f => f.Name == fieldName)?.GetValue(target);
        
        public static object GetFieldValue(this object target, string fieldName)
            => target.GetType().GetRuntimeFields().FirstOrDefault(f => f.Name == fieldName)?.GetValue(target);
    }
}