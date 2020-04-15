using System;
using System.Collections.Generic;
using System.Text;

namespace Fambook.UserService.DataAccess.Data.Repository.IRepository
{
    public interface IUnitOfWork : IDisposable
    {
        // Repository interfaces:
        IUserRepository User { get; }
        IProfileRepository Profile { get; }

        void Save();
    }
}
