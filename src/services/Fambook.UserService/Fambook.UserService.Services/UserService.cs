using System;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using Fambook.UserService.DataAccess.Data.Repository.IRepository;
using Fambook.UserService.Models;
using Fambook.UserService.Services.Exceptions;
using Fambook.UserService.Services.Helpers;
using Fambook.UserService.Services.Interfaces;
using Newtonsoft.Json;
using RabbitMQ.Client;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace Fambook.UserService.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private IRabbitManager _manager;

        public UserService(IUnitOfWork unitOfWork, IRabbitManager manager)
        {
            _unitOfWork = unitOfWork;
            _manager = manager;
        }

        public void Create(UserViewModel userViewModel)
        {
            if (userViewModel.Email == null || userViewModel.Password == null || userViewModel.FirstName == null ||
                userViewModel.LastName == null ||
                userViewModel.Birthdate == null) throw new InvalidUserException("Not all properties are filled");

            var user = new User
            {
                FirstName = userViewModel.FirstName,
                LastName = userViewModel.LastName,
                Birthdate = userViewModel.Birthdate,
                Profile = new Profile()
            };

            _unitOfWork.User.Add(user);
            _unitOfWork.Save();

            var credentials = new Credentials
            {
                Email = userViewModel.Email,
                Password = userViewModel.Password,
                UserId = user.Id
            };

//            var serializedObject = JsonSerializer.Serialize(credentials);

//            _manager.Publish(new
//            {
//                credentials
//            }, "exchange.topic.user.create", "topic", "*.queue.user.create.#");
        }

        public User Get(int id)
        {
            if (id == 0)
                return null;

            var user = _unitOfWork.User.GetWithProfile(id);

            if (user == null)
                return null;

            user.Profile.ProfilePicture = null;
            return user;
        }

        public void Delete(User user)
        {
            throw new NotImplementedException();
        }
    }
}
