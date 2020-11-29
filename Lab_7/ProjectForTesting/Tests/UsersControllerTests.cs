using Moq;
using NUnit.Framework;
using ProjectForTesting.Controllers;
using ProjectForTesting.DataAccess.Repositories;
using ProjectForTesting.Domain.Entities;
using ProjectForTesting.Dtos;
using ProjectForTesting.LoggerService;
using System.Threading.Tasks;

namespace Tests
{
    internal class UsersControllerTests
    {
        [Test]
        public async Task GetUserById()
        {
            var repository = new Mock<IRepository<User>>();
            var logger = new Mock<ILoggerManager>();

            var controller = new UsersController(repository.Object, logger.Object);
            await controller.GetById(1);

            repository.Verify(u => u.GetByIdAsync(1));
        }

        [Test]
        public async Task GetAllUsers()
        {
            var repository = new Mock<IRepository<User>>();
            var logger = new Mock<ILoggerManager>();

            var controller = new UsersController(repository.Object, logger.Object);
            await controller.GetAll();

            repository.Verify(u => u.GetAllAsync());
        }
        
        [Test]
        public async Task AddUser()
        {
            var repository = new Mock<IRepository<User>>();
            var logger = new Mock<ILoggerManager>();
            var userDto = new UserDto
            {
                Firstname = "Test",
                Surname = "Test",
                Account = "Test\\\\Test"
            };

            var controller = new UsersController(repository.Object, logger.Object);
            await controller.Add(userDto);

            repository.Verify(u => u.AddAsync(It.IsAny<User>()));
        }

        [Test]
        public async Task UpdateUser()
        {
            var repository = new Mock<IRepository<User>>();
            var logger = new Mock<ILoggerManager>();
            var userDto = new UserDto
            {
                Firstname = "Test",
                Surname = "Test",
                Account = "Test\\Test"
            };

            var controller = new UsersController(repository.Object, logger.Object);
            await controller.Update(It.IsAny<int>(), userDto);

            repository.Verify(u => u.AddAsync(It.IsAny<User>()));
        }

        [Test]
        public async Task DeleteUser()
        {
            var repository = new Mock<IRepository<User>>();
            var logger = new Mock<ILoggerManager>();

            var controller = new UsersController(repository.Object, logger.Object);
            await controller.Delete(1);

            repository.Verify(u => u.DeleteByIdAsync(1));
        }
    }
}