using Application.Commands;
using Application.DTO.IoTDTOs;
using Application.DTO.PollDTOs;
using Application.Queries;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    /// <summary>
    /// API endpoint for IoT devices
    /// </summary>
    [ApiController]
    [Route("api/IoT")]
    public class IoTDeviceController : BaseApiController
    {
        /// <summary>
        /// Get all registered IoTDevices
        /// </summary>
        /// <returns>Json object with list of IoT Guids</returns>
        /// <response code="200">Json of form [{deviceID: id}, ...]</response>
        [HttpGet]
        public async Task<ActionResult<List<CreateIoTDTO>>> getAllIoTDevices()
        {
            var result = await Mediator.Send(new GetAllIoTDevicesQuery());
            return Ok(result);
        }

        /// <summary>
        /// Get an IoT device by it's id.
        /// </summary>
        /// <param name="id">IoTDevice Guid</param>
        /// <returns>Json object with IoT Guid</returns>
        /// <response code="200">Json of form {DeviceID: id, DeviceName: name, PollQueue: [pollIDs]}</response>
        /// /// <response code="404">IoT device with id not found.</response>
        [HttpGet("{id}")]
        public async Task<ActionResult<GetIoTWithQueueDTO>> getIoTDevice(Guid id)
        {
            var result = await Mediator.Send(new GetIoTDeviceQuery(id));
            if (result == null)
            {
                return Problem(
                    title: "No poll by id found.",
                    detail: $"IoT device with id {id} was not found in database.",
                    statusCode: StatusCodes.Status404NotFound
                    );
            }
            return Ok(result);
        }

        /// <summary>
        /// Register a new IoT Device
        /// </summary>
        /// <param name="ioTDTO">Device info to register</param>
        /// <returns>Status code 201</returns>
        /// <response code="201">Device registered</response>
        /// <response code="403">Permission denied, IoT device already registered</response>
        [HttpPost]
        public async Task<ActionResult<int>> registerIoTDevice(CreateIoTDTO ioTDTO)
        {
            var result = await Mediator.Send(new RegisterIoTDeviceCommand(ioTDTO));
            if (result == -1)
            {
                return Problem(
                    title: "Permission denied, IoT device already registered.",
                    detail: $"IoT device with id {ioTDTO.deviceID} already exists.",
                    statusCode: StatusCodes.Status403Forbidden
                    );
            }
            return result == 1 ? StatusCode(201) : new BadRequestObjectResult(result);
        }

        /// <summary>
        /// Serve a public poll to an IoT device
        /// </summary>
        /// <param name="IoTId">Guid of IoT device</param>
        /// <param name="pollId">String id of poll</param>
        /// <returns></returns>
        /// <response code="200">Poll served to IoT device</response>
        /// <response code="400">Bad Request</response>
        [HttpPost("{IoTId}/{pollId}")]
        public async Task<ActionResult<int>> servePoll(Guid IoTId, string pollId)
        {
            var result = await Mediator.Send(new ServePollCommand(IoTId, pollId));
            if (result == -1)
            {
                return Problem(
                    title: "IoT device not found.",
                    detail: $"IoT device with id {IoTId} not found.",
                    statusCode: StatusCodes.Status400BadRequest
                    );
            }
            if (result == -2)
            {
                return Problem(
                    title: "Poll not found.",
                    detail: $"Poll with id {pollId} not found.",
                    statusCode: StatusCodes.Status400BadRequest
                    );
            }
            if (result == -3)
            {
                return Problem(
                    title: "Cannot serve private polls to IoT devices currently.",
                    detail: $"Poll with id {pollId} could not be served.",
                    statusCode: StatusCodes.Status400BadRequest
                    );
            }
            if (result == -4)
            {
                return Problem(
                    title: "Cannot serve closed polls.",
                    detail: $"Poll with id {pollId} has already closed.",
                    statusCode: StatusCodes.Status400BadRequest
                    );
            }
            return Ok($"Poll with id {pollId} successfully served to IoT device with id {IoTId}");
        }

        /// <summary>
        /// Gets all polls that has been served to an IoTDevice
        /// </summary>
        /// <param name="id">IoT device Guid to get polls for</param>
        /// <returns>List of polls, each with fields {Id, Question, IsPrivate, IsClosed, EndTime, CreatorId, CreatorName, CountVotes}</returns>
        /// <response code="200">Poll served to IoT device</response>
        /// <response code="400">Bad Request</response>
        [HttpGet("servedPolls/{id}")]
        public async Task<ActionResult<List<GetPollDTO>>> getServedPolls(Guid id)
        {
            var result = await Mediator.Send(new GetServedPollQuery(id));
            if (result == null)
            {
                return Problem(
                    title: "IoT device not found.",
                    detail: $"IoT device with id {id} not found.",
                    statusCode: StatusCodes.Status400BadRequest
                    );
            }
            return Ok(result);
        }

        [HttpPost("servedPolls/{IoTId}/{pollId}")]
        public async Task<ActionResult<int>> removeServedPollByPollId(Guid IoTId, string pollId)
        {
            var result = await Mediator.Send(new RemoveServedPollCommand(IoTId, pollId));
            if (result == -1)
            {
                return Problem(
                    title: "IoT device not found.",
                    detail: $"IoT device with id {IoTId} not found.",
                    statusCode: StatusCodes.Status400BadRequest
                    );
            }
            if (result == -2)
            {
                return Problem(
                    title: "Poll id not found in queue for IoT device.",
                    detail: $"Poll id with id {pollId} was not served to IoT device with id {IoTId}.",
                    statusCode: StatusCodes.Status400BadRequest
                    );
            }
            return Ok(result);
        }
    }
}
