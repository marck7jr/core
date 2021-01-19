using System;

namespace Marck7JR.Core.Extensions
{
    public static class UriExtensions
    {
        public static string FromUri(this Uri uri) => uri.AbsoluteUri;
        public static Uri ToUri(this string @string) => new(@string);
    }
}
