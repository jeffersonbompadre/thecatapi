using System.Threading.Tasks;
using TheCatAPIIntegration.Service;
using TheCatApplication.Commands;
using TheCatDomain.Interfaces;
using TheCatDomain.Interfaces.Application;
using TheCatDomain.Interfaces.Integration;
using TheCatDomain.Interfaces.Repositories;
using TheCatDomain.Models;
using TheCatRepository.Context;
using TheCatRepository.Repositories;
using Xunit;

namespace TheCatTest.Application
{
    /// <summary>
    /// Classe responsável por realizar os testes dos métodos implementados no projeto Application.
    /// </summary>
    public class ApplicationTest
    {
        // Fields que deverão ser instanciadas no construtor da classe de teste

        readonly IAppConfiguration appConfiguration;
        readonly TheCatDBContext theCatDBContext;
        readonly ITheCatAPI theCatAPI;
        readonly IBreedsRepository breedsRepository;
        readonly ICategoryRepository categoryRepository;
        readonly IImageUrlRepository imageUrlRepository;
        readonly ICommandCapture commandCapture;

        /// <summary>
        /// Construtor utilizado para instaciar as classes que serão testadas
        /// </summary>
        public ApplicationTest()
        {
            appConfiguration = new AppConfiguration();
            theCatDBContext = new TheCatDBContext(appConfiguration);
            theCatAPI = new TheCatAPIService(appConfiguration);
            imageUrlRepository = new ImageUrlRepository(theCatDBContext);
            breedsRepository = new BreedsRepository(theCatDBContext, imageUrlRepository);
            categoryRepository = new CategoryRepository(theCatDBContext);
            commandCapture = new CommandCapture(appConfiguration, theCatAPI, breedsRepository, categoryRepository, imageUrlRepository);
        }

        /// <summary>
        /// Realiza teste da captura Breeds com 3 imagens, caso exista
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task CapureAllBreedsWithImagesTest()
        {
            await commandCapture.CapureAllBreedsWithImages();
        }

        /// <summary>
        /// Realiza teste da captura 3 Imagens de Chapéu e Óculos
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task CaptureImagesByCategoryTest()
        {
            await commandCapture.CaptureImagesByCategory();
        }
    }
}
