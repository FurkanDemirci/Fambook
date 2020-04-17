using System;
using System.Collections.Generic;
using System.Text;
using Fambook.UserService.Models;

namespace Fambook.UserService.Services.Interfaces
{
    public interface IProfileService
    {
        Profile Get(int id);
        void Delete(Profile profile);
        void Upload(int id, byte[] picture);
    }
}
