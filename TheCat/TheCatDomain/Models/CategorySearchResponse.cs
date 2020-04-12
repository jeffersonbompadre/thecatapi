namespace TheCatDomain.Models
{
    /// <summary>
    /// Classe que será desserializada na chamada do método Categories da API TheCatAPI
    /// </summary>
    public class CategorySearchResponse
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }
}
