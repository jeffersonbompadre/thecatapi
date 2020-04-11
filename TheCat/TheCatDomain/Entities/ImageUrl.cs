namespace TheCatDomain.Entities
{
    public class ImageUrl
    {
        public string ImageUrlId { get; set; }
        public string Url { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }

        public Breeds Breeds { get; set; }
        public Category Category { get; set; }
    }
}
