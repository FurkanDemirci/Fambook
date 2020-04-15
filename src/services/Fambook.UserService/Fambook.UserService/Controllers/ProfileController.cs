using Fambook.UserService.DataAccess.Data.Repository.IRepository;
using Fambook.UserService.Models;
using Fambook.UserService.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Fambook.UserService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProfileController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly IProfileService _profileService;

        public ProfileController(ILogger<UserController> logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _profileService = new Services.ProfileService(unitOfWork);
        }

        [HttpGet]
        public Profile Get(int id)
        {
            return _profileService.Get(id);
        }
    }
}
