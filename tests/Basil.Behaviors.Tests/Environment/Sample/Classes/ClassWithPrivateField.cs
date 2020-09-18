namespace Basil.Behaviors.Tests.Environment.Sample.Classes
{
    public class ClassWithPrivateField<TField>
    {
        private TField Field;

        public ClassWithPrivateField(TField fieldValue)
        {
            Field = fieldValue;
        }
    }
}