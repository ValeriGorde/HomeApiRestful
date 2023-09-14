using HomeApi.DAL.Models;
using HomeApi.DAL.Queries;
using HomeApi.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace HomeApi.DAL.Repositories
{
    public class DeviceRepository : IDeviceRepository
    {
        private readonly ApplicationDBContext _dbContext;

        public DeviceRepository(ApplicationDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>
        /// Удаление устройства
        /// </summary>
        /// <param name="device"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task DeleteDevice(Device device)
        {
            var entry = await _dbContext.Devices.FirstOrDefaultAsync(e => e.Id == device.Id);

            if (entry != null)
                _dbContext.Devices.Remove(device);

            await _dbContext.SaveChangesAsync();
        }


        /// <summary>
        /// Получение устройства по id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<Device> GetDeviceById(Guid id)
        {
            return await _dbContext.Devices
                .Include(d => d.Room)
                .Where(d => d.Id == id).FirstOrDefaultAsync();
                
        }

        /// <summary>
        /// Получение устройства по имени
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<Device> GetDeviceByName(string name)
        {
            return await _dbContext.Devices
                .Include(d => d.Room)
                .Where(d => d.Name == name).FirstOrDefaultAsync();
        }

        /// <summary>
        /// Получение всех устройств
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<Device[]> GetDevices()
        {
            return await _dbContext.Devices
                .Include(d => d.Room).ToArrayAsync();
        }

        /// <summary>
        /// Добавить новое устройство
        /// </summary>
        /// <param name="device"></param>
        /// <param name="room"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task CreateDevice(Device device, Room room)
        {
            // Привязываем новое устройство к соответсвующей комнате
            device.RoomId = room.Id;
            device.Room = room;

            var entry = _dbContext.Entry(device);
            if(entry.State == EntityState.Detached)
                await _dbContext.Devices.AddAsync(device);

            await _dbContext.SaveChangesAsync();
        }

        /// <summary>
        /// Обновление устройства
        /// </summary>
        /// <param name="device"></param>
        /// <param name="room"></param>
        /// <param name="query"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task UpdateDevice(Device device, Room room, UpdateDeviceQuery query)
        {
            // Привязываем новое устройство к соответсвующей комнате перед обновлением
            device.RoomId = room.Id;
            device.Room = room;

            // Если в запрос переданы значения, то проверяем их на null 
            // При необходимости - обновляем устройство
            if (!string.IsNullOrEmpty(query.NewName))
                device.Name = query.NewName;
            if(!string.IsNullOrEmpty(query.NewSerialNumber))
                device.SerialNumber = query.NewSerialNumber;

            var entry = _dbContext.Entry(device);

            if (entry.State == EntityState.Detached)
                _dbContext.Devices.Update(device);

            await _dbContext.SaveChangesAsync();
        }
    }
}
