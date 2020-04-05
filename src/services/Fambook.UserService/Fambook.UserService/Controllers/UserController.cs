using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        public void Create(User user)
        {
            _userService.Create(user);
        }

        [HttpGet]
        public User Get(int id)
        {
            return _userService.Get(id);
        }
    }
}
