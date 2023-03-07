namespace mis.Core
{
    public static class StringExtensions
    {
        public static bool IsFilled(this string input) => !string.IsNullOrWhiteSpace(input);
    }
}
