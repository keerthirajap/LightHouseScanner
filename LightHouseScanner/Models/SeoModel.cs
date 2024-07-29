namespace LightHouseScanner.Models
{
    public class SeoModel
    {
        public string CanonicalURL { get; set; } = string.Empty;

        public string Title { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public string SiteName { get; set; } = string.Empty;

        public string OgImageURL { get; set; } = string.Empty;

        public string OgImageURLAlt { get; set; } = string.Empty;

        public string OgImageType { get; set; } = string.Empty;

        public string OgImageHeight { get; set; } = string.Empty;

        public string OgImageWidth { get; set; } = string.Empty;

        public string OgType { get; set; } = string.Empty;

        public DateTime CreatedDate { get; set; } = DateTime.Now;

        public DateTime ModifiedDate { get; set; } = DateTime.Now;

        public string TwitterSite { get; set; } = "@sitescan+";

        public bool AllowRobots { get; set; } = true;

        public List<string> Tags { get; set; } = new List<string>();

        public List<string> SeeAlso { get; set; } = new List<string>();
    }
}