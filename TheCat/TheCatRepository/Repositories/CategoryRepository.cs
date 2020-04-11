using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using TheCatDomain.Entities;
using TheCatDomain.Interfaces.Repositories;
using TheCatRepository.Context;

namespace TheCatRepository.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        readonly TheCatContext theCatContext;

        public CategoryRepository(TheCatContext theCatContext)
        {
            this.theCatContext = theCatContext;
        }

        public async Task<ICollection<Category>> GetAllCategory()
        {
            return await theCatContext.Category.AsNoTracking().ToListAsync();
        }

        public async Task<Category> GetCategory(int id)
        {
            return await theCatContext.Category.FirstOrDefaultAsync(x => x.CategoryId == id);
        }

        public async Task AddCategory(Category Category)
        {
            if (!Category.IsValid())
                return;
            else
            {
                theCatContext.Add(Category);
                await theCatContext.SaveChangesAsync();
            }
        }

        public async Task UpdateCategory(Category Category)
        {
            if (!Category.IsValid())
                return;
            else
            {
                theCatContext.Entry(Category).State = EntityState.Modified;
                await theCatContext.SaveChangesAsync();
            }
        }
    }
}
