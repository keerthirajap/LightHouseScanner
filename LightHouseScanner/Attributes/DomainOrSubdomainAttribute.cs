namespace LightHouseScanner.Attributes
{
    using System.ComponentModel.DataAnnotations;
    using System.Text.RegularExpressions;

    public class DomainOrSubdomainAttribute : ValidationAttribute
    {
        private static readonly Regex UrlRegex = new Regex(
            @"^(?:http[s]?://)?(?:www\.)?(?<subdomain>[^/\.]+)\.(?<domain>[^/\.]+)\.(?<tld>[^/\.]+)$",
            RegexOptions.Compiled | RegexOptions.IgnoreCase);

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var url = value as string;

            if (string.IsNullOrWhiteSpace(url))
            {
                return new ValidationResult("URL cannot be null or empty.");
            }

            var uri = new Uri(url, UriKind.Absolute);

            if (!Uri.IsWellFormedUriString(url, UriKind.Absolute))
            {
                return new ValidationResult("Invalid URL format.");
            }

            var match = UrlRegex.Match(uri.Host);
            if (!match.Success)
            {
                return new ValidationResult("URL does not contain a valid domain or subdomain.");
            }

            return ValidationResult.Success;
        }
    }
}