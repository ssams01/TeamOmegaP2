using FantasyTravel.Logic;
using FantasyTravel.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FantasyTravel.Api.Controllers
{

    [Route("api/weather/[controller]")]
    [ApiController]
    public class WeatherController : ControllerBase
    {
        private readonly JsonRepository _repo;
        private readonly ILogger<WeatherController> _logger;

        
        public WeatherController (JsonRepository repo, ILogger<WeatherController> logger)
        {
            _repo = repo;
            _logger = logger;
        }

        [HttpGet("{x}, {y}")]
        public async Task<ActionResult<Weather>> GetWeatherByCoordinatesAsync (int x, int y)
        {
            Weather weather;
            try
            {
                weather = await _repo.GetWeatherByCoordinatesAsync(x, y);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(500);
            }
            return weather;
        }
    }
}
