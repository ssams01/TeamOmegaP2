using FantasyTravel.Logic;
using FantasyTravel.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
//using System.Text.Json;

namespace FantasyTravel.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlaceController : ControllerBase
    {
        private readonly IRepository _repo;
        private readonly ILogger<PlaceController> _logger;

        /*
        public IActionResult Index()
        {
            return View();
        }
        */

        public PlaceController(IRepository repo, ILogger<PlaceController> logger)
        {
            this._repo = repo;
            this._logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Place>>> GetAllPlacesAsync ()
        {
            IEnumerable<Place> places;
            try
            {
                places = await _repo.GetAllPlacesAsync();
            }
            catch (Exception ex) {
                _logger.LogError(ex, ex.Message);
                return StatusCode(500);
            }
            return places.ToList();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Place>> GetPlaceByIdAsync(int id)
        {
            Place place;
            try
            {
                place = await _repo.GetPlaceByIdAsync(id);
            }
            catch(Exception e)
            {
                _logger.LogError(e, e.Message);
                return StatusCode(500);
            }
            return place;
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeletePlaceByIdAsync (int id)
        {
            try
            {
                await _repo.DeletePlaceByIdAsync(id);
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return StatusCode(500);
            }
            return StatusCode(200);
        }
    }
}
