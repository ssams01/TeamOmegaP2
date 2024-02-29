using FantasyTravel.Logic;

namespace FantasyTravel.Data
{
    public interface IRepository
    {
        public Task<IEnumerable<Place>> GetAllPlacesAsync ();
        public Task<Place> GetPlaceByIdAsync (int id);
        public Task EnterNewPlaceAsync (Place place);
        public Task DeletePlaceByIdAsync (int id);
        public Task<IEnumerable<Biome>> GetAllBiomesAsync();
        public Task UpdateBiomeTemperatureAsync(int id, double temp);
    }
}
