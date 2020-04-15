using System;
using System.Collections.Generic;
using System.Text;
using Fambook.UserService.Models;

namespace Fambook.UserService.DataAccess.Data.Repository.IRepository
{
    public interface IProfileRepository : IRepository<Profile>
    {
        void Update(Profile profile);
    }
}
