using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using TheCatAPIIntegration.Service;
using TheCatDomain.Entities;
using TheCatDomain.Interfaces;
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

        readonly IAppConfiguration appConfiguration;
        readonly ITheCatAPI theCatAPI;
        readonly IELKIntegration elkIntegration;


        /// <summary>
        /// Construtor utilizado para instanciar as classes que serão testadas
        /// </summary>
        public IntegrationTest()
        {
            appConfiguration = new AppConfiguration();
            theCatAPI = new TheCatAPIService(appConfiguration);
            elkIntegration = new ELKIntegrationService(appConfiguration);
        }

        #region Testes de Integração com TheCatAPI

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

        #endregion

        #region Testes de Integração com ELK

        [Fact]
        public async Task ELKAddDocTest()
        {
            var stopWatch = new Stopwatch();
            stopWatch.Start();
            await Task.Delay(300);
            stopWatch.Stop();
            var logEvent = new LogEvent(DateTime.UtcNow, LogLevel.Debug, "ELKAddDocTest", stopWatch.ElapsedMilliseconds);
            logEvent.SetLogEventId(1);
            logEvent.SetDescription("Teste de Integração com ELK");
            await elkIntegration.AddDoc(logEvent);
        }

        #endregion
    }
}
