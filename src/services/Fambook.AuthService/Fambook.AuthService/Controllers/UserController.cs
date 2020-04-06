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
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private IUserService _userService;

        public UserController(ILogger<UserController> logger, IUserService userService)
        {
            _logger = logger;
            _userService = userService;
        }

        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody]AuthenticateModel model)
        {
            var userWithToken = _userService.Authenticate(model.Email, model.Password);

            if (userWithToken == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            userWithToken.User = userWithToken.User.WithoutPassword();
            return Ok(userWithToken);
        }
    }
}
