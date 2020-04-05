using System;
using System.Collections.Generic;
using System.Text;
using Fambook.UserService.DataAccess.Data.Repository.IRepository;

namespace Fambook.UserService.DataAccess.Data.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _db;

        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;

            // Repository's
            User = new UserRepository(_db);
        }

        public IUserRepository User { get; }

        public void Save()
        {
            _db.SaveChanges();
        }

        public void Dispose()
        {
            _db.Dispose();
        }
    }
}
