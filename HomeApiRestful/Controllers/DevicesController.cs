using AutoMapper;
using HomeApi.Contracts.Models.Devices;
using HomeApi.DAL.Models;
using HomeApi.DAL.Queries;
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
        [Route("")]
        public async Task<IActionResult> Add([FromBody] AddDeviceRequest request)
        {
            var room = await _rooms.GetRoomByName(request.RoomLocation);
            if (room == null)
                return StatusCode(400, $"Ошибка: Комната {request.RoomLocation} не подключена. Сначала подключите комнату!");

            var device = await _devices.GetDeviceByName(request.Name);
            if (device != null)
                return StatusCode(400, $"Ошибка: Устройство {request.Name} уже существует.");

            var newDevice = _mapper.Map<AddDeviceRequest, Device>(request);
            await _devices.CreateDevice(newDevice, room);

            return StatusCode(201, $"Устройство {request.Name} добавлено! Идентификатор: {newDevice.Id}");
        }


        /// <summary>
        /// Обновление существующего устройства (частичное)
        /// </summary>
        /// <param name="id"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPatch]
        [Route("{id}")]
        public async Task<IActionResult> Edit([FromRoute] Guid id, [FromBody] EditDeviceRequest request)
        {
            var room = await _rooms.GetRoomByName(request.NewRoom);
            if (room == null)
                return StatusCode(400, $"Ошибка: Комната {request.NewName} не подключена. Сначала подключите комнату");

            var device = await _devices.GetDeviceById(id);
            if (device == null)
                return StatusCode(400, $"Ошибка: Устройства с идентификатором {id} не существует.");

            var withSameName = await _devices.GetDeviceByName(request.NewName);
            if (withSameName == null)
                return StatusCode(400, $"Ошибка: Устройство с именем {request.NewName} уже существует. Выберите другое имя!");

            await _devices.UpdateDevice(device, room, new UpdateDeviceQuery
            {
                NewName = request.NewName,
                NewSerialNumber = request.NewSerialNumber
            });

            return StatusCode(200, $"Устройство обновлено! Имя: {device.Name}, " +
                $"Серийный номер: {device.SerialNumber}, Комната подключения: {device.Room.Name}.");
        }

        /// <summary>
        /// Удаление устройства
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteById([FromRoute] Guid id)
        {
            var device = await _devices.GetDeviceById(id);
            if (device == null)
                return StatusCode(400, $"Ошибка: Устройство с идентификатором {id} не найдено.");

            await _devices.DeleteDevice(device);

            return StatusCode(200, "Устройство успешно удалено!");
        }
    }
}
