using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NavitaAPI.Models;
using NavitaAPI.Repository.IRepository;

namespace NavitaAPI.Controllers
{
    [Route("api/v{version:apiversion}/marcas")]
    [ApiVersion("2.0")]
    [ApiController]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public class MarcasV2Controller : ControllerBase
    {
        #region Construtor/Injection
        private readonly IMarcaRepository _npmarcas;
        private readonly IMapper _mapper;

        public MarcasV2Controller(IMarcaRepository npmarcas, IMapper mapper)
        {
            _npmarcas = npmarcas;
            _mapper = mapper;
        }
        #endregion

        #region Pegar Marcas
        /// <summary>
        /// Get List of Marcas
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(200, Type= typeof(List<Marca>))]
        public IActionResult GetMarcas()
        {
            var obj = _npmarcas.GetMarcas().FirstOrDefault();

            return Ok(_mapper.Map<Marca>(obj));
        }
        #endregion
    }
}