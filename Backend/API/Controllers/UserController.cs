using Application.Commands;
using Application.DTO;
using Application.Queries;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/user")]
    public class UserController : BaseApiController
    {
        [HttpGet("{id}")]
        public async Task<ActionResult<UserDTO>> GetUserById(string id)
        {
            var result = await Mediator.Send(new GetUserQuery() { id = id });
            return Ok(result);
        }

        [HttpGet]
        public async Task<ActionResult<List<User>>> GetAllUsers()
        {
            var result = await Mediator.Send(new GetAllUsersQuery());
            return Ok(result);
        }
        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<ActionResult<IdentityResult>> createUser([FromBody] CreateUserDTO createUserDTO)
        {
            var result = await Mediator.Send(new CreateUserCommand(createUserDTO));
            return !result.Succeeded ? new BadRequestObjectResult(result) : StatusCode(201);
        }
        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<ActionResult<IdentityResult>> authUser([FromBody] LoginUserDTO loginUserDTO)
        {
            var validated = await Mediator.Send(new ValidateUserCommand(loginUserDTO));
            if (validated)
            {
                var token = await Mediator.Send(new CreateTokenCommand());
                return Ok(token);
            }
            return Unauthorized();
        }
        [HttpPut]
        public async Task<ActionResult<IdentityResult>> UpdateUserById([FromBody] CreateUserDTO updateUserDTO)
        {
            var result = await Mediator.Send(new UpdateUserCommand(updateUserDTO));
            return !result.Succeeded ? new BadRequestObjectResult(result) : StatusCode(201);
        }
    }
}
