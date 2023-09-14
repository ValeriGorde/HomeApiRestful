using HomeApi.DAL.Models;
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
        public Task<Room> GetRoomByName(string name);
        public Task AddRoom(Room room);
    }
}
