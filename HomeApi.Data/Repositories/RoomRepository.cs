using HomeApi.DAL.Models;
using HomeApi.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeApi.DAL.Repositories
{
    /// <summary>
    /// Репозиторий для операций с объектами типа "Room" в базе
    /// </summary>
    public class RoomRepository : IRoomRepository
    {
        private readonly ApplicationDBContext _dbContext;
        public RoomRepository(ApplicationDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>
        /// Метод для добавления команты
        /// </summary>
        /// <param name="room"></param>
        /// <returns></returns>
        public async Task AddRoom(Room room)
        {
            var entry = _dbContext.Entry(room);
            if (entry.State == EntityState.Detached)
                await _dbContext.Rooms.AddAsync(room);

            await _dbContext.SaveChangesAsync();
        }

        /// <summary>
        /// Метод для получения комнаты по наименованию
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<Room> GetRoomByName(string name)
        {
            return await _dbContext.Rooms
                .Where(r => r.Name == name).FirstOrDefaultAsync();
        }
    }
}
