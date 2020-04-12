using System.Collections.Generic;
using System.Threading.Tasks;
using TheCatDomain.Models;

namespace TheCatDomain.Interfaces.Integration
{
    /// <summary>
    /// Interface que especifica o contrato que deve ser seguido para implementar os métodos
    /// necessários para coletar informações da TheCatAPI
    /// </summary>
    public interface ITheCatAPI
    {
        Task<ICollection<BreedsSearchResponse>> GetBreeds();
        Task<ICollection<CategorySearchResponse>> GetCategories();
        Task<ICollection<ImageSearchResponse>> GetImagesByCategory(int categoryId, int limitImages = 4);
        Task<ICollection<ImageSearchResponse>> GetImagesByBreeds(string breedsId, int limitImages = 3);
    }
}
