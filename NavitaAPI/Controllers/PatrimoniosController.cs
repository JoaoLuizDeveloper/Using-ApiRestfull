using System.Collections.Generic;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NativaAPI.Models;
using NativaAPI.Repository.IRepository;

namespace NativaAPI.Controllers
{
    //[Route("api/Patrimonios")]
    [Route("api/v{version:apiversion}/trails")]
    [ApiController]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public class PatrimoniosController : ControllerBase
    {
        private readonly IPatrimonioRepository _patrepo;
        private readonly IMapper _mapper;

        public PatrimoniosController(IPatrimonioRepository patrepo, IMapper mapper)
        {
            _patrepo = patrepo;
            _mapper = mapper;
        }

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

        /// <summary>
        /// Get Individual Patrimonio
        /// </summary>
        /// <param name="id">The id of the Patrimonio</param>
        /// <returns></returns>
        [HttpGet("{id:int}", Name = "GetPatrimonio")]
        [ProducesResponseType(200, Type = typeof(Patrimonio))]
        [ProducesResponseType(404)]
        [ProducesDefaultResponseType]
        [Authorize(Roles = "Admin")]
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

        [HttpPost]
        [ProducesResponseType(201, Type = typeof(Patrimonio))]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult CreatePatrimonio([FromBody] Patrimonio patrimonio)
        {
            if (patrimonio == null)
            {
                return BadRequest(ModelState);
            }

            if(_patrepo.PatrimonioExists(patrimonio.Nome))
            {
                ModelState.AddModelError("", "Patrimonio Exist");
                return StatusCode(404, ModelState);
            }

            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var patrimoniosObj = _mapper.Map<Patrimonio>(patrimonio);

            if (!_patrepo.CreatePatrimonio(patrimoniosObj))
            {
                ModelState.AddModelError("",$"Algo errado ao salvar {patrimonio.Nome}");
                return StatusCode(500, ModelState);
            }

            return CreatedAtRoute("GetPatrimonio", new { id= patrimoniosObj.Id }, patrimoniosObj);
        }

        [HttpPatch("{id:int}", Name = "UpdatePatrimonio")]
        [ProducesResponseType(204)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult UpdatePatrimonio(int id, [FromBody] Patrimonio patrimoniosDto)
        {
            if (patrimoniosDto == null || id != patrimoniosDto.Id)
            {
                return BadRequest(ModelState);
            }

            var trailsObj = _mapper.Map<Patrimonio>(patrimoniosDto);

            if (!_patrepo.UpdatePatrimonio(trailsObj))
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
    }
}