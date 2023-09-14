using AutoMapper;
using HomeApi.Contracts.Models.Devices;
using HomeApi.Contracts.Models.Home;
using HomeApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Text;

namespace HomeApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HomeController: ControllerBase
    {
        private IMapper _mapper;
        // Ссылка на объект конфигурации
        private IOptions<HomeOptions> _options;

        public HomeController(IOptions<HomeOptions> options, IMapper mapper)
        {
            _options = options;
            _mapper = mapper;
        }


        /// <summary>
        /// Метод для получения информации о доме
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("info")]
        public IActionResult Info() 
        {
            var infoResponse = _mapper.Map<HomeOptions, InfoResponse>(_options.Value);

            // Преобразуем результат в строку и выводим, как обычную веб-страницу
            return StatusCode(200, infoResponse);
        }

    }
}
