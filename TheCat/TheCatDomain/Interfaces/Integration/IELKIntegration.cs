using System.Threading.Tasks;
using TheCatDomain.Entities;

namespace TheCatDomain.Interfaces.Integration
{
    public interface IELKIntegration
    {
        Task CreateIndex();
        Task AddDoc(LogEvent logEvent);
    }
}
