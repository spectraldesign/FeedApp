using Application.Commands;
using Application.DTO;
using Application.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("/api/vote")]
    public class VoteController : BaseApiController
    {
        [AllowAnonymous]
        [HttpGet("{Id}")]
        public async Task<ActionResult<GetVoteDTO>> GetVoteById(Guid Id)
        {
            var result = await Mediator.Send(new GetVoteQuery(Id));
            return Ok(result);
        }

        [AllowAnonymous]
        [HttpGet("votesByPoll/{pollId}")]
        public async Task<ActionResult<List<GetVoteDTO>>> GetVotesByPollId(Guid pollId)
        {
            var result = await Mediator.Send(new GetVotesByPollIdQuery(pollId));
            return Ok(result);
        }

        [HttpGet("myvotes")]
        public async Task<ActionResult<List<GetVoteDTO>>> GetUserVotes()
        {
            var result = await Mediator.Send(new GetYourVotesQuery());
            return Ok(result);
        }

        [AllowAnonymous]
        [HttpPost("{pollId}")]
        public async Task<ActionResult<int>> CreateVote(Guid pollId, [FromBody] CreateVoteDTO createVoteDTO)
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

        [HttpPut("{Id}")]
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

        [HttpDelete("{Id}")]
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
