using System;
using System.Collections.Generic;
using System.Text;
using Fambook.UserService.DataAccess.Data.Repository.IRepository;
using Fambook.UserService.Models;
using Fambook.UserService.Services.Interfaces;

namespace Fambook.UserService.Services
{
    public class ProfileService : IProfileService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProfileService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Profile Get(int id)
        {
            if (id == 0)
                return null;

            var profile = _unitOfWork.Profile.Get(id);
            return profile;
        }

        public void Delete(Profile profile)
        {
            throw new NotImplementedException();
        }
    }
}
