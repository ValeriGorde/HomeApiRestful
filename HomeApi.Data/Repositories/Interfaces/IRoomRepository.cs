using HomeApi.DAL.Models;
using HomeApi.DAL.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeApi.DAL.Repositories.Interfaces
{
    /// <summary>
    /// Интерфейс, определяющий методы для доступа к объектам типа Room в БД
    /// </summary>
    public interface IRoomRepository
    {
        Task<Room[]> GetRooms();
        Task<Room> GetRoomById(Guid id);
        Task<Room> GetRoomByName(string name);
        Task AddRoom(Room room);    
        Task DeleteRoom(Room room);
        Task UpdateRoom(Room room, UpdateRoomQuery query);
    }
}
