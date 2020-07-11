namespace Basil.Behaviors.Rules.Mask
{
    public class AnyCharMaskRule : MaskRuleBase
    {
        public override bool Verify(string substring) => substring.Length == 1;
    }
}