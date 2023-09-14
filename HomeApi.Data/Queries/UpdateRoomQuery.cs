using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeApi.DAL.Queries
{
    /// <summary>
    /// Класс для передачи дополнительных параметров для комнат
    /// </summary>
    public class UpdateRoomQuery
    {
        public string NewName { get; set; }

        public UpdateRoomQuery(string newName = null)
        {
            NewName = newName;
        }
    }
}
