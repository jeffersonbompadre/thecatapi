using System.Linq;
using TheCatAPIIntegration.Service;
using TheCatDomain.Interfaces.Integration;
using TheCatDomain.Models;
using Xunit;

namespace TheCatTest.Integration
{
    /// <summary>
    /// Classe resonsável por realizar os testes dos métodos implementados no projeto de integração com a API TheCatAPI
    /// </summary>
    public class IntegrationTest
    {
        // Fields que deverão ser instanciadas no construtor da classe de teste

        readonly ITheCatAPI theCatAPI;

        /// <summary>
        /// Construtor utilizado para instanciar as classes que serão testadas
        /// </summary>
        public IntegrationTest()
        {
            var appSettings = AppConfiguration.GetAppSettings();
            theCatAPI = new TheCatAPIService(appSettings);
        }

        /// <summary>
        /// Realiza teste da chamada ao método Breeds da API TheCatAPI retornando todas inforações encontradas
        /// </summary>
        [Fact]
        public async void GetBreedsTest()
        {
            var result = await theCatAPI.GetBreeds();
            Assert.NotNull(result);
        }

        /// <summary>
        /// Realiza teste da chamada ao método Categories da API TheCatAPI retornando todas inforações encontradas
        /// </summary>
        [Fact]
        public async void GetCategoriesTest()
        {
            var result = await theCatAPI.GetCategories();
            Assert.NotNull(result);
        }

        /// <summary>
        /// Realiza teste da chamada ao método Images/Search da API TheCatAPI retornando todas inforações encontradas
        /// Filtrando pelo Id Category
        /// </summary>
        [Fact]
        public async void GetImagesByCategoryTest()
        {
            var resultCategories = await theCatAPI.GetCategories();
            var result = await theCatAPI.GetImagesByCategory(resultCategories.FirstOrDefault().Id);
            Assert.NotNull(result);
        }

        /// <summary>
        /// Realiza teste da chamada ao método Images/Search da API TheCatAPI retornando todas inforações encontradas
        /// Filtrando pelo Id Breeds
        /// </summary>
        [Fact]
        public async void GetImagesByBreedsTest()
        {
            var resultBreeds = await theCatAPI.GetBreeds();
            var result = await theCatAPI.GetImagesByBreeds(resultBreeds.FirstOrDefault().Id);
            Assert.NotNull(result);
        }
    }
}
