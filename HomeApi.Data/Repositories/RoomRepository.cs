using HomeApi.DAL.Models;
using HomeApi.DAL.Queries;
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
        /// Удаление комнаты по id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task DeleteRoom(Room room)
        {
            var entry = await _dbContext.Rooms.FirstOrDefaultAsync(e => e.Id == room.Id);
            if(entry != null)
            _dbContext.Rooms.Remove(room);

            await _dbContext.SaveChangesAsync();
        }

        /// <summary>
        /// Получение комнаты по id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<Room> GetRoomById(Guid id)
        {
            return await _dbContext.Rooms.Where(r => r.Id == id).FirstOrDefaultAsync();
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

        /// <summary>
        /// Метод для получения всех комнат
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<Room[]> GetRooms()
        {
            return await _dbContext.Rooms.ToArrayAsync();
        }

        /// <summary>
        /// Обновление комнаты
        /// </summary>
        /// <param name="room"></param>
        /// <param name="query"></param>
        /// <returns></returns>
        public async Task UpdateRoom(Room room, UpdateRoomQuery query)
        {
            if(!string.IsNullOrEmpty(query.NewName))
                room.Name = query.NewName;

            var entry = _dbContext.Entry(room);
            if (entry.State == EntityState.Detached)
                _dbContext.Rooms.Update(room);

            await _dbContext.SaveChangesAsync();
        }
    }
}
