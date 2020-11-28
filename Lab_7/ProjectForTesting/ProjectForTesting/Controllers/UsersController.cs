using Microsoft.AspNetCore.Mvc;
using ProjectForTesting.DataAccess;
using ProjectForTesting.Domain.Entities;
using ProjectForTesting.Domain.ValueObjects;
using ProjectForTesting.Dtos;
using System.Threading.Tasks;

namespace ProjectForTesting.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public UsersController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            return Ok(await _unitOfWork.Users.GetByIdAsync(id));
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
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

            _unitOfWork.Users.Update(user);
            await _unitOfWork.CommitAsync();

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Add(int id)
        {
            await _unitOfWork.Users.DeleteByIdAsync(id);
            await _unitOfWork.CommitAsync();

            return Ok();
        }
    }
}