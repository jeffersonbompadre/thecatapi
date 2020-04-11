using System.Collections.Generic;
using System.Threading.Tasks;
using TheCatDomain.Entities;

namespace TheCatDomain.Interfaces.Repositories
{
    public interface IBreedsRepository
    {
        Task<ICollection<Breeds>> GetAllBreeds();
        Task<Breeds> GetBreeds(string id);
        Task AddBreeds(Breeds breeds);
        Task UpdateBreeds(Breeds breeds);
    }
}
