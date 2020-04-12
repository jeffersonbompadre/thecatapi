using System.Collections.Generic;

namespace TheCatDomain.Models
{
    /// <summary>
    /// Classe auxiliar relacionada na classe AppSetting para seguir o padrão do arquivo AppSettgings.json
    /// Conterá informações de acesso ao site: TheCatAPI, tanto Urls como métodos necessários.
    /// </summary>
    public class TheCatSettings
    {
        public string BaseURL { get; set; }
        public string BreedsMethod { get; set; }
        public string CategoryMethod { get; set; }
        public string ImageMethod { get; set; }
        public List<string> ImageCategoryFilter { get; set; } = new List<string>();
    }
}
