using System.Collections.Generic;
using System.Threading.Tasks;
using TheCatDomain.Entities;

namespace TheCatDomain.Interfaces.Repositories
{
    /// <summary>
    /// Interface que especifica o contrato que deve ser seguido para implementar os métodos
    /// necessários para o repositório Category
    /// </summary>
    public interface ICategoryRepository
    {
        Task<ICollection<Category>> GetAllCategory();
        Task<Category> GetCategory(int id);
        Task AddCategory(Category category);
        Task UpdateCategory(Category category);
    }
}
