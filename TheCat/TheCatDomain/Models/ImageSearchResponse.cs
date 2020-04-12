namespace TheCatDomain.Models
{
    /// <summary>
    /// Classe que será desserializada na chamada do método Images/Search da API TheCatAPI
    /// </summary>
    public class ImageSearchResponse
    {
        public string Id { get; set; }
        public string Url { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
    }
}
