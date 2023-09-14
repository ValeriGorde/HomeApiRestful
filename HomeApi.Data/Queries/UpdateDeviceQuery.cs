using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeApi.DAL.Queries
{
    /// <summary>
    /// Класс для передачи дополнительных параметров при обновлении устройства
    /// </summary>
    public class UpdateDeviceQuery
    {
        public string NewName { get; set; }
        public string NewSerialNumber { get; set; }

        public UpdateDeviceQuery(string newName = null, string newSerialNumner = null)
        {
            NewName = newName;
            NewSerialNumber = newSerialNumner;
        }
    }
}
