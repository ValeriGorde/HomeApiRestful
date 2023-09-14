using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeApi.DAL.Models
{
    [Table("Devices")]
    public class Device: BaseModel
    {
        public DateTime AddDate { get; set; } = DateTime.Now;
        public string Name { get; set; }
        public string Manufacturer { get; set; }
        public string Model { get; set; }
        public string SerialNumber { get; set; }
        public int CurrentVolts { get; set; }
        public bool GasUsage { get; set; }
        public string Location { get; set; }

        // Навигационное свойство (внешний ключ)
        public Guid RoomId { get; set; }
        public Room Room { get; set; }
    }
}
