namespace Basil.Behaviors.Tests.Environment.Sample.Classes
{
    public class ClassWithPrivateProperty<TProperty>
    {
        private TProperty Property { get; set; }
        
        public ClassWithPrivateProperty(TProperty propertyValue)
        {
            Property = propertyValue;
        }
    }
}