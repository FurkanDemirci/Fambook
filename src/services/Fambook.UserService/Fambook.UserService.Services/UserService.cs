using System;
using System.Security.Cryptography;
using Fambook.UserService.DataAccess.Data.Repository.IRepository;
using Fambook.UserService.Models;
using Fambook.UserService.Services.Helpers;
using Fambook.UserService.Services.Interfaces;

namespace Fambook.UserService.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void Create(User user)
        {
            if (user.Email == null || user.Password == null || user.FirstName == null || user.LastName == null ||
                user.Birthdate == null) return;

            user.Password = HashPassword(user.Password);
            user.Profile = new Profile();

            _unitOfWork.User.Add(user);
            _unitOfWork.Save();
        }

        public User Get(int id)
        {
            if (id == 0)
                return null;

            var user = _unitOfWork.User.GetWithProfile(id);
            return user == null ? null : user.WithoutPassword();
        }

        public void Delete(User user)
        {
            throw new NotImplementedException();
        }

        private string HashPassword(string password)
        {
            byte[] salt;
            new RNGCryptoServiceProvider().GetBytes(salt = new byte[16]);
            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 10000);
            byte[] hash = pbkdf2.GetBytes(20);
            byte[] hashBytes = new byte[36];
            Array.Copy(salt, 0, hashBytes, 0, 16);
            Array.Copy(hash, 0, hashBytes, 16, 20);
            return Convert.ToBase64String(hashBytes);
        }
    }
}
