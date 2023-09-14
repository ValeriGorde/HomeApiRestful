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
    /// Интерфейс, определяющая методы для доступа к объектам типа Device в БД
    /// </summary>
    public interface IDeviceRepository
    {
        Task<Device[]> GetDevices();
        Task<Device> GetDeviceByName(string name);
        Task<Device> GetDeviceById(Guid id);
        Task CreateDevice(Device device, Room room);
        Task UpdateDevice(Device device, Room room, UpdateDeviceQuery query);
        Task DeleteDevice(Device device);

    }
}
