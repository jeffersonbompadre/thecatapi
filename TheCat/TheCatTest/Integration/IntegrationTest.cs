using System.Linq;
using TheCatAPIIntegration.Service;
using TheCatDomain.Interfaces.Integration;
using TheCatDomain.Models;
using Xunit;

namespace TheCatTest.Integration
{
    public class IntegrationTest
    {
        readonly ITheCatAPI theCatAPI;

        public IntegrationTest()
        {
            var appSettings = AppConfiguration.GetAppSettings();
            theCatAPI = new TheCatAPIService(appSettings);
        }

        [Fact]
        public async void GetBreedsTest()
        {
            var result = await theCatAPI.GetBreeds();
            Assert.NotNull(result);
        }

        [Fact]
        public async void GetCategoriesTest()
        {
            var result = await theCatAPI.GetCategories();
            Assert.NotNull(result);
        }

        [Fact]
        public async void GetImagesByCategoryTest()
        {
            var resultCategories = await theCatAPI.GetCategories();
            var result = await theCatAPI.GetImagesByCategory(resultCategories.FirstOrDefault().Id);
            Assert.NotNull(result);
        }

        [Fact]
        public async void GetImagesByBreedsTest()
        {
            var resultBreeds = await theCatAPI.GetBreeds();
            var result = await theCatAPI.GetImagesByBreeds(resultBreeds.FirstOrDefault().Id);
            Assert.NotNull(result);
        }
    }
}
