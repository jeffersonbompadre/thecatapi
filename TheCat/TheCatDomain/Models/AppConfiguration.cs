using Newtonsoft.Json;
using System;
using System.IO;

namespace TheCatDomain.Models
{
    public static class AppConfiguration
    {
        public static AppSettings GetAppSettings()
        {
            string CONFIG_FILE_PATH = Path.Combine(Environment.CurrentDirectory, "Data", "appsettings.json");
            return JsonConvert.DeserializeObject<AppSettings>(File.ReadAllText(CONFIG_FILE_PATH));
        }

    }
}
