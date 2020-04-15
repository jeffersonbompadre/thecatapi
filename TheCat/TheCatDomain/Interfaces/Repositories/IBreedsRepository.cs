using System.Collections.Generic;
using System.Threading.Tasks;
using TheCatDomain.Entities;

namespace TheCatDomain.Interfaces.Repositories
{
    /// <summary>
    /// Interface que especifica o contrato que deve ser seguido para implementar os métodos
    /// necessários para o repositório Breeds
    /// </summary>
    public interface IBreedsRepository
    {
        Task<ICollection<Breeds>> GetAllBreeds(bool includeImages = false);
        Task<Breeds> GetBreeds(string idOrName, bool includeImages = false);
        Task<ICollection<Breeds>> GetBreedsByTemperament(string temperament, bool includeImages = false);
        Task<ICollection<Breeds>> GetBreedsByOrigin(string origin, bool includeImages = false);
        Task AddBreeds(Breeds breeds);
        Task UpdateBreeds(Breeds breeds);
    }
}
