using Microsoft.AspNetCore.Mvc;
using ProjectForTesting.DataAccess.Repositories;
using ProjectForTesting.Domain.Entities;
using ProjectForTesting.Domain.ValueObjects;
using ProjectForTesting.Dtos;
using ProjectForTesting.LoggerService;
using System.Threading.Tasks;

namespace ProjectForTesting.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly ILoggerManager _logger;
        private readonly IRepository<User> _repository;

        public UsersController(IRepository<User> repository, ILoggerManager logger)
        {
            _repository = repository;
            _logger = logger;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            _logger.LogInfo($"Get user by id '{id}'");
            return Ok(await _repository.GetByIdAsync(id));
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            _logger.LogInfo("Get all users");
            return Ok(await _repository.GetAllAsync());
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] UserDto userDto)
        {
            var user = new User
            {
                Firstname = userDto.Firstname,
                Surname = userDto.Surname,
                Account = AdAccount.For(userDto.Account)
            };

            _logger.LogInfo($"Add user with firstname {userDto.Firstname}");

            await _repository.AddAsync(user);

            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UserDto userDto)
        {
            var user = new User
            {
                Firstname = userDto.Firstname,
                Surname = userDto.Surname,
                Account = AdAccount.For(userDto.Account)
            };

            _logger.LogInfo($"Update user with id '{id}'");

            await _repository.AddAsync(user);

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            _logger.LogInfo($"Delete user with id '{id}'");

            await _repository.DeleteByIdAsync(id);

            return Ok();
        }
    }
}