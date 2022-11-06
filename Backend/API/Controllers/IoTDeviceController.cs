using Application.Commands;
using Application.DTO.IoTDTOs;
using Application.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    /// <summary>
    /// API endpoint for IoT devices
    /// </summary>
    [Authorize]
    [ApiController]
    [Route("api/IoT")]
    public class IoTDeviceController : BaseApiController
    {
        /// <summary>
        /// Get all registered IoTDevices
        /// </summary>
        /// <returns>Json object with list of IoT Guids</returns>
        /// <response code="200">Json of form [{deviceID: id}, ...]</response>
        [AllowAnonymous]
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
        [AllowAnonymous]
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
        /// Serve a poll to an IoT device
        /// </summary>
        /// <param name="id">Guid of IoT device</param>
        /// <param name="pollId">String id of poll</param>
        /// <returns></returns>
        /// <response code="200">Poll served to IoT device</response>
        /// <response code="400">Bad Request</response>
        [HttpPost("{id}")]
        public async Task<ActionResult<int>> servePoll(Guid id, [FromBody] string pollId)
        {
            var result = await Mediator.Send(new ServePollCommand(id, pollId));
            if (result == -1)
            {
                return Problem(
                    title: "IoT device not found.",
                    detail: $"IoT device with id {id} not found.",
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
            return Ok($"Poll with id {pollId} successfully served to IoT device with id {id}");
        }
    }
}
