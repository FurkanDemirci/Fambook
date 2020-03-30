using System;
using System.Collections.Generic;
using System.Text;

namespace Fambook.MessageService.DataAccess.Data.Repository.IRepository
{
    public interface IUnitOfWork : IDisposable
    {
        // Repository interfaces:

        void Save();
    }
}
