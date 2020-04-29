using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Fambook.AuthService.Models;
using Fambook.AuthService.Services.Helpers;
using Fambook.AuthService.Services.Interfaces;
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
        private readonly IAuthService _authService;

        public AuthController(ILogger<AuthController> logger, IAuthService authService)
        {
            _logger = logger;
            _authService = authService;
        }

        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody]AuthenticateModel model)
        {
            var userWithToken = _authService.Authenticate(model.Email, model.Password);

            if (userWithToken == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            return Ok(userWithToken);
        }
    }
}
