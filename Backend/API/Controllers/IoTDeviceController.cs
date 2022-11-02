using Application.Commands;
using Application.DTO.IoTDTOs;
using Application.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
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
        public async Task<ActionResult<List<IoTDTO>>> getAllIoTDevices()
        {
            var result = await Mediator.Send(new GetAllIoTDevicesQuery());
            return Ok(result);
        }

        /// <summary>
        /// Get an IoT device by it's id.
        /// </summary>
        /// <param name="id">IoTDevice Guid</param>
        /// <returns>Json object with IoT Guid</returns>
        /// <response code="200">Json of form {deviceID: id}</response>
        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<ActionResult<IoTDTO>> getIoTDevice(Guid id)
        {
            var result = await Mediator.Send(new GetIoTDeviceQuery(id));
            return Ok(result);
        }

        /// <summary>
        /// Register a new IoT Device
        /// </summary>
        /// <param name="id">Device ID to register</param>
        /// <returns>Status code 201</returns>
        /// <response code="201">Device registered</response>
        /// <response code="403">Permission denied, IoT device already registered</response>
        [HttpPost]
        public async Task<ActionResult<int>> registerIoTDevice(IoTDTO ioTDTO)
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
    }
}
