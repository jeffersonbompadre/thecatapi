using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using TheCatDomain.Entities;
using TheCatDomain.Interfaces.Repositories;

namespace TheCatWebApp.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class BreedsController : ControllerBase
    {
        readonly Stopwatch stopWatch;
        readonly IBreedsRepository breedsRepository;
        readonly ILogger<BreedsController> logger;
        readonly IWebHostEnvironment env;

        public BreedsController(IBreedsRepository breedsRepository, ILogger<BreedsController> logger, IWebHostEnvironment env)
        {
            stopWatch = new Stopwatch();
            this.breedsRepository = breedsRepository;
            this.logger = logger;
            this.env = env;
        }

        /// <summary>
        /// API Retorna todas as raças coletadas do TheCatAPI
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("listatodasracas")]
        //public async Task<IEnumerable<Breeds>> GetAllBreeds()
        public async Task<IActionResult> GetAllBreeds()
        {
            try
            {
                stopWatch.Restart();
                stopWatch.Start();
                var result = await breedsRepository.GetAllBreeds(true);
                stopWatch.Stop();
                logger.LogInformation((int)LogLevel.Information, $"Encontrados {result.Count} raças;{stopWatch.ElapsedMilliseconds}");
                if (env.IsDevelopment())
                    logger.LogDebug((int)LogLevel.Debug, $"Encontrados {result.Count} raças;{stopWatch.ElapsedMilliseconds}");
                return Ok(result);
            }
            catch (Exception ex)
            {
                logger.LogError((int)LogLevel.Error, $"Erro ao buscar raças: {ex.Message}");
                return BadRequest();
            }
        }

        /// <summary>
        /// API Retorna uma raça específica, de acordo com o ID ou Nome passado como parâmetro
        /// </summary>
        /// <param name="codigoOuNome">Informar o Id ou Nome de uma raça</param>
        /// <returns></returns>
        [HttpGet]
        [Route("buscaraca")]
        public async Task<IActionResult> GetBreeds(string codigoOuNome)
        {
            try
            {
                stopWatch.Restart();
                stopWatch.Start();
                var result = await breedsRepository.GetBreeds(codigoOuNome, true);
                stopWatch.Stop();
                var msg = result != null ? $"Raça {result?.Name} encontrada" : $"Raça pesquisada por: {codigoOuNome} não encontrada";
                logger.LogInformation((int)LogLevel.Information, $"{msg};{stopWatch.ElapsedMilliseconds}");
                if (env.IsDevelopment())
                    logger.LogDebug((int)LogLevel.Debug, $"{msg};{stopWatch.ElapsedMilliseconds}");
                return Ok(result);
            }
            catch (Exception ex)
            {
                logger.LogError((int)LogLevel.Error, $"Erro ao buscar raça por {codigoOuNome}: {ex.Message}");
                return BadRequest();
            }
        }

        /// <summary>
        /// API Retorna uma ou mais raças por temperamento
        /// </summary>
        /// <param name="temperamento">Informar temperamento desejado</param>
        /// <returns></returns>
        [HttpGet]
        [Route("buscaracaportemperamento")]
        public async Task<IActionResult> GetBreedsByTemperament(string temperamento)
        {
            try
            {
                stopWatch.Restart();
                stopWatch.Start();
                var result = await breedsRepository.GetBreedsByTemperament(temperamento, true);
                stopWatch.Stop();
                var msg = result != null ? $"Encontrados {result.Count} raça(s) para temperamento {temperamento}" : $"Raças pesquisadas por temperamento: {temperamento} não encontradas";
                logger.LogInformation((int)LogLevel.Information, $"{msg};{stopWatch.ElapsedMilliseconds}");
                if (env.IsDevelopment())
                    logger.LogDebug((int)LogLevel.Debug, $"{msg};{stopWatch.ElapsedMilliseconds}");
                return Ok(result);
            }
            catch (Exception ex)
            {
                logger.LogError((int)LogLevel.Error, $"Erro ao buscar raça por temperamento {temperamento}: {ex.Message}");
                return BadRequest();
            }
        }

        /// <summary>
        /// API Retorna uma ou mais raças por origem
        /// </summary>
        /// <param name="origem">Informar a origem desejada</param>
        /// <returns></returns>
        [HttpGet]
        [Route("buscaracapororigem")]
        public async Task<IActionResult> GetBreedsByOrigin(string origem)
        {
            try
            {
                stopWatch.Restart();
                stopWatch.Start();
                var result = await breedsRepository.GetBreedsByOrigin(origem, true);
                stopWatch.Stop();
                var msg = result != null ? $"Encontrados {result.Count} raça(s) para origem {origem}" : $"Raças pesquisadas por origem: {origem} não encontradas";
                logger.LogInformation((int)LogLevel.Information, $"{msg};{stopWatch.ElapsedMilliseconds}");
                if (env.IsDevelopment())
                    logger.LogDebug((int)LogLevel.Debug, $"{msg};{stopWatch.ElapsedMilliseconds}");
                return Ok(result);
            }
            catch (Exception ex)
            {
                logger.LogError((int)LogLevel.Error, $"Erro ao buscar raça por origem {origem}: {ex.Message}");
                return BadRequest();
            }
        }
    }
}