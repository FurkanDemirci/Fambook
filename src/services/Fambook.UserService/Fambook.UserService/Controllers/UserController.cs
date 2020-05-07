using System;
using Fambook.UserService.DataAccess.Data.Repository.IRepository;
using Fambook.UserService.Models;
using Fambook.UserService.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Fambook.UserService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly IUserService _userService;

        public UserController(ILogger<UserController> logger, IUnitOfWork unitOfWork, IRabbitManager manager)
        {
            _logger = logger;
            _userService = new Services.UserService(unitOfWork, manager);
        }

        [HttpPost("create")]
        public IActionResult Create(UserViewModel userViewModel)
        {
            try
            {
                _userService.Create(userViewModel);
            }
            catch (Exception e)
            {
                return BadRequest(new {message = e.Message});
            }
            return Ok();
        }

        [HttpGet("{id}")]
        public IActionResult Get([FromRoute] int id)
        {
            return Ok(_userService.Get(id));
        }
    }
}
