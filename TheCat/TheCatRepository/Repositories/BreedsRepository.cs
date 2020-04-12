using Dapper;
using System;
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
        // Comandos base para serem concatenados
        const string queryBase =
            @"SELECT BreedsId, Name, Origin, Temperament, Description
              FROM breeds";

        readonly TheCatDBContext theCatContext;

        /// <summary>
        /// Construtor da classe: Espera um DBContext responsável por acessar a base e que implementa os
        /// comandos de banco de dados
        /// </summary>
        /// <param name="theCatContext"></param>
        public BreedsRepository(TheCatDBContext theCatContext)
        {
            this.theCatContext = theCatContext;
        }

        /// <summary>
        /// Método traz todas as as informações da tabela Breeds
        /// </summary>
        /// <returns></returns>
        public async Task<ICollection<Breeds>> GetAllBreeds()
        {
            using (var conn = theCatContext.GetConnection)
            {
                var result = await conn.QueryAsync<Breeds>(queryBase);
                return result.ToList();
            }
        }

        /// <summary>
        /// Método traz a informação da tabela Breeds conforme o Id informado
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Breeds> GetBreeds(string id)
        {
            using (var conn = theCatContext.GetConnection)
            {
                var result = await conn.QueryAsync<Breeds>($"{queryBase} WHERE BreedsId = '{id}'");
                return result.FirstOrDefault();
            }
        }

        /// <summary>
        /// Método traz todas as informações da tabela Breed que contenham o Temperamento passado
        /// </summary>
        /// <param name="temperament"></param>
        /// <returns></returns>
        public async Task<ICollection<Breeds>> GetBreedsByTemperament(string temperament)
        {
            using (var conn = theCatContext.GetConnection)
            {
                var result = await conn.QueryAsync<Breeds>($"{queryBase} WHERE Temperament like '%{temperament}%'");
                return result.ToList();
            }
        }

        /// <summary>
        /// Método traz todas as informações da tabela Breed que contenham a Origem passada
        /// </summary>
        /// <param name="origin"></param>
        /// <returns></returns>
        public async Task<ICollection<Breeds>> GetBreedsByOrigin(string origin)
        {
            using (var conn = theCatContext.GetConnection)
            {
                var result = await conn.QueryAsync<Breeds>($"{queryBase} WHERE Origin like '%{origin}%'");
                return result.ToList();
            }
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
                var sqlCommand =
                    @"INSERT INTO breeds 
                        (BreedsId, Name, Origin, Temperament, Description) 
                      VALUES
                        (@BreedsId, @Name, @Origin, @Temperament, @Description)";
                using (var conn = theCatContext.GetConnection)
                {
                    await conn.ExecuteAsync(sqlCommand, breeds);
                }
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
                var sqlCommand =
                    @"UPDATE breeds SET 
                        Name = @Name
                        , Origin = @Origin
                        , Temperament = @Temperament
                        , Description = @Description
                    WHERE BreedsId = @BreedsId";
                using (var conn = theCatContext.GetConnection)
                {
                    await conn.ExecuteAsync(sqlCommand, breeds);
                }
            }
        }
    }
}
