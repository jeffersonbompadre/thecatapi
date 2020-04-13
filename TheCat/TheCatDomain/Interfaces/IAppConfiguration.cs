using TheCatDomain.Models;

namespace TheCatDomain.Interfaces
{
    public interface IAppConfiguration
    {
        AppSettings GetAppSettings();
    }
}
