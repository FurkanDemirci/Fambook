using System;
using System.Collections.Generic;
using System.Text;
using Fambook.AuthService.Models;

namespace Fambook.AuthService.Logic.Interfaces
{
    public interface IAuthLogic
    {
        void CreateCredentials(Credentials credentials);
        CredentialsWithToken Authenticate(string email, string password);
        string HashPassword(string password);
        void DeHashPassword(string password, string dbPassword);
    }
}
