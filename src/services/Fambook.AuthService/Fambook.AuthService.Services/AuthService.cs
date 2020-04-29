using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Fambook.AuthService.DataAccess.Data;
using Fambook.AuthService.Models;
using Fambook.AuthService.Services.Helpers;
using Fambook.AuthService.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Fambook.AuthService.Services
{
    public class AuthService : IAuthService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly Jwt _jwtOptions;

        public AuthService(ApplicationDbContext dbContext, IOptions<Jwt> jwtOptions)
        {
            _dbContext = dbContext;
            _jwtOptions = jwtOptions.Value;
        }

        public void Create(Credentials credentials)
        {
            credentials.Password = HashPassword(credentials.Password);
            _dbContext.Credentials.Add(credentials);
            _dbContext.SaveChanges();
        }

        public CredentialsWithToken Authenticate(string email, string password)
        {
            var credentials = _dbContext.Credentials
                .FirstOrDefault(u => u.Email == email);

            // return null if user not found
            if (credentials == null)
                return null;

            try
            {
                DeHashPassword(password, credentials.Password);
            }
            catch
            {
                return null;
            }

            // authentication successful so generate jwt token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtOptions.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, credentials.Id.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);

            credentials = credentials.WithoutPassword();
            return new CredentialsWithToken{Credentials = credentials, Token = tokenHandler.WriteToken(token) };
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
