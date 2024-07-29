namespace LightHouseScanner.Attributes
{
    using System.ComponentModel.DataAnnotations;
    using System.Text.RegularExpressions;

    public class DomainOrSubdomainAttribute : ValidationAttribute
    {
        private static readonly Regex DomainRegex = new Regex(
            @"^(?!-)(?:[a-zA-Z0-9-]{1,63}\.)+[a-zA-Z]{2,}$",
            RegexOptions.Compiled | RegexOptions.IgnoreCase);

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var url = value as string;

            if (string.IsNullOrWhiteSpace(url))
            {
                return new ValidationResult("URL cannot be null or empty.");
            }

            // Try to create a Uri object with or without scheme
            if (!Uri.TryCreate(url, UriKind.Absolute, out var uri))
            {
                // Attempt to prepend scheme if missing and parse again
                if (!Uri.TryCreate("http://" + url, UriKind.Absolute, out uri))
                {
                    return new ValidationResult("Invalid URL format.");
                }
            }

            // Ensure it's either HTTP or HTTPS
            if (uri.Scheme != Uri.UriSchemeHttp && uri.Scheme != Uri.UriSchemeHttps)
            {
                return new ValidationResult("URL must be HTTP or HTTPS.");
            }

            // Check if the URL host matches the regex for domain or subdomain
            var host = uri.Host;

            var match = DomainRegex.Match(host);
            if (!match.Success)
            {
                return new ValidationResult("URL does not contain a valid domain or subdomain.");
            }

            return ValidationResult.Success;
        }
    }
}