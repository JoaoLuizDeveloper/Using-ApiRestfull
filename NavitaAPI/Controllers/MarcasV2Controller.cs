using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NativaAPI.Models;
using NativaAPI.Repository.IRepository;

namespace NativaAPI.Controllers
{
    [Route("api/v{version:apiversion}/marcasV2")]
    [ApiVersion("2.0")]
    [ApiController]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public class MarcasV2Controller : ControllerBase
    {
        private readonly IMarcaRepository _npmarcas;
        private readonly IMapper _mapper;

        public MarcasV2Controller(IMarcaRepository npmarcas, IMapper mapper)
        {
            _npmarcas = npmarcas;
            _mapper = mapper;
        }

        /// <summary>
        /// Get List of Marcas
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(200, Type= typeof(List<Marca>))]
        public IActionResult GetNationalParks()
        {
            var obj = _npmarcas.GetMarcas().FirstOrDefault();

            return Ok(_mapper.Map<Marca>(obj));
        }
    }
}