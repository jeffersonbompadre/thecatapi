using System;
using System.Linq;
using System.Threading.Tasks;
using TheCatDomain.Entities;
using TheCatDomain.Interfaces.Application;
using TheCatDomain.Interfaces.Integration;
using TheCatDomain.Interfaces.Repositories;
using TheCatDomain.Models;

namespace TheCatApplication.Commands
{
    /// <summary>
    /// Classe responsável em ler informações da TheCatAPI e armazenar na base de dados
    /// </summary>
    public class CommandCapture : ICommandCapture
    {
        // Fields que serão recebidos como parâmetro no construtor (Inversão de Controle)

        readonly ITheCatAPI theCatAPI;
        readonly IBreedsRepository breedsRepository;
        readonly ICategoryRepository categoryRepository;
        readonly IImageUrlRepository imageUrlRepository;
        readonly AppSettings appSettings;

        /// <summary>
        /// Construtor recebe AppSettings para ter os filtros referente ao nome Category
        /// a serem filtrados para associar ImageUrl
        /// </summary>
        /// <param name="appSettings"></param>
        public CommandCapture(AppSettings appSettings)
        {
            this.appSettings = appSettings;
        }

        /// <summary>
        /// Construtor recebe como parâmetro a instância das classes de Integração e Repositórios
        /// </summary>
        /// <param name="theCatAPI"></param>
        /// <param name="breedsRepository"></param>
        /// <param name="categoryRepository"></param>
        /// <param name="imageUrlRepository"></param>
        public CommandCapture(ITheCatAPI theCatAPI, IBreedsRepository breedsRepository, ICategoryRepository categoryRepository, IImageUrlRepository imageUrlRepository)
        {
            this.theCatAPI = theCatAPI;
            this.breedsRepository = breedsRepository;
            this.categoryRepository = categoryRepository;
            this.imageUrlRepository = imageUrlRepository;
        }

        /// <summary>
        /// Método captura a lista Breeds da API TheCatAPI e armazena na base de dados
        /// juntamente com as imagens encontradas para cada Breeds
        /// </summary>
        /// <returns></returns>
        public async Task CapureAllBreedsWithImages()
        {
            // Captura uma lista de Breeds a partir da API TheCatAPI
            var breedsList = await theCatAPI.GetBreeds();
            // Para cada Breeds encontrado, armazena na base de dados
            foreach (var breeds in breedsList)
            {
                // Verifica se Breeds já existe na base de dados, se não existir, armazena
                var breedsInDB = await breedsRepository.GetBreeds(breeds.Id);
                if (breedsInDB == null)
                {
                    breedsInDB = new Breeds(breeds.Id, breeds.Name);
                    breedsInDB.SetOrigin(breeds.Origin);
                    breedsInDB.SetTemperament(breeds.Temperament);
                    breedsInDB.SetDescription(breeds.Description);
                    await breedsRepository.AddBreeds(breedsInDB);
                }
                // Econtra as imagens do registro Breeds atual, caso exista, armazena as imagens na base de dados
                var imagesList = await theCatAPI.GetImagesByBreeds(breeds.Id);
                if (imagesList != null && imagesList.Count > 0)
                {
                    foreach (var image in imagesList)
                    {
                        // Verifica se Image existe. Se não, cria o objeto
                        // A variável: imageExists, é para fazer o tratamento se Add ou Update
                        // pois chamando este métodos, a tabela associativa será gravada, caso
                        // a imagem não tenha existido anteriormente.
                        var imageInDB = await imageUrlRepository.GetImageUrl(image.Id);
                        var imageExists = imageInDB != null;
                        if (!imageExists)
                        {
                            imageInDB = new ImageUrl(image.Id, image.Url);
                            imageInDB.SetWidth(image.Width);
                            imageInDB.SetHeight(image.Height);
                        }
                        imageInDB.SetBreeds(breedsInDB);
                        if (!imageExists)
                            await imageUrlRepository.AddImageUrl(imageInDB);
                        else
                            await imageUrlRepository.UpdateImageUrl(imageInDB);
                    }
                }
            }
        }

        /// <summary>
        /// Método captura lista de Category, conforme os nomes definicos em AppSettings
        /// e a seguir, encontra 3 Image para cada e armazena na base de dados
        /// </summary>
        /// <returns></returns>
        public async Task CaptureImagesByCategory()
        {
            // Captura uma lista de Category da API TheCatAPI, em seguida filtra a lista
            // conforme os nomes definidos no AppSettings
            var categoryList = await theCatAPI.GetCategories();
            categoryList = categoryList.Where(x => appSettings.TheCatSettings.ImageCategoryFilter.Contains(x.Name)).ToList();
            // Para cada Category encontrado, armazena na base de dados
            foreach (var category in categoryList)
            {
                // Verifica se Category já existe na base de dados, se não existir, armazena
                var categoryInDB = await categoryRepository.GetCategory(category.Id);
                if (categoryInDB == null)
                {
                    categoryInDB = new Category(category.Id, category.Name);
                    await categoryRepository.AddCategory(categoryInDB);
                }
                // Econtra as imagens do registro Breeds atual, caso exista, armazena as imagens na base de dados
                var imagesList = await theCatAPI.GetImagesByCategory(category.Id, 3);
                if (imagesList != null && imagesList.Count > 0)
                {
                    foreach (var image in imagesList)
                    {
                        // Verifica se Image existe. Se não, cria o objeto
                        // A variável: imageExists, é para fazer o tratamento se Add ou Update
                        // pois chamando este métodos, a tabela associativa será gravada, caso
                        // a imagem não tenha existido anteriormente.
                        var imageInDB = await imageUrlRepository.GetImageUrl(image.Id);
                        var imageExists = imageInDB != null;
                        if (!imageExists)
                        {
                            imageInDB = new ImageUrl(image.Id, image.Url);
                            imageInDB.SetWidth(image.Width);
                            imageInDB.SetHeight(image.Height);
                        }
                        imageInDB.SetCategory(categoryInDB);
                        if (!imageExists)
                            await imageUrlRepository.AddImageUrl(imageInDB);
                        else
                            await imageUrlRepository.UpdateImageUrl(imageInDB);
                    }
                }
            }
        }
    }
}
