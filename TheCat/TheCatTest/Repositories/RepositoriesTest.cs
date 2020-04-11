using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
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
        readonly IBreedsRepository breedsRepository;

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

            breedsRepository = new BreedsRepository(contextDB);
        }

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
    }
}
