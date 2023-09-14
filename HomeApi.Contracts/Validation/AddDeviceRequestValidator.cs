using FluentValidation;
using HomeApi.Contracts.Models.Devices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeApi.Contracts.Validation
{
    public class AddDeviceRequestValidator: AbstractValidator<AddDeviceRequest>
    {
        /// <summary>
        /// Метод контруктор, устанавливающий правила
        /// </summary>
        public AddDeviceRequestValidator()
        {
            // Правила валидации
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Manufacturer).NotEmpty();
            RuleFor(x => x.Model).NotEmpty();
            RuleFor(x => x.SerialNumber).NotEmpty();
            RuleFor(x => x.CurrentVolts).NotEmpty().InclusiveBetween(120, 220);
            RuleFor(x => x.GasUsage).NotNull();
            RuleFor(x => x.RoomLocation).NotEmpty().Must(BeSupported).
                WithMessage($"Пожалуйста, выберите одно из предложенных расположение: {string.Join(",", "_validLocation")}");
        }


        /// <summary>
        /// Метод кастомной валидации для location
        /// </summary>
        /// <param name="location"></param>
        /// <returns></returns>
        private bool BeSupported(string location)
        {
            return Values.ValidRooms.Any(e => e == location);
        }
    }
}
