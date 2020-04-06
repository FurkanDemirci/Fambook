using System;
using System.Collections.Generic;
using System.Text;
using Fambook.AuthService.Models;

namespace Fambook.AuthService.Services.Interfaces
{
    public interface IUserService
    {
        UserWithToken Authenticate(string email, string password);
    }
}
