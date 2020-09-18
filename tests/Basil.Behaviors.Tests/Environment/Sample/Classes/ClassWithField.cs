namespace Basil.Behaviors.Tests.Environment.Sample.Classes
{
    public class ClassWithField<TField>
    {
        public TField Field;

        public ClassWithField(TField fieldValue)
        {
            Field = fieldValue;
        }
    }
}