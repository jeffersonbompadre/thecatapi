using System.Collections.Generic;
using System.Threading.Tasks;
using TheCatDomain.Models;

namespace TheCatDomain.Interfaces.Integration
{
    public interface ITheCatAPI
    {
        Task<ICollection<BreedsSearchResponse>> GetBreeds();
        Task<ICollection<CategorySearchResponse>> GetCategories();
        Task<ICollection<ImageSearchResponse>> GetImagesByCategory(string categoryId, int limitImages = 4);
        Task<ICollection<ImageSearchResponse>> GetImagesByBreeds(string breedsId, int limitImages = 3);
    }
}
