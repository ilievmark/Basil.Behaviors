using Xamarin.Forms.Internals;

namespace Basil.Behaviors.Extensions.Internal
{
    public static class ObjectReflectionExtension
    {
        public static TProperty GetPropertyValue<TProperty>(this object target, string propertyName)
            => (TProperty) target.GetType().GetProperty(propertyName)?.GetValue(target);
        
        public static object GetPropertyValue(this object target, string propertyName)
            => target.GetType().GetProperty(propertyName)?.GetValue(target);
        
        public static TField GetFieldValue<TField>(this object target, string fieldName)
            => (TField) target.GetType().GetField(fieldName)?.GetValue(target);
        
        public static object GetFieldValue(this object target, string fieldName)
            => target.GetType().GetField(fieldName)?.GetValue(target);
    }
}