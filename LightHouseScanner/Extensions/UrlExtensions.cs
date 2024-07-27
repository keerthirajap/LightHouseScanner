namespace LightHouseScanner.Extensions
{
    using System;
    using System.Linq;

    public static class UrlExtensions
    {
        public static string ExtractDomainOrSubdomain(this Uri uri)
        {
            if (uri == null)
                throw new ArgumentNullException(nameof(uri));

            var host = uri.Host;

            // Split the host into parts
            var parts = host.Split('.');

            if (parts.Length < 2)
                throw new ArgumentException("The URL does not contain a valid domain.");

            // Extract the domain or subdomain
            if (parts.Length == 2)
            {
                // No subdomain, just the domain (e.g., example.com)
                return parts[0];
            }
            else
            {
                // Extract the subdomain (e.g., web.whatsapp.com -> web)
                return string.Join('.', parts.Take(parts.Length - 2));
            }
        }

        public static string ExtractDomainOrSubdomain(string url)
        {
            if (string.IsNullOrWhiteSpace(url))
                throw new ArgumentException("URL cannot be null or empty.", nameof(url));

            var uri = new Uri(url);
            return uri.ExtractDomainOrSubdomain();
        }
    }
}