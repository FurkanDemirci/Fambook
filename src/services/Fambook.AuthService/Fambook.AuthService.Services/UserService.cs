using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Fambook.AuthService;
using Fambook.AuthService.DataAccess.Data;
using Fambook.AuthService.Models;
using Fambook.AuthService.Services.Helpers;
using Fambook.AuthService.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace Fambook.AuthService.Services
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly AppSettings _appSettings;

        public UserService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
            _appSettings = new AppSettings();
        }

        public UserWithToken Authenticate(string email, string password)
        {
            var user = _dbContext.User
                .Where(u => u.Email == email)
                .Include(u => u.Profile)
                .FirstOrDefault();

            // return null if user not found
            if (user == null)
                return null;

            try
            {
                DeHashPassword(password, user.Password);
            }
            catch
            {
                return null;
            }

            // authentication successful so generate jwt token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return new UserWithToken{User = user, Token = tokenHandler.WriteToken(token) };
        }

        public void DeHashPassword(string password, string dbPassword)
        {
            byte[] hashBytes = Convert.FromBase64String(dbPassword);
            byte[] salt = new byte[16];
            Array.Copy(hashBytes, 0, salt, 0, 16);
            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 10000);
            byte[] hash = pbkdf2.GetBytes(20);
            for (int i = 0; i < 20; i++)
                if (hashBytes[i + 16] != hash[i])
                    throw new UnauthorizedAccessException();
        }
    }
}
