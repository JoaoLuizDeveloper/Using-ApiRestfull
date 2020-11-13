using System;
using System.Collections.Generic;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NavitaAPI.Models;
using NavitaAPI.Models.DTOs;
using NavitaAPI.Repository.IRepository;

namespace NavitaAPI.Controllers
{
    [Route("api/v{version:apiversion}/patrimonios")]
    [ApiController]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public class PatrimoniosController : ControllerBase
    {
        #region Construtor/Injection
        private readonly IPatrimonioRepository _patrepo;
        private readonly IMapper _mapper;

        public PatrimoniosController(IPatrimonioRepository patrepo, IMapper mapper)
        {
            _patrepo = patrepo;
            _mapper = mapper;
        }
        #endregion

        #region Pegar Lista de Patrimonios
        /// <summary>
        /// Get List of Patrimonios
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(200, Type= typeof(List<Patrimonio>))]
        public IActionResult GetPatrimonios()
        {
            var objList = _patrepo.GetPatrimonios();
            var objDTo = new List<Patrimonio>();

            foreach (var obj in objList)
            {
                objDTo.Add(_mapper.Map<Patrimonio>(obj));
            }

            return Ok(objDTo);
        }
        #endregion

        #region Pegar Patrimonio Individual
        /// <summary>
        /// Get Individual Patrimonio
        /// </summary>
        /// <param name="id">The id of the Patrimonio</param>
        /// <returns></returns>
        [HttpGet("{id:int}", Name = "GetPatrimonio")]
        [ProducesResponseType(200, Type = typeof(Patrimonio))]
        [ProducesResponseType(404)]
        [ProducesDefaultResponseType]
        public IActionResult GetPatrimonio(int id)
        {
            var obj = _patrepo.GetPatrimonio(id);
            if (obj == null)
            {
                return NotFound();
            }

            var objDTO = _mapper.Map<Patrimonio>(obj);
            return Ok(objDTO);
        }

        #endregion

        #region Pegar Patrimonio dentro de Marcas
        /// <summary>
        /// Get Individual Patrimonio
        /// </summary>
        /// <param name="patrimonioid">The id of the PatrimonioId</param>
        /// <returns></returns>
        [HttpGet("[action]/{patrimonioid:int}")]
        [ProducesResponseType(200, Type = typeof(Patrimonio))]
        [ProducesResponseType(404)]
        [ProducesDefaultResponseType]
        public IActionResult GetPatrimonioInMarcas(int patrimonioid)
        {
            var objList = _patrepo.GetPatrimoniosInMarcas(patrimonioid);
            if (objList == null)
            {
                return NotFound();
            }

            var objDto = new List<Patrimonio>();
            foreach(var obj in objList)
            {
                objDto.Add(_mapper.Map<Patrimonio>(obj));
            }
            
            return Ok(objDto);
        }
        #endregion

        #region Criar, Atualizar e Deletar patrimonio
        [HttpPost]
        [ProducesResponseType(201, Type = typeof(Patrimonio))]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult CreatePatrimonio([FromBody] PatrimonioCreateDto patrimonio)
        {
            if (patrimonio == null)
            {
                return BadRequest(ModelState);
            }

            if (_patrepo.PatrimonioExists(patrimonio.Nome))
            {
                ModelState.AddModelError("", "Patrimonio Exist");
                return StatusCode(404, ModelState);
            }

            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var patrimoniosObj = _mapper.Map<Patrimonio>(patrimonio);

            patrimoniosObj.NumeroTombo = new Random().Next();

            if (!_patrepo.CreatePatrimonio(patrimoniosObj))
            {
                ModelState.AddModelError("",$"Algo errado ao salvar {patrimonio.Nome}");
                return StatusCode(500, ModelState);
            }

            return CreatedAtRoute("GetPatrimonio", new { version = HttpContext.GetRequestedApiVersion().ToString(), id = patrimoniosObj.Id }, patrimoniosObj);
        }

        [HttpPatch("{id:int}", Name = "UpdatePatrimonio")]
        [ProducesResponseType(204)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult UpdatePatrimonio(int id, [FromBody] PatrimonioUpdateDto patrimoniosDto)
        {
            if (patrimoniosDto == null || id != patrimoniosDto.Id)
            {
                return BadRequest(ModelState);
            }

            var patrimoniosObj = _mapper.Map<Patrimonio>(patrimoniosDto);

            if (!_patrepo.UpdatePatrimonio(patrimoniosObj))
            {
                ModelState.AddModelError("", $"Algo errado ao atualizar {patrimoniosDto.Nome}");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpDelete("{id:int}", Name = "DeletePatrimonio")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult DeletePatrimonio(int id)
        {
            if (!_patrepo.PatrimonioExists(id))
            {
                return NotFound();
            }

            var patrimoniosDto = _patrepo.GetPatrimonio(id);

            if (!_patrepo.DeletePatrimonio(patrimoniosDto))
            {
                ModelState.AddModelError("", $"Algo errado ao deletar {patrimoniosDto.Nome}");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        #endregion
    }
}