namespace Basil.Behaviors.Tests.Environment.Sample.Classes
{
    public class ClassWithProperty<TProperty>
    {
        public TProperty Property { get; set; }
        
        public ClassWithProperty(TProperty propertyValue)
        {
            Property = propertyValue;
        }
    }
}