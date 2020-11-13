using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NavitaAPI.Models;
using NavitaAPI.Repository.IRepository;

namespace NavitaAPI.Controllers
{
    [Authorize] 
    [Route("api/v{version:apiversion}/Users")]
    [ApiVersion("1.0")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        #region Construtor/Injection
        private readonly IUserRepository _userRepo;

        public UsersController(IUserRepository userRepo)
        {
            _userRepo = userRepo;
        }
        #endregion

        #region Login
        [AllowAnonymous]
        [HttpPost("Authenticate")]
        public IActionResult Authenticate ([FromBody] AuthenticationModel model)
        {
            var user = _userRepo.Authenticate(model.UserName, model.Email, model.Password);

            if(user == null)
            {
                return BadRequest(new { message = "Algo esta incorreto! Tente novamente." });
            }

            return Ok(user);
        }
        #endregion

        #region Registro
        [AllowAnonymous]
        [HttpPost("register")]
        public IActionResult Register([FromBody] AuthenticationModel model)
        {
            bool ifUserUnique = _userRepo.isUniqueUser(model.UserName);

            if (!ifUserUnique)
            {
                return BadRequest(new { message = "Usuario já existe" });
            }

            var user = _userRepo.Register(model.UserName, model.Email, model.Password);

            if(user == null)
            {
                return BadRequest(new { message = "Erro ao registrar" });
            }

            return Ok(user);
        }
        #endregion
    }
}