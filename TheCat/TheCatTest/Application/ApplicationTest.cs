﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TheCatAPIIntegration.Service;
using TheCatApplication.Commands;
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
            var appSettings = AppConfiguration.GetAppSettings();
            var contextDB = new TheCatDBContext(appSettings);

            theCatAPI = new TheCatAPIService(appSettings);
            breedsRepository = new BreedsRepository(contextDB);
            categoryRepository = new CategoryRepository(contextDB);
            imageUrlRepository = new ImageUrlRepository(contextDB);
            commandCapture = new CommandCapture(appSettings, theCatAPI, breedsRepository, categoryRepository, imageUrlRepository);
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
