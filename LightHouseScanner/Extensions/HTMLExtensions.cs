namespace LightHouseScanner.Extensions
{
    public static class HTMLExtensions
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
    }
}