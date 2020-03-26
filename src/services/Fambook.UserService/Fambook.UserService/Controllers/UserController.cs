using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Fambook.UserService.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Fambook.UserService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;

        public UserController(ILogger<UserController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public User Get()
        {
            var profile = new Profile
            {
                Id = 1,
                Gender = "Male",
                Description = "Admin user"
            };

            return new User
            {
                Id = 1,
                Email = "furkan.demirci@live.nl", 
                FirstName = "Furkan", 
                LastName = "Demirci", 
                Birthdate = "05/03/1997",
                Profile = profile
            };
        }
    }
}
