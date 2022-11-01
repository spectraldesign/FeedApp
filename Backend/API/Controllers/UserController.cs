using Application.Commands;
using Application.DTO.UserDTOs;
using Application.Queries;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    /// <summary>
    /// User API endpoint
    /// </summary>
    [Authorize]
    [ApiController]
    [Route("api/user")]
    public class UserController : BaseApiController
    {
        /// <summary>
        /// Get a user from their ID
        /// </summary>
        /// <param name="id">UserID</param>
        /// <returns>User info</returns>
        /// <response code="200">{Id, UserName}</response>
        /// <response code="400">Bad Request</response>
        /// <response code="401">Unauthorized</response>
        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public async Task<ActionResult<UserDTO>> GetUserById(string id)
        {
            var result = await Mediator.Send(new GetUserQuery() { id = id });
            if (result == null)
            {
                return Problem(
                    title: "No user found by ID.",
                    statusCode: StatusCodes.Status400BadRequest
                    );
            }
            return Ok(result);
        }

        /// <summary>
        /// Get all users
        /// </summary>
        /// <returns>User info</returns>
        /// <response code="200">[{Id, UserName}]</response>
        [HttpGet]
        public async Task<ActionResult<List<User>>> GetAllUsers()
        {
            var result = await Mediator.Send(new GetAllUsersQuery());
            return Ok(result);
        }

        /// <summary>
        /// Create a new user
        /// </summary>
        /// <param name="createUserDTO">User info as json object containing {Firstname: _, Lastname: _, Email: _, Username: _, Password: _}</param>
        /// <returns>Jwt Token so the user gets logged in to the new user</returns>
        /// <response code="200">Jwt Token</response>
        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<ActionResult<IdentityResult>> createUser([FromBody] CreateUserDTO createUserDTO)
        {
            var result = await Mediator.Send(new CreateUserCommand(createUserDTO));
            if (result.Succeeded)
            {
                return await authUser(new LoginUserDTO() { UserName = createUserDTO.UserName, Password = createUserDTO.Password });

            }
            return new BadRequestObjectResult(result);
        }

        /// <summary>
        /// Login to a user.
        /// </summary>
        /// <param name="loginUserDTO">User info as a json object containing {UserName: _, Password: _}</param>
        /// <returns>Jwt Token used to authorize user</returns>
        /// <response code="200">Jwt Token</response>
        /// <response code="401">Unauthorized</response>
        [AllowAnonymous]
        [HttpPost("login")]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
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

        /// <summary>
        /// Update user info
        /// </summary>
        /// <param name="updateUserDTO">Json object with update info, valid fields are any of {Firstname, Lastname, Email, UserName, Password}</param>
        /// <returns>Statuscode 201</returns>
        /// <response code="201">User successfully updated</response>
        /// <response code="400">Bad Request</response>
        /// <response code="401">Unauthorized</response>
        [HttpPut]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public async Task<ActionResult<IdentityResult>> UpdateUserById([FromBody] UpdateUserDTO updateUserDTO)
        {
            var result = await Mediator.Send(new UpdateUserCommand(updateUserDTO));
            return !result.Succeeded ? new BadRequestObjectResult(result) : StatusCode(201);
        }
    }
}
