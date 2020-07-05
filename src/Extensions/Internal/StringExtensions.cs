namespace Basil.Behaviors.Extensions.Internal
{
    public static class StringExtensions
    {
        public static char LastCharacter(this string str) => str[str.Length - 1];
    }
}