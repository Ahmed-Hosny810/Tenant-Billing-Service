using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Pos.tenant.Application.Helpers
{
    public static class SlugHelper
    {
        public static string Generate(string source, int maxLength = 80)
        {
            if (string.IsNullOrWhiteSpace(source))
                return string.Empty;

            source = source.ToLowerInvariant().Trim();

            var sb = new StringBuilder();

            foreach (var c in source)
            {
                if ((c >= 'a' && c <= 'z') || char.IsDigit(c))
                {
                    sb.Append(c);
                }
                else
                {
                    sb.Append('-');
                }
            }

            var slug = Regex.Replace(sb.ToString(), "-+", "-").Trim('-');

            if (slug.Length > maxLength)
                slug = slug.Substring(0, maxLength).Trim('-');

            return slug;
        }
    }
}
