using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeApi.Contracts.Models.Devices
{
    /// <summary>
    /// Запрос для обновления свойств подключённого устройства
    /// </summary>
    public class EditDeviceRequest
    {
        public string NewName {get; set;}
        public string NewRoom { get; set; }
        public string NewSerialNumber { get; set; }
    }
}
