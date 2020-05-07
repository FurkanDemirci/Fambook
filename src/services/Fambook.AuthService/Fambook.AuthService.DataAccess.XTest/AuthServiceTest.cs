using Fambook.AuthService.DataAccess.Data;
using Fambook.AuthService.Models;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;

namespace Fambook.AuthService.DataAccess.XUnit
{
    public class AuthServiceTest
    {
        private readonly Credentials _credentials;

        public AuthServiceTest()
        {
            _credentials = new Credentials
            {
                Id = 1,
                Email = "Furkan.demirci@live.nl",
                Password = "Demirci543532",
                UserId = 1
            };
        }

        [Fact]
        public void Create_Credential_ProperMethodCalled()
        {
            // Arrange
            var context = new Mock<ApplicationDbContext>();
            var dbSetMock = new Mock<DbSet<Credentials>>();
            dbSetMock.Setup(x => x.Add(It.IsAny<Credentials>()));
            context.Setup(x => x.Set<Credentials>()).Returns(dbSetMock.Object);
            
            var authService = new Data.Services.AuthService(context.Object);

            // Act
            authService.Create(_credentials);

            // Assert
            context.Verify(x => x.SaveChanges(), Times.Once);
        }
    }
}
