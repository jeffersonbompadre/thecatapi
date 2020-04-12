using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheCatDomain.Entities;
using TheCatDomain.Interfaces.Repositories;
using TheCatRepository.Context;

namespace TheCatRepository.Repositories
{
    /// <summary>
    /// Classe que implementa a interface IBreedsRepository, responsável por armazenar e extrair informações
    /// da base de dados para a tabela Breeds
    /// </summary>
    public class BreedsRepository : IBreedsRepository
    {
        readonly TheCatContext theCatContext;

        /// <summary>
        /// Construtor da classe: Espera um DBContext responsável por acessar a base e que implementa os
        /// comandos de banco de dados
        /// </summary>
        /// <param name="theCatContext"></param>
        public BreedsRepository(TheCatContext theCatContext)
        {
            this.theCatContext = theCatContext;
        }


        /// <summary>
        /// Método traz todas as as informações da tabela Breeds
        /// </summary>
        /// <returns></returns>
        public async Task<ICollection<Breeds>> GetAllBreeds()
        {
            return await theCatContext.Breeds.AsNoTracking().ToListAsync();
        }

        /// <summary>
        /// Método traz a informação da tabela Breeds conforme o Id informado
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Breeds> GetBreeds(string id)
        {
            return await theCatContext.Breeds.FirstOrDefaultAsync(x => x.BreedsId == id);
        }

        /// <summary>
        /// Método traz todas as informações da tabela Breed que contenham o Temperamento passado
        /// </summary>
        /// <param name="temperament"></param>
        /// <returns></returns>
        public async Task<ICollection<Breeds>> GetBreedsByTemperament(string temperament)
        {
            return await theCatContext.Breeds
                .Where(x => x.Temperament.Contains(temperament))
                .AsNoTracking()
                .ToListAsync();
        }


        /// <summary>
        /// Método traz todas as informações da tabela Breed que contenham a Origem passada
        /// </summary>
        /// <param name="origin"></param>
        /// <returns></returns>
        public async Task<ICollection<Breeds>> GetBreedsByOrigin(string origin)
        {
            return await theCatContext.Breeds
                .Where(x => x.Origin.Contains(origin))
                .AsNoTracking()
                .ToListAsync();
        }

        /// <summary>
        /// Método adiciona um registro na tabela Breeds, caso o objeto breeds passado seja válido
        /// </summary>
        /// <param name="breeds"></param>
        /// <returns></returns>
        public async Task AddBreeds(Breeds breeds)
        {
            if (!breeds.IsValid())
                return;
            else
            {
                theCatContext.Add(breeds);
                await theCatContext.SaveChangesAsync();
            }
        }

        /// <summary>
        /// Método atualiza um registro na tabela Breeds, caso o objeto breeds passado seja válido
        /// </summary>
        /// <param name="breeds"></param>
        /// <returns></returns>
        public async Task UpdateBreeds(Breeds breeds)
        {
            if (!breeds.IsValid())
                return;
            else
            {
                theCatContext.Entry(breeds).State = EntityState.Modified;
                await theCatContext.SaveChangesAsync();
            }
        }
    }
}
