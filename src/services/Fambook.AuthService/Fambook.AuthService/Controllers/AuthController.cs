using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Fambook.AuthService.Logic.Interfaces;
using Fambook.AuthService.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Fambook.AuthService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly ILogger<AuthController> _logger;
        private readonly IAuthLogic _authLogic;

        public AuthController(ILogger<AuthController> logger, IAuthLogic authLogic)
        {
            _logger = logger;
            _authLogic = authLogic;
        }

        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody]AuthenticateModel model)
        {
            CredentialsWithToken userWithToken;
            try
            {
                userWithToken = _authLogic.Authenticate(model.Email, model.Password);
            }
            catch (Exception e)
            {
                return BadRequest(new {message = e.Message});
            }
            return Ok(userWithToken);
        }
    }
}
