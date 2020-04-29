using System;
using System.Collections.Generic;
using System.Linq;
using Fambook.UserService.DataAccess.Data.Repository;
using Fambook.UserService.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Moq;
using Xunit;

namespace Fambook.UserService.DataAccess.XTest
{
    public class UserRepoTest
    {
        [Fact]
        public void Add_UserObjectPassed_ProperMethodCalled()
        {
            // Arrange
            var userTest = new User();

            var context = new Mock<DbContext>();
            var dbSetMock = new Mock<DbSet<User>>();
            context.Setup(x => x.Set<User>()).Returns(dbSetMock.Object);
            dbSetMock.Setup(x => x.Add(It.IsAny<User>()));

            // Act
            var repository = new Repository<User>(context.Object);
            repository.Add(userTest);

            //Assert
            context.Verify(x => x.Set<User>());
            dbSetMock.Verify(x => x.Add(It.Is<User>(y => y == userTest)));
        }

        [Fact]
        public void Remove_UserObjectPassed_ProperMethodCalled()
        {
            // Arrange
            var userTest = new User();

            var context = new Mock<DbContext>();
            var dbSetMock = new Mock<DbSet<User>>();
            context.Setup(x => x.Set<User>()).Returns(dbSetMock.Object);
            dbSetMock.Setup(x => x.Remove(It.IsAny<User>()));

            // Act
            var repository = new Repository<User>(context.Object);
            repository.Remove(userTest);

            //Assert
            context.Verify(x => x.Set<User>());
            dbSetMock.Verify(x => x.Remove(It.Is<User>(y => y == userTest)));
        }

        [Fact]
        public void Get_UserObjectPassed_ProperMethodCalled()
        {
            // Arrange
            var userTest = new User();

            var context = new Mock<DbContext>();
            var dbSetMock = new Mock<DbSet<User>>();
            context.Setup(x => x.Set<User>()).Returns(dbSetMock.Object);
            dbSetMock.Setup(x => x.Find(It.IsAny<int>()));

            // Act
            var repository = new Repository<User>(context.Object);
            repository.Get(1);

            //Assert
            context.Verify(x => x.Set<User>());
            dbSetMock.Verify(x => x.Find(It.IsAny<int>()));
        }

        [Fact]
        public void GetAll_UserObjectPassed_ProperMethodCalled()
        {
            // Arrange
            var userTest = new User() {Id = 1};
            var userTestList = new List<User>() {userTest};

            var dbSetMock = new Mock<DbSet<User>>();
            dbSetMock.As<IQueryable<User>>().Setup(x => x.Provider).Returns(userTestList.AsQueryable().Provider);
            dbSetMock.As<IQueryable<User>>().Setup(x => x.Expression).Returns(userTestList.AsQueryable().Expression);
            dbSetMock.As<IQueryable<User>>().Setup(x => x.ElementType).Returns(userTestList.AsQueryable().ElementType);
            dbSetMock.As<IQueryable<User>>().Setup(x => x.GetEnumerator()).Returns(userTestList.AsQueryable().GetEnumerator());

            var context = new Mock<DbContext>();
            context.Setup(x => x.Set<User>()).Returns(dbSetMock.Object);

            // Act
            var repository = new Repository<User>(context.Object);
            var result = repository.GetAll();

            //Assert
            Assert.Equal(userTestList, result.ToList());
        }

        [Fact]
        public void GetFirstOrDefault_UserObjectPassed_ProperMethodCalled()
        {
            // Arrange
            var userTest = new User() { Id = 1 };
            var userTestList = new List<User>() { userTest };

            var dbSetMock = new Mock<DbSet<User>>();
            dbSetMock.As<IQueryable<User>>().Setup(x => x.Provider).Returns(userTestList.AsQueryable().Provider);
            dbSetMock.As<IQueryable<User>>().Setup(x => x.Expression).Returns(userTestList.AsQueryable().Expression);
            dbSetMock.As<IQueryable<User>>().Setup(x => x.ElementType).Returns(userTestList.AsQueryable().ElementType);
            dbSetMock.As<IQueryable<User>>().Setup(x => x.GetEnumerator()).Returns(userTestList.AsQueryable().GetEnumerator());

            var context = new Mock<DbContext>();
            context.Setup(x => x.Set<User>()).Returns(dbSetMock.Object);

            // Act
            var repository = new Repository<User>(context.Object);
            var result = repository.GetFirstOrDefault();

            //Assert
            Assert.Equal(userTest, result);
        }
    }
}
