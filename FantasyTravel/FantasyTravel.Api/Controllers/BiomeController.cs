using FantasyTravel.Logic;
using FantasyTravel.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FantasyTravel.Api {
    [Route("api/biome/[controller]")]
    [ApiController]
    public class BiomeController : ControllerBase {
        private readonly IRepository _repo;
        private readonly ILogger<BiomeController> _logger;

        public BiomeController (IRepository _repo, ILogger<BiomeController> logger) {
            this._repo = _repo;
            this._logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Biome>>> GetAllBiomesAsync() {
            IEnumerable<Biome> biomes;
            try {
                biomes = await _repo.GetAllBiomesAsync();
            }
            catch(Exception e) {
                _logger.LogError(e, e.Message);
                return StatusCode(500);
            }
            return biomes.ToList();
        }

        [HttpPatch]
        public async Task<ActionResult> UpdateBiomeTemperatureAsync(int id, double temp) {
            try {
                await _repo.UpdateBiomeTemperatureAsync(id, temp);
            }
            catch(Exception e) {
                _logger.LogError(e, e.Message);
                return StatusCode(500);
            }
            return StatusCode(200);
        }
    }
}