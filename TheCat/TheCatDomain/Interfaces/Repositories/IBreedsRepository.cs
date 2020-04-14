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
        Task<ICollection<Breeds>> GetAllBreeds();
        Task<Breeds> GetBreeds(string idOrName);
        Task<ICollection<Breeds>> GetBreedsByTemperament(string temperament);
        Task<ICollection<Breeds>> GetBreedsByOrigin(string origin);
        Task AddBreeds(Breeds breeds);
        Task UpdateBreeds(Breeds breeds);
    }
}
