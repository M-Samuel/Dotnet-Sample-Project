using Dotnet.EventSourcing.Application.Commands.CreateUser;
using Dotnet.EventSourcing.Application.DTOS;
using Dotnet.EventSourcing.Application.Queries;
using Dotnet.EventSourcing.Domain.UserDomain;
using Dotnet.EventSourcing.SharedKernel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Dotnet.EventSourcing.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserQueries _userQueries;
        private readonly ICreateUserCommand _createUserCommand;

        public UserController(IUserQueries userQueries, ICreateUserCommand createUserCommand)
        {
            _userQueries = userQueries;
            _createUserCommand = createUserCommand;
        }


        [HttpGet("/user/{id}")]
        public async Task<ActionResult<UserDTO>> GetUserById(Guid id, CancellationToken cancellationToken)
        {
            EventId eventId = new EventId(0,Guid.NewGuid().ToString());
            var user = await _userQueries.GetUserByIdAsync(eventId, id, cancellationToken);
            if(user == null)
                return NotFound();
            return Ok(user.ToDTO());
        }

        [HttpPost("/user")]

        public async Task<ActionResult<UserDTO>> CreateUser([FromForm] CreateUserData createUserData, CancellationToken cancellationToken)
        {
            EventId eventId = new EventId(0, Guid.NewGuid().ToString());
            Result<User> result = await _createUserCommand.ProcessCommandAsync(createUserData, eventId, cancellationToken);

            if (result.HasError)
                return BadRequest(result.DomainErrors);

            return new CreatedAtActionResult(nameof(GetUserById), "User", new { id = result.EntityValue.Id }, result.EntityValue.ToDTO());
        }
    }
}
