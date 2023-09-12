using AutoMapper;
using HomeApiRestful.Contracts.Devices;
using HomeApiRestful.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace HomeApiRestful.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DevicesController: ControllerBase
    {
        private IOptions<HomeOptions> _options;
        private IMapper _mapper;

        public DevicesController(IOptions<HomeOptions> options, IMapper mapper)
        {
            _options = options;
            _mapper = mapper;
        }

        /// <summary>
        /// Просмотр списка подключенных устройств
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("")]
        public IActionResult Get()
        {
            return StatusCode(200, "Устройства отсутствуют");

        }

        /// <summary>
        /// Добавление нового устройства
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("Add")]
        public IActionResult Add([FromBody] AddDeviceRequest request)
        {
            if (ModelState.IsValid)
            {
                return StatusCode(200, $"Устройство {request.Name} добавлено!");
            }
            return BadRequest(ModelState);


        }
    }
}
