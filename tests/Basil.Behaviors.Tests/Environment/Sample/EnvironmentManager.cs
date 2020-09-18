using Basil.Behaviors.Tests.Environment.Sample.Classes;

namespace Basil.Behaviors.Tests.Environment.Sample
{
    public class EnvironmentManager
    {
        public static ClassWithField<TField> CreateInstanceWithField<TField>(TField fieldValue)
            => new ClassWithField<TField>(fieldValue);
        
        public static ClassWithPrivateField<TField> CreateInstanceWithPrivateField<TField>(TField fieldValue)
            => new ClassWithPrivateField<TField>(fieldValue);
        
        public static ClassWithProperty<TProperty> CreateInstanceWithProperty<TProperty>(TProperty propertyValue)
            => new ClassWithProperty<TProperty>(propertyValue);
        
        public static ClassWithPrivateProperty<TProperty> CreateInstanceWithPrivateProperty<TProperty>(TProperty propertyValue)
            => new ClassWithPrivateProperty<TProperty>(propertyValue);
    }
}