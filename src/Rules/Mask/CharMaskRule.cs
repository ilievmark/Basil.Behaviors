namespace Basil.Behaviors.Rules.Mask
{
    public class CharMaskRule : MaskRuleBase
    {
        public override bool Verify(string substring)
            => base.Verify(substring) && substring.Length == 1;
    }
}