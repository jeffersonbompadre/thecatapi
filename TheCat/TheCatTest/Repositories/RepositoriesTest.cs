using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using TheCatDomain.Entities;
using TheCatDomain.Interfaces.Repositories;
using TheCatDomain.Models;
using TheCatRepository.Context;
using TheCatRepository.Repositories;
using Xunit;

namespace TheCatTest.Repositories
{
    public class RepositoriesTest
    {
        readonly Breeds breedsBase;
        readonly Category categoryBase;
        readonly IBreedsRepository breedsRepository;
        readonly ICategoryRepository categoryRepository;

        public RepositoriesTest()
        {
            var appSettings = AppConfiguration.GetAppSettings();
            var optionsBuilder = new DbContextOptionsBuilder<TheCatContext>();
            optionsBuilder.UseSqlServer(appSettings.ConnectionString, providerOptions => providerOptions.CommandTimeout(60));
            var contextDB = new TheCatContext(optionsBuilder.Options);

            breedsBase = new Breeds("abys", "Abyssinian")
            {
                Origin = "Egypt",
                Temperament = "Active, Energetic, Independent, Intelligent, Gentle",
                Description = "The Abyssinian is easy to care for, and a joy to have in your home. They’re affectionate cats and love both people and other animals."
            };

            categoryBase = new Category(1, "hats");

            breedsRepository = new BreedsRepository(contextDB);
            categoryRepository = new CategoryRepository(contextDB);
        }

        #region Breeds Test

        [Fact]
        public async void BreedsGetAllTest()
        {
            await AddBreeds();
            var result = await breedsRepository.GetAllBreeds();
            Assert.NotNull(result);
        }

        [Fact]
        public async void BreedsGetBreedsTest()
        {
            await AddBreeds();
            var result = await breedsRepository.GetBreeds(breedsBase.BreedsId);
            Assert.NotNull(result);
        }

        async Task AddBreeds()
        {
            var breedsInDB = await breedsRepository.GetBreeds(breedsBase.BreedsId);
            if (breedsInDB == null)
                await breedsRepository.AddBreeds(breedsBase);
            else
            {
                breedsInDB.Name = breedsBase.Name;
                breedsInDB.Origin = breedsBase.Origin;
                breedsInDB.Temperament = breedsBase.Temperament;
                breedsInDB.Description = breedsBase.Description;
                await breedsRepository.UpdateBreeds(breedsInDB);
            }
        }

        #endregion

        #region Category Test

        [Fact]
        public async void CategoryGetAllTest()
        {
            await AddCategory();
            var result = await categoryRepository.GetAllCategory();
            Assert.NotNull(result);
        }

        [Fact]
        public async void CategoryGetCategoryTest()
        {
            await AddCategory();
            var result = await categoryRepository.GetCategory(categoryBase.CategoryId);
            Assert.NotNull(result);
        }

        async Task AddCategory()
        {
            var categoryInDB = await categoryRepository.GetCategory(categoryBase.CategoryId);
            if (categoryInDB == null)
                await categoryRepository.AddCategory(categoryBase);
            else
            {
                categoryInDB.Name = categoryBase.Name;
                await categoryRepository.UpdateCategory(categoryInDB);
            }
        }

        #endregion
    }
}
