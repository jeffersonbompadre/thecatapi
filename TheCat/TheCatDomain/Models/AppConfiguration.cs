using Newtonsoft.Json;
using System;
using System.IO;

namespace TheCatDomain.Models
{
    /// <summary>
    /// Classe statica que lê a informação do arquivo: AppSettings.json e desserializa na classe AppSettings
    /// Como o arquivo AppSettings.json requer que seja copiado no BUILD, cada projeto que utilizar o domínio
    /// conterá este arquivo e poderá utilizar esta classe de configuração.
    /// </summary>
    public static class AppConfiguration
    {
        public static AppSettings GetAppSettings()
        {
            string CONFIG_FILE_PATH = Path.Combine(Environment.CurrentDirectory, "Data", "appsettings.json");
            return JsonConvert.DeserializeObject<AppSettings>(File.ReadAllText(CONFIG_FILE_PATH));
        }

    }
}
