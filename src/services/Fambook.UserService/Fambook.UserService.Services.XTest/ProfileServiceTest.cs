using System;
using Fambook.UserService.DataAccess.Data.Repository.IRepository;
using Fambook.UserService.Models;
using Fambook.UserService.Services.Exceptions;
using Moq;
using Xunit;

namespace Fambook.UserService.Services.XTest
{
    public class ProfileServiceTest
    {
        private readonly Profile _profile;

        public ProfileServiceTest()
        {
            _profile = new Profile
            {
                Id = 1,
                Gender = "Male",
                ProfilePicture = null,
                Description = "Loves hentai"
            };
        }

        [Fact]
        public void Get_Profile_With_Valid_Id()
        {
            // Arrange
            var unitOfWork = new Mock<IUnitOfWork>();
            unitOfWork.Setup(x => x.Profile.Get(1)).Returns(_profile);

            var profileService = new ProfileService(unitOfWork.Object);

            // Act
            var profile = profileService.Get(1);

            // Assert
            Assert.Equal(_profile, profile);
        }

        [Fact]
        public void Get_Profile_With_Invalid_Id_Returns_Null()
        {
            // Arrange
            var unitOfWork = new Mock<IUnitOfWork>();
            unitOfWork.Setup(x => x.Profile.Get(1)).Returns(_profile);

            var profileService = new ProfileService(unitOfWork.Object);

            // Act
            var profile = profileService.Get(0);

            // Assert
            Assert.Null(profile);
        }

        [Fact]
        public void Get_Non_Existing_Profile_With_Valid_Id_Returns_Null()
        {
            // Arrange
            var unitOfWork = new Mock<IUnitOfWork>();
            unitOfWork.Setup(x => x.Profile.Get(2));

            var profileService = new ProfileService(unitOfWork.Object);

            // Act
            var profile = profileService.Get(2);

            // Assert
            Assert.Null(profile);
        }

        [Fact]
        public void Upload_ProfilePicture_With_Valid_Id()
        {
            var profile = new Profile
            {
                Id = 1,
                Gender = "Male",
                ProfilePicture = new byte[10],
                Description = "Loves hentai"
            };

            // Arrange
            var unitOfWork = new Mock<IUnitOfWork>();
            unitOfWork.Setup(x => x.Profile.Get(1)).Returns(_profile);
            unitOfWork.Setup(x => x.Profile.Update(profile));

            var profileService = new ProfileService(unitOfWork.Object);

            // Act
            profileService.Upload(1, new byte[10]);
        }

        [Fact]
        public void Upload_ProfilePicture_With_Invalid_Id()
        {
            var profile = new Profile
            {
                Id = 1,
                Gender = "Male",
                ProfilePicture = new byte[10],
                Description = "Loves hentai"
            };

            // Arrange
            var unitOfWork = new Mock<IUnitOfWork>();
            unitOfWork.Setup(x => x.Profile.Get(1)).Returns(_profile);
            unitOfWork.Setup(x => x.Profile.Update(profile));

            var profileService = new ProfileService(unitOfWork.Object);

            // Act
            Exception ex = Assert.Throws<InvalidProfileException>(() => profileService.Upload(0, new byte[10]));

            // Assert
            Assert.Equal("Invalid id given", ex.Message);
        }

        [Fact]
        public void Upload_ProfilePicture_With_valid_Id_To_Non_Existing_Profile()
        {
            // Arrange
            var unitOfWork = new Mock<IUnitOfWork>();
            unitOfWork.Setup(x => x.Profile.Get(2));

            var profileService = new ProfileService(unitOfWork.Object);

            // Act
            Exception ex = Assert.Throws<InvalidProfileException>(() => profileService.Upload(2, new byte[10]));

            // Assert
            Assert.Equal("Profile does not exist", ex.Message);
        }
    }
}
