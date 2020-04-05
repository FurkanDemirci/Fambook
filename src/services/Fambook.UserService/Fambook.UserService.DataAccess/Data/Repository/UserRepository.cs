using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Fambook.UserService.DataAccess.Data.Repository.IRepository;
using Fambook.UserService.Models;

namespace Fambook.UserService.DataAccess.Data.Repository
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        private readonly ApplicationDbContext _db;

        public UserRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(User user)
        {
            var objFromDb = _db.User.FirstOrDefault(s => s.Id == user.Id);

            if (objFromDb != null)
            {
                objFromDb.Email = user.Email;
                objFromDb.FirstName = user.FirstName;
                objFromDb.LastName = user.LastName;
                objFromDb.Password = user.Password;
                objFromDb.Birthdate = user.Birthdate;
            }

            _db.SaveChanges();
        }
    }
}
