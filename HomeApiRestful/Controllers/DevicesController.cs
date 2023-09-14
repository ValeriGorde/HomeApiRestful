using AutoMapper;
using HomeApi.Contracts.Models.Devices;
using HomeApi.DAL.Models;
using HomeApi.DAL.Repositories.Interfaces;
using HomeApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace HomeApi.Controllers
{
    /// <summary>
    /// Контроллер устройств
    /// </summary>
    
    [ApiController]
    [Route("[controller]")]
    public class DevicesController : ControllerBase
    {
        private IDeviceRepository _devices;
        private IRoomRepository _rooms;
        private IMapper _mapper;

        public DevicesController(IMapper mapper, IDeviceRepository devices, IRoomRepository rooms)
        {
            _mapper = mapper;
            _devices = devices;
            _rooms = rooms;
        }

        /// <summary>
        /// Просмотр списка подключенных устройств
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("")]
        public async Task<IActionResult> GetDevices()
        {
            var devices = await _devices.GetDevices();

            var response = new GetDevicesResponse
            {
                DeviceAmount = devices.Length,
                Devices = _mapper.Map<Device[], DeviceView[]>(devices)
            };

            return StatusCode(200, response);
        }

        /// <summary>
        /// Добавление нового устройства
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("Add")]
        public IActionResult Add([FromBody] AddDeviceRequest request)
        {
            return StatusCode(200, $"Устройство {request.Name} добавлено!");
        }
    }
}
