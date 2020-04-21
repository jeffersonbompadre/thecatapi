namespace TheCatDomain.Models
{
    /// <summary>
    /// Classe auxiliar que será gerada a partir do AppConfiguration, que desserializa o arquivo AppSetting.json
    /// nesta classe.
    /// </summary>
    public class AppSettings
    {
        public string ConnectionString { get; set; }

        public TheCatSettings TheCatSettings { get; set; }
        public ELKSettings ELKSettings { get; set; }
    }
}
