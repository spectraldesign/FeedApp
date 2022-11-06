using Application.Commands;
using Application.DTO.VoteDTOs;
using Application.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    /// <summary>
    /// Vote API endpoint
    /// </summary>
    [Authorize]
    [ApiController]
    [Route("/api/vote")]
    public class VoteController : BaseApiController
    {
        /// <summary>
        /// Get a vote by its ID.
        /// </summary>
        /// <param name="Id">VoteID</param>
        /// <returns>Vote info, json object with fields {Id, IsPositive, VotedPollId, VotedPollQuestion}</returns>
        /// <response code="200">Vote</response>
        [AllowAnonymous]
        [HttpGet("{Id}")]
        public async Task<ActionResult<GetVoteDTO>> GetVoteById(Guid Id)
        {
            var result = await Mediator.Send(new GetVoteQuery(Id));
            return Ok(result);
        }

        /// <summary>
        /// Get all votes on a given poll.
        /// </summary>
        /// <param name="pollId">PollID</param>
        /// <returns>List of votes, each with fields {Id, IsPositive, VotedPollId, VotedPollQuestion}</returns>
        /// <response code="200">Votes</response>
        [AllowAnonymous]
        [HttpGet("votesByPoll/{pollId}")]
        public async Task<ActionResult<List<GetVoteDTO>>> GetVotesByPollId(string pollId)
        {
            var result = await Mediator.Send(new GetVotesByPollIdQuery(pollId));
            return Ok(result);
        }

        /// <summary>
        /// Get all votes for the currently logged in user.
        /// </summary>
        /// <returns>List of votes, each with fields {Id, IsPositive, VotedPollId, VotedPollQuestion}</returns>
        /// <response code="200">Votes</response>
        [HttpGet("myvotes")]
        public async Task<ActionResult<List<GetVoteDTO>>> GetUserVotes()
        {
            var result = await Mediator.Send(new GetYourVotesQuery());
            return Ok(result);
        }

        /// <summary>
        /// Create a new vote
        /// </summary>
        /// <param name="pollId">pollID of poll to vote on</param>
        /// <param name="createVoteDTO">Vote information, json object with content {IsPositive: _}</param>
        /// <returns>Status code 201</returns>
        /// <response code="201">Vote Registered</response>
        /// <response code="403">Forbidden</response>
        [AllowAnonymous]
        [HttpPost("{pollId}")]
        [ProducesResponseType(201)]
        [ProducesResponseType(403)]
        public async Task<ActionResult<int>> CreateVote(string pollId, [FromBody] CreateVoteDTO createVoteDTO)
        {
            var result = await Mediator.Send(new CreateVoteCommand(pollId, createVoteDTO));
            if (result == 0)
            {
                return Problem(
                    title: "Permission denied, cannot vote on closed poll.",
                    detail: $"Poll with id {pollId} is closed.",
                    statusCode: StatusCodes.Status403Forbidden
                    );
            }
            if (result == -1)
            {
                return Problem(
                    title: "Permission denied, cannot vote on private poll",
                    detail: $"Poll with id {pollId} is private, so you must log in to vote.",
                    statusCode: StatusCodes.Status403Forbidden
                    );
            }
            if (result == -2)
            {
                return Problem(
                    title: "Permission denied, cannot vote multiple times on the same poll",
                    detail: $"You have already voted for poll with id {pollId}.",
                    statusCode: StatusCodes.Status403Forbidden
                    );
            }
            if (result == -3)
            {
                return Problem(
                    title: "Invalid ID",
                    detail: $"Poll with ID {pollId} not found.",
                    statusCode: StatusCodes.Status403Forbidden
                    );
            }
            return result == 1 ? StatusCode(201) : new BadRequestObjectResult(result);
        }

        /// <summary>
        /// Update a vote.
        /// </summary>
        /// <param name="Id">voteID of vote to be updated</param>
        /// <param name="updateVoteDTO">Update info as json object with field {IsPositive: _}</param>
        /// <returns>Vote updated</returns>
        /// <response code="200">Vote updated</response>
        /// <response code="403">Forbidden</response>
        [HttpPut("{Id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(403)]
        public async Task<ActionResult<int>> UpdateVote(Guid Id, [FromBody] CreateVoteDTO updateVoteDTO)
        {
            var result = await Mediator.Send(new ChangeVoteCommand(Id, updateVoteDTO));
            if (result == 0)
            {
                return Problem(
                    title: "Permission denied, cannot edit vote on closed poll.",
                    detail: $"Poll with id {Id} is closed.",
                    statusCode: StatusCodes.Status403Forbidden
                    );
            }
            if (result == -1)
            {
                return Problem(
                    title: $"Permission denied, cannot edit vote with id: {Id}",
                    detail: "Logged in user must own the vote to change it.",
                    statusCode: StatusCodes.Status403Forbidden
                    );
            }
            if (result == -2)
            {
                return Problem(
                    title: "Not a valid vote",
                    detail: $"Vote with id {Id} not found.",
                    statusCode: StatusCodes.Status403Forbidden
                    );
            }
            return Ok("Vote updated");
        }

        /// <summary>
        /// Delete a vote.
        /// </summary>
        /// <param name="Id">voteID to be deleted</param>
        /// <returns>Vote deleted</returns>
        /// <response code="200">Vote deleted</response>
        /// <response code="403">Forbidden</response>
        [HttpDelete("{Id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(403)]
        public async Task<ActionResult<int>> DeleteVote(Guid Id)
        {
            var result = await Mediator.Send(new DeleteVoteCommand(Id));
            if (result == -1)
            {
                return Problem(
                    title: $"Permission denied, cannot delete vote with id: {Id}",
                    detail: "Logged in user must own the vote to delete it.",
                    statusCode: StatusCodes.Status403Forbidden
                    );
            }
            return Ok("Vote deleted");
        }
    }
}
