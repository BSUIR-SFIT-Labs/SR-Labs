using Microsoft.AspNetCore.Mvc;
using ProjectForTesting.DataAccess;
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
        private readonly IUnitOfWork _unitOfWork;

        public UsersController(IUnitOfWork unitOfWork, ILoggerManager logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            _logger.LogInfo($"Get user by id '{id}'");
            return Ok(await _unitOfWork.Users.GetByIdAsync(id));
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            _logger.LogInfo("Get all users");
            return Ok(await _unitOfWork.Users.GetAllAsync());
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

            await _unitOfWork.Users.AddAsync(user);
            await _unitOfWork.CommitAsync();

            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UserDto userDto)
        {
            var user = await _unitOfWork.Users.GetByIdAsync(id);

            user.Firstname = userDto.Firstname;
            user.Surname = userDto.Surname;
            user.Account = AdAccount.For(userDto.Account);

            _logger.LogInfo($"Update user with id '{id}'");

            _unitOfWork.Users.Update(user);
            await _unitOfWork.CommitAsync();

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Add(int id)
        {
            _logger.LogInfo($"Delete user with id '{id}'");

            await _unitOfWork.Users.DeleteByIdAsync(id);
            await _unitOfWork.CommitAsync();

            return Ok();
        }
    }
}