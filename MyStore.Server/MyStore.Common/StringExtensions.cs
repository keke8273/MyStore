using System.Text.RegularExpressions;

namespace MyStore.Common
{
    public static class StringExtensions
    {
        public static string Extract(this string value, string pattern)
        {
            return Regex.Match(value, pattern).Groups[1].Value;
        }
    }
}
