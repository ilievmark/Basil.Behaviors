namespace Basil.Behaviors.Extensions.Internal
{
    public static class StringExtensions
    {
        public static char LastCharacter(this string str) => str[str.Length - 1];

        public static bool ContainsChar(this string str, char c)
        {
            for (int i = 0; i < str.Length; i++)
                if (str[i] == c)
                    return true;
            return false;
        }
    }
}