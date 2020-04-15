using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TheCatDomain.Entities;
using TheCatDomain.Interfaces.Repositories;

namespace TheCatWebApp.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class BreedsController : ControllerBase
    {
        readonly IBreedsRepository breedsRepository;

        public BreedsController(IBreedsRepository breedsRepository)
        {
            this.breedsRepository = breedsRepository;
        }

        /// <summary>
        /// API Retorna todas as raças coletadas do TheCatAPI
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("listatodasracas")]
        public async Task<IEnumerable<Breeds>> GetAllBreeds()
        {
            return await breedsRepository.GetAllBreeds(true);
        }

        /// <summary>
        /// API Retorna uma raça específica, de acordo com o ID ou Nome passado como parâmetro
        /// </summary>
        /// <param name="codigoOuNome">Informar o Id ou Nome de uma raça</param>
        /// <returns></returns>
        [HttpGet]
        [Route("buscaraca")]
        public async Task<Breeds> GetBreeds(string codigoOuNome)
        {
            return await breedsRepository.GetBreeds(codigoOuNome, true);
        }

        /// <summary>
        /// API Retorna uma ou mais raças por temperamento
        /// </summary>
        /// <param name="temperamento">Informar temperamento desejado</param>
        /// <returns></returns>
        [HttpGet]
        [Route("buscaracaportemperamento")]
        public async Task<IEnumerable<Breeds>> GetBreedsByTemperament(string temperamento)
        {
            return await breedsRepository.GetBreedsByTemperament(temperamento, true);
        }

        /// <summary>
        /// API Retorna uma ou mais raças por origem
        /// </summary>
        /// <param name="origem">Informar a origem desejada</param>
        /// <returns></returns>
        [HttpGet]
        [Route("buscaracapororigem")]
        public async Task<IEnumerable<Breeds>> GetBreedsByOrigin(string origem)
        {
            return await breedsRepository.GetBreedsByOrigin(origem, true);
        }
    }
}