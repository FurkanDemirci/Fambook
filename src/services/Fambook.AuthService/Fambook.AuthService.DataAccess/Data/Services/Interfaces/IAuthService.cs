using System;
using System.Collections.Generic;
using System.Text;
using Fambook.AuthService.Models;

namespace Fambook.AuthService.DataAccess.Data.Services.Interfaces
{
    public interface IAuthService
    {
        Credentials GetUser(string email);
        void Create(Credentials credentials);
    }
}
