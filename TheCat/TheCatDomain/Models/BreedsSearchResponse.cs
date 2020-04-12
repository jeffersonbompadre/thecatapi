namespace TheCatDomain.Models
{
    /// <summary>
    /// Classe que será desserializada na chamada do método Breeds da API TheCatAPI
    /// </summary>
    public class BreedsSearchResponse
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Origin { get; set; }
        public string Temperament { get; set; }
        public string Description { get; set; }
    }
}
