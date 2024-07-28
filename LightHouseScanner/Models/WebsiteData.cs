namespace LightHouseScanner.Models
{
    public class WebsiteData
    {
        public int Id { get; set; }
        public string DomainName { get; set; } = string.Empty;
        public int Performance { get; set; }
        public int Accessibility { get; set; }
        public int BestPractices { get; set; }
        public int Seo { get; set; }
        public string FinalScreenshot { get; set; } = string.Empty;
        public int PageLoadTime { get; set; }
        public int ServerResponseTime { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedIPAddress { get; set; } = string.Empty;
        public DateTime LastScan { get; set; }
        public string LastScanIPAddress { get; set; } = string.Empty;

        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
    }
}