using System.Diagnostics.CodeAnalysis;

namespace Marck7JR.Core.Extensions
{
    public static class StringExtensions
    {
        public static bool IsNotNullOrEmpty(
#if NETSTANDARD2_1 || NETCOREAPP3_0_OR_GREATER
            [NotNullWhen(true)]
#endif
            this string? @string
            ) => !@string.IsNullOrEmpty();
        public static bool IsNotNullOrWhiteSpace(
#if NETSTANDARD2_1 || NETCOREAPP3_0_OR_GREATER
            [NotNullWhen(true)]
#endif
            this string? @string
            ) => !@string.IsNullOrWhiteSpace();
        public static bool IsNullOrEmpty(
#if NETSTANDARD2_1 || NETCOREAPP3_0_OR_GREATER
            [NotNullWhen(false)]
#endif
        this string? @string
            ) => string.IsNullOrEmpty(@string);
        public static bool IsNullOrWhiteSpace(
#if NETSTANDARD2_1 || NETCOREAPP3_0_OR_GREATER
            [NotNullWhen(false)]
#endif
            this string? @string
            ) => string.IsNullOrWhiteSpace(@string);
    }
}
