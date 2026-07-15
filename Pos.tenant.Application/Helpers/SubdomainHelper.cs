using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Pos.tenant.Application.Helpers
{
    public static class SubdomainHelper
    {
        private const int MaxLength = 63;

        private static readonly HashSet<string> ReservedSubdomains = new(StringComparer.OrdinalIgnoreCase)
        {
            "www",
            "api",
            "admin",
            "app",
            "mail",
            "smtp",
            "ftp",
            "localhost",
            "dev",
            "test",
            "staging",
            "support",
            "help",
            "billing",
            "identity",
            "tenant"
        };

        public static string Normalize(string subdomain)
        {
            return subdomain.Trim().ToLowerInvariant();
        }

        public static bool IsValid(string subdomain)
        {
            if (string.IsNullOrWhiteSpace(subdomain))
                return false;

            subdomain = Normalize(subdomain);

            if (subdomain.Length > MaxLength)
                return false;

            if (ReservedSubdomains.Contains(subdomain))
                return false;

            return Regex.IsMatch(
                subdomain,
                "^[a-z0-9]([a-z0-9-]*[a-z0-9])?$"
            );
        }
    }
}
