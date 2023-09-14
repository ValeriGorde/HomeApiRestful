using AutoMapper;
using HomeApi.Contracts.Models.Rooms;
using HomeApi.DAL.Models;
using HomeApi.DAL.Queries;
using HomeApi.DAL.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HomeApi.Controllers
{
    /// <summary>
    /// Контроллер комнат
    /// </summary>
    
    [ApiController]
    [Route("[controller]")]
    public class RoomsController : ControllerBase
    {
        private IRoomRepository _rooms;
        private IMapper _mapper;

        public RoomsController(IRoomRepository rooms, IMapper mapper)
        {
            _rooms = rooms;
            _mapper = mapper;
        }

        /// <summary>
        /// Просмотр списка комнат
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("")]
        public async Task<IActionResult> GetRooms()
        {
            var rooms = await _rooms.GetRooms();

            var response = new GetRoomsResponse
            {
                RoomAmount = rooms.Length,
                Rooms = _mapper.Map<Room[], RoomView[]>(rooms)
            };

            return StatusCode(200, response); 
        }

        /// <summary>
        /// Добавление комнаты
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("")]
        public async Task<IActionResult> Add([FromBody] AddRoomRequest request)
        {
            var room = await _rooms.GetRoomByName(request.Name);
            if (room != null)
                return StatusCode(400, $"Комната с именем - {request.Name} уже существует.");

            var newRoom = _mapper.Map<AddRoomRequest, Room>(request);
            await _rooms.AddRoom(newRoom);

            return StatusCode(201, $"Комната под названием {newRoom.Name} успешно создана!");
        }

        /// <summary>
        /// Удаление комнаты по идентификатору
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var room = await _rooms.GetRoomById(id);
            if (room == null)
                return StatusCode(400, $"Ошибка: Комната с идентификатором {id} не найдена.");

            await _rooms.DeleteRoom(room);

            return StatusCode(200, "Комната успешно удалено!");
        }

        [HttpPatch]
        [Route("{id}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] EditRoomRequest request)
        {
            var room = await _rooms.GetRoomById(id);
            if (room == null)
                return StatusCode(400, $"Ошибка: Комната с идентификатором {id} не найдено");

            var withSameName = _rooms.GetRoomByName(request.NewName);
            if (withSameName == null)
                return StatusCode(400, $"Ошибка: Комната с именем {request.NewName} уже существует. Выберите другое наименование.");

            await _rooms.UpdateRoom(room, new UpdateRoomQuery
            {
                NewName = request.NewName
            });

            return StatusCode(200, $"Комната успешно обновлена с именем {room.Name} и идентификатором {room.Id}.");

        }
    }
}
