using Application.Commands;
using Application.DTO;
using Application.Queries;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/poll")]
    public class PollController : BaseApiController
    {
        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<List<Poll>>> getAllPolls()
        {
            var result = await Mediator.Send(new GetAllPollsQuery());
            return Ok(result);
        }
        [AllowAnonymous]
        [HttpGet("{Id}")]
        public async Task<ActionResult<Poll>> getPoll(Guid Id)
        {
            var result = await Mediator.Send(new GetPollQuery(Id));
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<int>> createPoll([FromBody] CreatePollDTO createPollDTO)
        {
            var result = await Mediator.Send(new CreatePollCommand(createPollDTO));
            return result == 1 ? StatusCode(201) : new BadRequestObjectResult(result);
        }

        [HttpPut("{Id}")]
        public async Task<ActionResult<int>> updatePoll(Guid Id, [FromBody] CreatePollDTO createPollDTO)
        {
            var result = await Mediator.Send(new UpdatePollCommand(Id, createPollDTO));
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
            return Ok($"Poll {Id} updated");
        }

        [HttpPut("{Id}/close")]
        public async Task<ActionResult<int>> closePoll(Guid Id)
        {
            var result = await Mediator.Send(new ClosePollCommand(Id));
            if (result == -1)
            {
                return Problem(
                    title: $"Permission denied, cannot close poll with id: {Id}",
                    detail: "Logged in user must own the poll or have admin privileges",
                    statusCode: StatusCodes.Status403Forbidden
                    );
            }
            return result == 1 ? Ok($"Poll {Id} closed.") : new BadRequestObjectResult(result);
        }

        [HttpDelete("{Id}")]
        public async Task<ActionResult<int>> deletePoll(Guid Id)
        {
            var result = await Mediator.Send(new DeletePollCommand(Id));
            if (result == -1)
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
