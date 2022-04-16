namespace Basil.Behaviors.Core
{
    public class CommandParams
    {
        public class ValidationResultArgs<TProperty>
        {
            public ValidationResultArgs(TProperty value, bool valid)
            {
                Value = value;
                Valid = valid;
            }

            public TProperty Value { get; }
            public bool Valid { get; }
        }
    }
}