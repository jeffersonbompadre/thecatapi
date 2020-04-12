using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using TheCatDomain.Entities;
using TheCatDomain.Interfaces.Repositories;
using TheCatRepository.Context;

namespace TheCatRepository.Repositories
{
    /// <summary>
    /// Classe que implementa a interface ICategoryRepository, responsável por armazenar e extrair informações
    /// da base de dados para a tabela Category
    /// </summary>
    public class CategoryRepository : ICategoryRepository
    {
        readonly TheCatContext theCatContext;

        /// <summary>
        /// Construtor da classe: Espera um DBContext responsável por acessar a base e que implementa os
        /// comandos de banco de dados
        /// </summary>
        /// <param name="theCatContext"></param>
        public CategoryRepository(TheCatContext theCatContext)
        {
            this.theCatContext = theCatContext;
        }

        /// <summary>
        /// Método traz todas as as informações da tabela Category
        /// </summary>
        /// <returns></returns>
        public async Task<ICollection<Category>> GetAllCategory()
        {
            return await theCatContext.Category.AsNoTracking().ToListAsync();
        }

        /// <summary>
        /// Método traz a informação da tabela Category conforme o Id informado
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Category> GetCategory(int id)
        {
            return await theCatContext.Category.FirstOrDefaultAsync(x => x.CategoryId == id);
        }

        /// <summary>
        /// Método adiciona um registro na tabela Category, caso o objeto category passado seja válido
        /// </summary>
        /// <param name="category"></param>
        /// <returns></returns>
        public async Task AddCategory(Category category)
        {
            if (!category.IsValid())
                return;
            else
            {
                theCatContext.Add(category);
                await theCatContext.SaveChangesAsync();
            }
        }

        /// <summary>
        /// Método atualiza um registro na tabela Category, caso o objeto category passado seja válido
        /// </summary>
        /// <param name="category"></param>
        /// <returns></returns>
        public async Task UpdateCategory(Category category)
        {
            if (!category.IsValid())
                return;
            else
            {
                theCatContext.Entry(category).State = EntityState.Modified;
                await theCatContext.SaveChangesAsync();
            }
        }
    }
}
