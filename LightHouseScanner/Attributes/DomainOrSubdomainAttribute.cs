namespace LightHouseScanner.Attributes
{
    using System.ComponentModel.DataAnnotations;
    using System.Text.RegularExpressions;

    public class DomainOrSubdomainAttribute : ValidationAttribute
    {
        private static readonly Regex DomainRegex = new Regex(
            @"^(?:[a-zA-Z0-9-]+\.)+[a-zA-Z]{2,}$",
            RegexOptions.Compiled | RegexOptions.IgnoreCase);

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var url = value as string;

            if (string.IsNullOrWhiteSpace(url))
            {
                return new ValidationResult("URL cannot be null or empty.");
            }

            // Create a Uri object to ensure it's a well-formed URL
            if (!Uri.TryCreate(url, UriKind.Absolute, out var uri) || uri.Scheme != Uri.UriSchemeHttp && uri.Scheme != Uri.UriSchemeHttps)
            {
                return new ValidationResult("Invalid URL format.");
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