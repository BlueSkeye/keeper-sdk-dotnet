using System;
using System.Diagnostics;
using System.Text;

namespace KeeperSecurity.Utils
{
    /// <exclude />
    public static class StringUtils
    {
        public static string ToSnakeCase(this string text)
        {
            var sb = new StringBuilder();
            for (var i = 0; i < text.Length; i++)
            {
                var ch = text[i];
                if (char.IsUpper(ch) && i > 0)
                {
                    sb.Append('_');
                    sb.Append(char.ToLower(ch));
                }
                else
                {
                    sb.Append(ch);
                }
            }

            return sb.ToString();
        }

        public static string StripUrl(this string url)
        {
            try
            {
                var builder = new UriBuilder(url);
                return builder.Host + builder.Path;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                return url;
            }
        }
    }
}
