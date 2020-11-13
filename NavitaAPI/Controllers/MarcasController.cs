using System.Collections.Generic;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NavitaAPI.Models;
using NavitaAPI.Repository.IRepository;

namespace NavitaAPI.Controllers
{
    [Route("api/v{version:apiversion}/marcas")]
    [ApiVersion("1.0")]
    [ApiController]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public class MarcasController : ControllerBase
    {
        private readonly IMarcaRepository _npmarcas;
        private readonly IMapper _mapper;

        public MarcasController(IMarcaRepository npmarcas, IMapper mapper)
        {
            _npmarcas = npmarcas;
            _mapper = mapper;
        }

        #region Pegar Lista de Marcas
        /// <summary>
        /// Get List of Marcas
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(200, Type= typeof(List<Marca>))]
        public IActionResult GetMarcas()
        {
            var objList = _npmarcas.GetMarcas();
            var objDTo = new List<Marca>();

            foreach (var obj in objList)
            {
                objDTo.Add(_mapper.Map<Marca>(obj));
            }

            return Ok(objDTo);
        }
        #endregion

        #region Pegar Marca Individual
        /// <summary>
        /// Get Individual Marcas
        /// </summary>
        /// <param name="id">The id of the Marcas</param>
        /// <returns></returns>
        [HttpGet("{id:int}", Name = "GetMarca")]
        [ProducesResponseType(200, Type = typeof(Marca))]
        [ProducesResponseType(404)]
        [Authorize]
        [ProducesDefaultResponseType]
        public IActionResult GetMarca(int id)
        {
            var obj = _npmarcas.GetMarca(id);
            if (obj == null)
            {
                return NotFound();
            }

            var objDTO = _mapper.Map<Marca>(obj);
            return Ok(objDTO);
        }
        #endregion

        #region Criar, Atualizar e Deletar Marca
        /// <summary>
        /// CriarMarca
        /// </summary>
        /// <param name="marcas">Criação de Marcas</param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(201, Type = typeof(Marca))]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult CriarMarca([FromBody] Marca marcas)
        {
            if (marcas == null)
            {
                return BadRequest(ModelState);
            }

            if(_npmarcas.MarcaExists(marcas.Nome))
            {
                ModelState.AddModelError("", "Marcas Exist");
                return StatusCode(404, ModelState);
            }

            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var marcaObj = _mapper.Map<Marca>(marcas);

            if (!_npmarcas.CreateMarca(marcaObj))
            {
                ModelState.AddModelError("",$"Algo errado ao salvar {marcaObj.Nome}");
                return StatusCode(500, ModelState);
            }

            return CreatedAtRoute("GetMarca", new { version=HttpContext.GetRequestedApiVersion().ToString(), id= marcaObj.Id }, marcaObj);
        }

        [HttpPatch("{id:int}", Name = "UpdateMarca")]
        [ProducesResponseType(204)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult UpdateMarca(int id, [FromBody] Marca marcas)
        {
            if (marcas == null || id != marcas.Id)
            {
                return BadRequest(ModelState);
            }

            var marcaObj = _mapper.Map<Marca>(marcas);

            if (!_npmarcas.UpdateMarca(marcaObj))
            {
                ModelState.AddModelError("", $"Algo de errado ao atualizar {marcaObj.Nome}");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpDelete("{id:int}", Name = "DeleteMarca")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult DeleteMarca(int id)
        {
            if (!_npmarcas.MarcaExists(id))
            {
                return NotFound();
            }

            var marcaObj = _npmarcas.GetMarca(id);

            if (!_npmarcas.DeleteMarca(marcaObj))
            {
                ModelState.AddModelError("", $"Algo de errado ao deletar {marcaObj.Nome}");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }
        #endregion

    }
}