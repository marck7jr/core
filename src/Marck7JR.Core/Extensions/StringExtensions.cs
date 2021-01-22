namespace Marck7JR.Core.Extensions
{
    public static class StringExtensions
    {
        public static bool IsNullOrEmpty(this string? @string) => string.IsNullOrEmpty(@string);
        public static bool IsNullOrWhiteSpace(this string? @string) => string.IsNullOrWhiteSpace(@string);
    }
}
