using System;
using Fambook.AuthService.DataAccess.Data.Services.Interfaces;
using Fambook.AuthService.Logic.Exceptions;
using Fambook.AuthService.Logic.Helpers;
using Fambook.AuthService.Models;
using Microsoft.Extensions.Options;
using Moq;
using Xunit;

namespace Fambook.AuthService.Logic.XUnit
{
    public class AuthLogicTest
    {
        private readonly Credentials _credentials;
        private readonly CredentialsWithToken _credentialsWithToken;

        public AuthLogicTest()
        {
            _credentials = new Credentials
            {
                Id = 1,
                Email = "Furkan.demirci@live.nl",
                Password = "g9oVkOm27ZweV7ymd8SpuZZOSZ8hHTTJgKynuE5A6Py9RJKv",
                UserId = 1
            };

            _credentialsWithToken = new CredentialsWithToken
            {
                Credentials = _credentials,
                Token = "random token"
            };
        }

        [Fact] 
        public void Create_Credentials_With_Valid_Credentials()
        {
            // Arrange
            var authService = new Mock<IAuthService>();
            var jwtOptions = new Mock<IOptions<Jwt>>();
            authService.Setup(x => x.Create(_credentials));
            
            var authLogic = new AuthLogic(authService.Object, jwtOptions.Object);

            // Act
            authLogic.CreateCredentials(_credentials);
        }

        [Fact]
        public void Create_Credentials_With_Invalid_Credentials()
        {
            // Arrange
            var authService = new Mock<IAuthService>();
            var jwtOptions = new Mock<IOptions<Jwt>>();
            authService.Setup(x => x.Create(_credentials));

            var authLogic = new AuthLogic(authService.Object, jwtOptions.Object);

            var credentials = new Credentials
            {
                Id = 1,
                Email = "",
                Password = "fdssdfsdfsdfs",
                UserId = 1
            };

            // Act
            Exception ex = Assert.Throws<InvalidCredentialsException>(() => authLogic.CreateCredentials(credentials));

            // Assert
            Assert.Equal("Invalid credentials given", ex.Message);
        }

        [Fact]
        public void Authenticate_With_Valid_Email_And_Password()
        {
            var email = "Furkan.demirci@live.nl";
            var password = "Demirci543532";

            // Arrange
            var authService = new Mock<IAuthService>();
            var jwtOptions = new Mock<IOptions<Jwt>>();
            authService.Setup(x => x.GetUser(email)).Returns(_credentials);
            jwtOptions.Setup(x => x.Value).Returns(new Jwt
            {
                Secret =
                    "THIS IS A TEST KEY USED BY FAMBOOK. PLEASE CHANGE THIS TO SOMETHING USEFULL AND HIDE IT AWAY SO ITS NOT IN CODE"
            });

            var authLogic = new AuthLogic(authService.Object, jwtOptions.Object);
            
            // Act
            var credentialsWithToken = authLogic.Authenticate(email, password);
            credentialsWithToken.Token = "random token";
            _credentialsWithToken.Credentials = _credentialsWithToken.Credentials.WithoutPassword();

            // Assert
            Assert.Equal(_credentialsWithToken.Credentials, credentialsWithToken.Credentials);
            Assert.Equal(_credentialsWithToken.Token, credentialsWithToken.Token);
        }
    }
}
