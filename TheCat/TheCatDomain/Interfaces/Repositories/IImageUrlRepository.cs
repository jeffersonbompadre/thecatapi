using System.Collections.Generic;
using System.Threading.Tasks;
using TheCatDomain.Entities;

namespace TheCatDomain.Interfaces.Repositories
{
    public interface IImageUrlRepository
    {
        Task<ICollection<ImageUrl>> GetAllImageUrl();
        Task<ImageUrl> GetImageUrl(string id);
        Task AddImageUrl(ImageUrl ImageUrl);
        Task UpdateImageUrl(ImageUrl ImageUrl);
    }
}
