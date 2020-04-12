using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using TheCatDomain.Entities;
using TheCatDomain.Interfaces.Repositories;
using TheCatRepository.Context;

namespace TheCatRepository.Repositories
{
    /// <summary>
    /// Classe que implementa a interface IImageUrlRepository, responsável por armazenar e extrair informações
    /// da base de dados para a tabela ImageUrl
    /// </summary>
    public class ImageUrlRepository : IImageUrlRepository
    {
        readonly TheCatContext theCatContext;

        /// <summary>
        /// Construtor da classe: Espera um DBContext responsável por acessar a base e que implementa os
        /// comandos de banco de dados
        /// </summary>
        /// <param name="theCatContext"></param>
        public ImageUrlRepository(TheCatContext theCatContext)
        {
            this.theCatContext = theCatContext;
        }

        /// <summary>
        /// Método traz todas as as informações da tabela ImageUrl
        /// </summary>
        /// <returns></returns>
        public async Task<ICollection<ImageUrl>> GetAllImageUrl()
        {
            return await theCatContext.ImageUrl.AsNoTracking().ToListAsync();
        }

        /// <summary>
        /// Método traz a informação da tabela ImageUrl conforme o Id informado
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ImageUrl> GetImageUrl(string id)
        {
            return await theCatContext.ImageUrl.FirstOrDefaultAsync(x => x.ImageUrlId == id);
        }

        /// <summary>
        /// Método adiciona um registro na tabela ImageUrl, caso o objeto imageUrl passado seja válido
        /// </summary>
        /// <param name="imageUrl"></param>
        /// <returns></returns>
        public async Task AddImageUrl(ImageUrl imageUrl)
        {
            if (!imageUrl.IsValid())
                return;
            else
            {
                IgnoreForeignRelation(imageUrl);
                theCatContext.Add(imageUrl);
                await theCatContext.SaveChangesAsync();
            }
        }

        /// <summary>
        /// Método atualiza um registro na tabela ImageUrl, caso o objeto imageUrl passado seja válido
        /// Também verifica se os objetos Breeds e Category estão relacionados e ignora o status de atualização deles
        /// para que o ORM não tente inserir novamente registros já existentes
        /// </summary>
        /// <param name="imageUrl"></param>
        /// <returns></returns>
        public async Task UpdateImageUrl(ImageUrl imageUrl)
        {
            if (!imageUrl.IsValid())
                return;
            else
            {
                IgnoreForeignRelation(imageUrl);
                theCatContext.Entry(imageUrl).State = EntityState.Modified;
                await theCatContext.SaveChangesAsync();
            }
        }

        /// <summary>
        /// Altera o status dos objetos Breeds e Category no traching changes do ORM para UnChanged, para que estes
        /// não sofram alteração na base de dados, sendo que é para apenas gravar na tabela ImageUrl dentro deste repositório
        /// </summary>
        /// <param name="imageUrl"></param>
        void IgnoreForeignRelation(ImageUrl imageUrl)
        {
            if (imageUrl.Breeds != null)
                theCatContext.Entry(imageUrl.Breeds).State = EntityState.Unchanged;
            if (imageUrl.Category != null)
                theCatContext.Entry(imageUrl.Category).State = EntityState.Unchanged;
        }
    }
}
