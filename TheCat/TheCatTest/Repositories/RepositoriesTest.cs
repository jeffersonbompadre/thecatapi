using System;
using System.Threading.Tasks;
using TheCatDomain.Entities;
using TheCatDomain.Interfaces;
using TheCatDomain.Interfaces.Repositories;
using TheCatDomain.Models;
using TheCatRepository.Context;
using TheCatRepository.Repositories;
using Xunit;

namespace TheCatTest.Repositories
{
    /// <summary>
    /// Classe resonsável por realizar os testes dos métodos implementados no projeto Repositórios
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class RepositoriesTest : Attribute
    {
        // Fields que deverão ser instanciadas no construtor da classe de teste

        readonly Breeds breedsBase;
        readonly Category categoryBase;
        readonly ImageUrl imageUrlBase;
        readonly IAppConfiguration appConfiguration;
        readonly IBreedsRepository breedsRepository;
        readonly ICategoryRepository categoryRepository;
        readonly IImageUrlRepository imageUrlRepository;

        /// <summary>
        /// Construtor utilizado para instanciar as classes que serão testadas
        /// </summary>
        public RepositoriesTest()
        {
            appConfiguration = new AppConfiguration();
            var contextDB = new TheCatDBContext(appConfiguration);

            breedsBase = new Breeds("abys", "Abyssinian");
            breedsBase.SetOrigin("Egypt");
            breedsBase.SetTemperament("Active, Energetic, Independent, Intelligent, Gentle");
            breedsBase.SetDescription("The Abyssinian is easy to care for, and a joy to have in your home. They’re affectionate cats and love both people and other animals.");

            categoryBase = new Category(1, "hats");

            imageUrlBase = new ImageUrl("393", "https://cdn2.thecatapi.com/images/393.jpg");
            imageUrlBase.SetWidth(1024);
            imageUrlBase.SetHeight(654);
            imageUrlBase.SetBreeds(breedsBase);
            imageUrlBase.SetCategory(categoryBase);

            breedsRepository = new BreedsRepository(contextDB);
            categoryRepository = new CategoryRepository(contextDB);
            imageUrlRepository = new ImageUrlRepository(contextDB);
        }

        #region Breeds Test

        /// <summary>
        /// Realiza teste para trazer todos os registros da tabela Breeds.
        /// Sempre executa a chamada do método Add, para garantir que existe pelo
        /// menos um registro na tabela.
        /// Caso o resultado seja nulo, o teste falhará
        /// </summary>
        [Fact]
        public async void BreedsGetAllTest()
        {
            await AddBreeds();
            var result = await breedsRepository.GetAllBreeds();
            Assert.NotNull(result);
        }

        /// <summary>
        /// Realiza teste para trazer um registro da tabela Breeds através do Id.
        /// Sempre executa a chamada do método Add, para garantir que existe pelo
        /// menos um registro na tabela.
        /// Caso o resultado seja nulo, o teste falhará
        /// </summary>
        [Fact]
        public async void BreedsGetBreedsTest()
        {
            await AddBreeds();
            var result = await breedsRepository.GetBreeds(breedsBase.BreedsId);
            Assert.NotNull(result);
        }

        /// <summary>
        /// Realiza teste para trazer um ou mais registros da tabela Breeds através da informação Temperamento.
        /// Como é um campo que grava temperamentos separados por ",", verifica se contém a informação
        /// Sempre executa a chamada do método Add, para garantir que existe pelo
        /// menos um registro na tabela.
        /// Caso o resultado seja nulo, o teste falhará
        /// </summary>
        /// <param name="temperament"></param>
        [Theory]
        [InlineData("Active")]
        [InlineData("Energetic")]
        [InlineData("Independent")]
        [InlineData("Intelligent")]
        [InlineData("Gentle")]
        public async void BreedsGetByTemperamentTest(string temperament)
        {
            await AddBreeds();
            var result = await breedsRepository.GetBreedsByTemperament(temperament);
            Assert.NotNull(result);
        }

        /// <summary>
        /// Realiza teste para trazer um ou mais registros da tabela Breeds através da informação Origem.
        /// Sempre executa a chamada do método Add, para garantir que existe pelo
        /// menos um registro na tabela.
        /// Caso o resultado seja nulo, o teste falhará
        /// </summary>
        /// <param name="origin"></param>
        [Theory]
        [InlineData("Egypt")]
        public async void BreedsGetByOriginTest(string origin)
        {
            await AddBreeds();
            var result = await breedsRepository.GetBreedsByOrigin(origin);
            Assert.NotNull(result);
        }

        /// <summary>
        /// Garante que existirá pelo menos um registro na tabela, para que os testes de get sejam executados
        /// </summary>
        /// <returns></returns>
        async Task AddBreeds()
        {
            var breedsInDB = await breedsRepository.GetBreeds(breedsBase.BreedsId);
            if (breedsInDB == null)
                await breedsRepository.AddBreeds(breedsBase);
            else
            {
                breedsInDB.SetName(breedsBase.Name);
                breedsInDB.SetOrigin(breedsBase.Origin);
                breedsInDB.SetTemperament(breedsBase.Temperament);
                breedsInDB.SetDescription(breedsBase.Description);
                await breedsRepository.UpdateBreeds(breedsInDB);
            }
        }

        #endregion

        #region Category Test

        /// <summary>
        /// Realiza teste para trazer todos os registros da tabela Category.
        /// Sempre executa a chamada do método Add, para garantir que existe pelo
        /// menos um registro na tabela.
        /// Caso o resultado seja nulo, o teste falhará
        /// </summary>
        [Fact]
        public async void CategoryGetAllTest()
        {
            await AddCategory();
            var result = await categoryRepository.GetAllCategory();
            Assert.NotNull(result);
        }

        /// <summary>
        /// Realiza teste para trazer um registro da tabela Category através do Id.
        /// Sempre executa a chamada do método Add, para garantir que existe pelo
        /// menos um registro na tabela.
        /// Caso o resultado seja nulo, o teste falhará
        /// </summary>
        [Fact]
        public async void CategoryGetCategoryTest()
        {
            await AddCategory();
            var result = await categoryRepository.GetCategory(categoryBase.CategoryId);
            Assert.NotNull(result);
        }

        /// <summary>
        /// Garante que existirá pelo menos um registro na tabela, para que os testes de get sejam executados
        /// </summary>
        /// <returns></returns>
        async Task AddCategory()
        {
            var categoryInDB = await categoryRepository.GetCategory(categoryBase.CategoryId);
            if (categoryInDB == null)
                await categoryRepository.AddCategory(categoryBase);
            else
            {
                categoryInDB.SetName(categoryBase.Name);
                await categoryRepository.UpdateCategory(categoryInDB);
            }
        }

        #endregion

        #region Image Test

        /// <summary>
        /// Realiza teste para trazer todos os registros da tabela ImageUrl.
        /// Sempre executa a chamada do método Add, para garantir que existe pelo
        /// menos um registro na tabela.
        /// Caso o resultado seja nulo, o teste falhará
        /// </summary>
        [Fact]
        public async void ImageUrlGetAllTest()
        {
            await AddImageUrl();
            var result = await imageUrlRepository.GetAllImageUrl();
            Assert.NotNull(result);
        }

        /// <summary>
        /// Realiza teste para trazer um registro da tabela ImageUrl através do Id.
        /// Sempre executa a chamada do método Add, para garantir que existe pelo
        /// menos um registro na tabela.
        /// Caso o resultado seja nulo, o teste falhará
        /// </summary>
        [Fact]
        public async void ImageUrlGetCategoryTest()
        {
            await AddImageUrl();
            var result = await imageUrlRepository.GetImageUrl(imageUrlBase.ImageUrlId);
            Assert.NotNull(result);
        }

        /// <summary>
        /// Garante que existirá pelo menos um registro na tabela, para que os testes de get sejam executados
        /// Este método relaciona as informações de Breeds e Category, apesar das informações não serem
        /// obrigatórias na tabela
        /// </summary>
        /// <returns></returns>
        async Task AddImageUrl()
        {
            var imageUrlInDB = await imageUrlRepository.GetImageUrl(imageUrlBase.ImageUrlId);
            if (imageUrlInDB == null)
                await imageUrlRepository.AddImageUrl(imageUrlBase);
            else
            {
                imageUrlInDB.SetUrl(imageUrlBase.Url);
                imageUrlInDB.SetWidth(imageUrlBase.Width);
                imageUrlInDB.SetHeight(imageUrlBase.Height);
                imageUrlInDB.SetBreeds(breedsBase);
                imageUrlInDB.SetCategory(categoryBase);
                await imageUrlRepository.UpdateImageUrl(imageUrlInDB);
            }
        }

        #endregion
    }
}
