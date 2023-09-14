using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeApi.Contracts.Validation
{
    /// <summary>
    /// Класс для хранения всех допустимых значений для валидатора
    /// </summary>
    public class Values
    {
        public static string[] ValidRooms = new[]
        {
            "Кухня",
            "Ванная",
            "Гостиная",
            "Туалет"
        };
    }
}
