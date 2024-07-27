namespace LightHouseScanner.Extensions
{
    using System.Text.Json;

    public static class CommonExtensions
    {
        public static string GetCSSPercentageClass(string percent)
        {
            if (!int.TryParse(percent, out int percentInt))
            {
                return "";
            }

            return percentInt switch
            {
                < 35 => "black",
                < 55 => "red",
                < 80 => "orange",
                _ => "green"
            };
        }

        public static (string query, string countryCode) GetIPAddressDetails(string jsonString)
        {
            if (string.IsNullOrEmpty(jsonString))
            {
                return ("N/A", "N/A");
            }

            try
            {
                var jsonDoc = JsonDocument.Parse(jsonString);
                var query = jsonDoc.RootElement.GetProperty("query").GetString();
                var countryCode = jsonDoc.RootElement.GetProperty("countryCode").GetString();
                return (query, countryCode);
            }
            catch (Exception)
            {
                return ("N/A", "N/A");
            }
        }
    }
}