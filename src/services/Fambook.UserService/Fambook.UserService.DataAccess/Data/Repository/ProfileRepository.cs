using System.Linq;
using Fambook.UserService.DataAccess.Data.Repository.IRepository;
using Fambook.UserService.Models;

namespace Fambook.UserService.DataAccess.Data.Repository
{
    public class ProfileRepository : Repository<Profile>, IProfileRepository
    {
        private readonly ApplicationDbContext _db;

        public ProfileRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Profile profile)
        {
            var objFromDb = _db.Profile.FirstOrDefault(s => s.Id == profile.Id);

            if (objFromDb != null)
            {
                objFromDb.Gender = profile.Gender;
                objFromDb.ProfilePicture = profile.ProfilePicture;
                objFromDb.Description = profile.Description;
            }

            _db.SaveChanges();
        }
    }
}
