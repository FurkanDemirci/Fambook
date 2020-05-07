using System;
using Fambook.UserService.DataAccess.Data.Repository.IRepository;
using Fambook.UserService.Models;
using Fambook.UserService.Services.Exceptions;
using Fambook.UserService.Services.Interfaces;
using Moq;
using Xunit;

namespace Fambook.UserService.Services.XUnit
{
    public class UserServiceTest
    {
        private readonly User _user;
        private readonly Credentials _credentials;

        public UserServiceTest()
        {
            _user = new User
            {
                Id = 1,
                FirstName = "Furkan",
                LastName = "Demirci",
                Birthdate = "05/03/1997",
                Profile = new Profile()
            };

            _credentials = new Credentials
            {
                Email = "Furkan.Demirci@live.nl",
                Password = "Demirci543532",
                UserId = 1
            };
        }

        [Fact]
        public void Create_With_Valid_User_Throws_No_Exception()
        {
            // Arrange
            var unitOfWork = new Mock<IUnitOfWork>();
            var rabbitManager = new Mock<IRabbitManager>();
            unitOfWork.Setup(x => x.User.Add(_user));
            unitOfWork.Setup(x => x.Save());
            rabbitManager.Setup(x =>
                x.Publish(new {_credentials}, "exchange.topic.user.create", "topic", "*.queue.user.create.#"));

            var userService = new UserService(unitOfWork.Object, rabbitManager.Object);
            var userViewModel = new UserViewModel
            {
                Id = 1,
                Email = "Furkan.Demirci@live.nl",
                Password = "Demirci543532",
                FirstName = "Furkan",
                LastName = "Demirci",
                Birthdate = "05/03/1997"
            };

            // Act
            userService.Create(userViewModel);
        }

        [Fact]
        public void Create_With_Invalid_User_Throws_InvalidUserException()
        {
            // Arrange
            var unitOfWork = new Mock<IUnitOfWork>();
            var rabbitManager = new Mock<IRabbitManager>();
            unitOfWork.Setup(x => x.User.Add(_user));
            unitOfWork.Setup(x => x.Save());
            rabbitManager.Setup(x =>
                x.Publish(new { _credentials }, "exchange.topic.user.create", "topic", "*.queue.user.create.#"));

            var userService = new UserService(unitOfWork.Object, rabbitManager.Object);
            var userViewModel = new UserViewModel
            {
                Id = 1,
                Email = null,
                Password = "Demirci543532",
                FirstName = null,
                LastName = "Demirci",
                Birthdate = "05/03/1997"
            };

            // Act
            Exception ex = Assert.Throws<InvalidUserException>(() => userService.Create(userViewModel));

            // Assert
            Assert.Equal("Not all properties are filled", ex.Message);
        }

        [Fact]
        public void Get_User_With_Valid_Id()
        {
            // Arrange
            var unitOfWork = new Mock<IUnitOfWork>();
            var rabbitManager = new Mock<IRabbitManager>();
            unitOfWork.Setup(x => x.User.GetWithProfile(1)).Returns(_user);

            var userService = new UserService(unitOfWork.Object, rabbitManager.Object);

            // Act
            var user = userService.Get(1);

            // Assert
            Assert.Equal(_user, user);
        }

        [Fact]
        public void Get_User_With_Invalid_Id_Returns_Null()
        {
            // Arrange
            var unitOfWork = new Mock<IUnitOfWork>();
            var rabbitManager = new Mock<IRabbitManager>();
            unitOfWork.Setup(x => x.User.GetWithProfile(1)).Returns(_user);

            var userService = new UserService(unitOfWork.Object, rabbitManager.Object);

            // Act
            var user = userService.Get(0);

            // Assert
            Assert.Null(user);
        }

        [Fact]
        public void Get_Non_Existing_User_With_Valid_Id_Returns_Null()
        {
            // Arrange
            var unitOfWork = new Mock<IUnitOfWork>();
            var rabbitManager = new Mock<IRabbitManager>();
            unitOfWork.Setup(x => x.User.GetWithProfile(2));

            var userService = new UserService(unitOfWork.Object, rabbitManager.Object);

            // Act
            var user = userService.Get(2);

            // Assert
            Assert.Null(user);
        }
    }
}
