using System.Collections.Generic;
using System.Threading.Tasks;
using TheCatDomain.Entities;

namespace TheCatDomain.Interfaces.Repositories
{
    /// <summary>
    /// Interface que especifica o contrato que deve ser seguido para implementar os métodos
    /// necessários para o repositório ImageUrl
    /// </summary>
    public interface IImageUrlRepository
    {
        Task<ICollection<ImageUrl>> GetAllImageUrl();
        Task<ImageUrl> GetImageUrl(string id);
        Task<ICollection<ImageUrl>> GetImageUrlByCategory(int id);
        Task<ICollection<ImageUrl>> GetImageUrlByBreeds(string id);
        Task AddImageUrl(ImageUrl imageUrl);
        Task UpdateImageUrl(ImageUrl imageUrl);
    }
}
