using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Fambook.AuthService.DataAccess.Data.Services.Interfaces;
using Fambook.AuthService.Models;
using Microsoft.Extensions.Options;

namespace Fambook.AuthService.DataAccess.Data.Services
{
    public class AuthService : IAuthService
    {
        private readonly ApplicationDbContext _dbContext;

        public AuthService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Credentials GetUser(string email)
        {
            return _dbContext.Credentials.FirstOrDefault(c => c.Email == email);
        }

        public void Create(Credentials credentials)
        {
            _dbContext.Credentials.Add(credentials);
            _dbContext.SaveChanges();
        }
    }
}
