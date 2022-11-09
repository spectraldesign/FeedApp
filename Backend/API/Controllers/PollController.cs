using Application.Commands;
using Application.DTO.PollDTOs;
using Application.Queries;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    /// <summary>
    /// Poll API endpoint
    /// </summary>
    [Authorize]
    [ApiController]
    [Route("api/poll")]
    public class PollController : BaseApiController
    {
        /// <summary>
        /// Get all polls.
        /// </summary>
        /// <returns>List of polls, each with fields {Id, Question, IsPrivate, IsClosed, EndTime, CreatorId, CreatorName, CountVotes}</returns>
        /// <response code="200">Polls</response>
        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<List<Poll>>> getAllPolls()
        {
            var result = await Mediator.Send(new GetAllPollsQuery());
            return Ok(result);
        }

        /// <summary>
        /// Get a poll by its ID.
        /// </summary>
        /// <param name="Id">pollID to get</param>
        /// <returns>Poll info with fields {Id, Question, IsPrivate, IsClosed, EndTime, CreatorId, CreatorName, CountVotes}</returns>
        /// <response code="200">Poll</response>
        [AllowAnonymous]
        [HttpGet("{Id}")]
        public async Task<ActionResult<Poll>> getPoll(string Id)
        {
            var result = await Mediator.Send(new GetPollQuery(Id));
            return Ok(result);
        }

        /// <summary>
        /// Gets all polls for the currently logged in user.
        /// </summary>
        /// <returns>List of poll IDs belonging to the currently logged in user</returns>
        [HttpGet("myPolls")]
        public async Task<ActionResult<List<GetPollIdDTO>>> getUserPolls()
        {
            var result = await Mediator.Send(new GetUserPollsQuery());
            return Ok(result);
        }

        /// <summary>
        /// Create a new poll.
        /// </summary>
        /// <param name="createPollDTO">Json object with poll info, must contain fields {Question: _, IsPrivate: _, EndTime: _}</param>
        /// <returns>Status code 201</returns>
        /// <response code="201">Poll created</response>
        [HttpPost]
        [ProducesResponseType(201)]
        public async Task<ActionResult<int>> createPoll([FromBody] CreatePollDTO createPollDTO)
        {
            var result = await Mediator.Send(new CreatePollCommand(createPollDTO));
            return result == 1 ? StatusCode(201) : new BadRequestObjectResult(result);
        }

        /// <summary>
        /// Update a poll.
        /// </summary>
        /// <param name="Id">pollID of poll to update</param>
        /// <param name="updatePollDTO">Json object with updated poll info, should contain at least one of {Question: _, IsPrivate: _, EndTime: _}</param>
        /// <returns>Poll {id} updated</returns>
        /// <response code="200">Poll {id} updated</response>
        /// <response code="403">Forbidden</response>
        /// /// <response code="404">Poll not found</response>
        [HttpPut("{Id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(403)]
        public async Task<ActionResult<int>> updatePoll(string Id, [FromBody] UpdatePollDTO updatePollDTO)
        {
            var result = await Mediator.Send(new UpdatePollCommand(Id, updatePollDTO));
            if (result == 0)
            {
                return Problem(
                    title: "Cannot edit poll that has closed.",
                    statusCode: StatusCodes.Status403Forbidden
                    );
            }
            if (result == -1)
            {
                return Problem(
                    title: $"Permission denied, cannot edit poll with id: {Id}",
                    detail: "Logged in user must own the poll or have admin privileges",
                    statusCode: StatusCodes.Status403Forbidden
                    );
            }
            if (result == -2)
            {
                return Problem(
                    title: "Error, no such poll.",
                    detail: $"Poll with id {Id} not found.",
                    statusCode: StatusCodes.Status404NotFound
                    );
            }
            return Ok($"Poll {Id} updated");
        }

        /// <summary>
        /// Close a poll manually.
        /// </summary>
        /// <param name="Id">pollID of poll to be closed.</param>
        /// <returns>Poll {id} closed.</returns>
        /// <response code="200">Poll {id} closed.</response>
        /// <response code="403">Forbidden</response>
        /// /// <response code="404">Poll not found</response>
        [HttpPut("{Id}/close")]
        [ProducesResponseType(200)]
        [ProducesResponseType(403)]
        public async Task<ActionResult<int>> closePoll(string Id)
        {
            var result = await Mediator.Send(new ClosePollCommand(Id));
            if (result == -1)
            {
                return Problem(
                    title: "Error, no such poll.",
                    detail: $"Poll with id {Id} not found.",
                    statusCode: StatusCodes.Status404NotFound
                    );
            }
            if (result == -2)
            {
                return Problem(
                    title: $"Permission denied, cannot close poll with id: {Id}",
                    detail: "Logged in user must own the poll or have admin privileges",
                    statusCode: StatusCodes.Status403Forbidden
                    );
            }
            return result == 1 ? Ok($"Poll {Id} closed.") : new BadRequestObjectResult(result);
        }

        /// <summary>
        /// Delete a poll.
        /// </summary>
        /// <param name="Id">pollID of poll to be deleted</param>
        /// <returns>Poll {id} deleted</returns>
        /// <response code="200">Poll {id} deleted</response>
        /// <response code="403">Forbidden</response>
        /// /// <response code="404">Poll not found</response>
        [HttpDelete("{Id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(403)]
        public async Task<ActionResult<int>> deletePoll(string Id)
        {
            var result = await Mediator.Send(new DeletePollCommand(Id));
            if (result == -1)
            {
                return Problem(
                    title: "Error, no such poll.",
                    detail: $"Poll with id {Id} not found.",
                    statusCode: StatusCodes.Status404NotFound
                    );
            }
            if (result == -2)
            {
                return Problem(
                    title: $"Permission denied, cannot delete poll with id: {Id}",
                    detail: "Logged in user must own the poll or have admin privileges",
                    statusCode: StatusCodes.Status403Forbidden
                    );
            }
            return Ok($"Poll {Id} deleted");
        }
    }
}
