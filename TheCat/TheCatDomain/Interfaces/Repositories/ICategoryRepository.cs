using System.Collections.Generic;
using System.Threading.Tasks;
using TheCatDomain.Entities;

namespace TheCatDomain.Interfaces.Repositories
{
    public interface ICategoryRepository
    {
        Task<ICollection<Category>> GetAllCategory();
        Task<Category> GetCategory(int id);
        Task AddCategory(Category category);
        Task UpdateCategory(Category category);
    }
}
