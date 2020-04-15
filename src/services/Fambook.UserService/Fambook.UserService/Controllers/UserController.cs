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

        public UserController(ILogger<UserController> logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _userService = new Services.UserService(unitOfWork);
        }

        [HttpPost("create")]
        public IActionResult Create(User user)
        {
            _userService.Create(user);
            return Ok();
        }

        [HttpGet]
        public IActionResult Get(int id)
        {
            return Ok(_userService.Get(id));
        }
    }
}
