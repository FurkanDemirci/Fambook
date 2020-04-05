using System;
using System.Collections.Generic;
using System.Text;
using Fambook.UserService.Models;

namespace Fambook.UserService.DataAccess.Data.Repository.IRepository
{
    public interface IUserRepository : IRepository<User>
    {
        void Update(User user);
    }
}
