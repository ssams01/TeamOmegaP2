using System;
using System.Text;
using System.Data.SqlClient;
using FantasyTravel.Logic;
using Microsoft.Extensions.Logging;

namespace FantasyTravel.Data
{
    public class SqlRepository : IRepository
    {
        private readonly string _connectionString;
        private readonly ILogger<SqlRepository> _logger;

        public SqlRepository (string connectionString, ILogger<SqlRepository> logger)
        {
            this._connectionString = connectionString;
            this._logger = logger;
        }

        public async Task<IEnumerable<Place>> GetAllPlacesAsync ()
        {
            using SqlConnection connection = new SqlConnection(this._connectionString);
            //Console.WriteLine("Opening connection");
            await connection.OpenAsync();
            //Console.WriteLine("getting SQL statement");
            string cmdText = "SELECT p.Id, p.Name, p.Description, p.Language, p.BiomeType, b.BiomeType as BiomeDesc, b.Temp FROM [FantasyTravel].[Places] as p JOIN [FantasyTravel].[Biomes] AS b ON p.BiomeType = b.Id;";
            using SqlCommand cmd = new SqlCommand(cmdText, connection);
            using SqlDataReader reader = await cmd.ExecuteReaderAsync();
            Console.WriteLine("Reader executed...");
            List<Place> places = new List<Place>();

            while (await reader.ReadAsync())
            {
                int Id = (int)reader["Id"];
                string name = reader["Name"].ToString() ?? "";
                string description = reader["Description"].ToString() ?? "";
                int language = (int)reader["Language"];
                int biomeType = (int)reader["BiomeType"];
                double temp = (double)reader["Temp"];

                places.Add(new Place(Id, language, biomeType, temp, name, description));
            }
            await connection.CloseAsync();

            _logger.LogInformation("Executed GetAllPlacesAsync; returned {0} results.", places.Count);
            return places;
        }

        public async Task<Place> GetPlaceByIdAsync (int id)
        {

            using SqlConnection connection = new SqlConnection(this._connectionString);
            await connection.OpenAsync();

            string cmdText = "SELECT * From [FantasyTravel].[Places] WHERE Id = @id;";

            using SqlCommand cmd = new SqlCommand(cmdText, connection);

            cmd.Parameters.AddWithValue("@id", id);

            using SqlDataReader reader = await cmd.ExecuteReaderAsync();
            Place tmpPlace = new Place();

            while (await reader.ReadAsync())
            {
                int Id = (int)reader["Id"];
                string name = reader["Name"].ToString() ?? "";
                string description = reader["Description"].ToString() ?? "";
                int language = (int)reader["Language"];
                int biomeType = (int)reader["BiomeType"];

                tmpPlace = new Place(Id, language, biomeType, name, description);
            }
            await connection.CloseAsync();
            return tmpPlace;
        }

        public async Task EnterNewPlaceAsync (Place place)
        {
           
            using SqlConnection connection = new(_connectionString);
            await connection.OpenAsync();

            //string cmdText = @"INSERT INTO [FantasyTravel].[Places] (Id, Name, Description, Language, BiomeType) VALUES (@id, @name, @description, @language, @biometype);";
            string cmdText = @"INSERT INTO [FantasyTravel].[Places] (Name, Description, Language, BiomeType) VALUES (@name, @description, @language, @biometype);";

            using SqlCommand cmd = new(cmdText, connection);

            //cmd.Parameters.AddWithValue("@id", place.id);
            cmd.Parameters.AddWithValue("@name", place.name);
            cmd.Parameters.AddWithValue("@description", place.description);
            cmd.Parameters.AddWithValue("@language", place.language);
            cmd.Parameters.AddWithValue("@biometype", place.biomeType);
         

            await cmd.ExecuteNonQueryAsync();
            await connection.CloseAsync();

            _logger.LogInformation("Executed EnterNewPlaceAsync, place id #{0}: {1} created.", place.id, place.name);
        }

        // Implemented by James and Ian
        public async Task DeletePlaceByIdAsync (int id)
        {
            using SqlConnection connection = new SqlConnection(this._connectionString);
            await connection.OpenAsync();
            string cmdText = "DELETE FROM [FantasyTravel].[Places] WHERE Id = @id;";
            using SqlCommand cmd = new SqlCommand(cmdText, connection);
            cmd.Parameters.AddWithValue("@id", id);

            await cmd.ExecuteNonQueryAsync();
            await connection.CloseAsync();

            _logger.LogInformation("Executed DeletePlaceByIdAsync; place id #{0} deleted.", id);
        }

        public async Task<IEnumerable<Biome>> GetAllBiomesAsync() {
            using SqlConnection connection = new SqlConnection(this._connectionString);
            await connection.OpenAsync();
            string cmdText = "SELECT * FROM [FantasyTravel].[Biomes];";
            using SqlCommand cmd = new SqlCommand(cmdText, connection);

            using SqlDataReader reader = await cmd.ExecuteReaderAsync();
            List<Biome> biomes = new List<Biome>();

            while(await reader.ReadAsync()) {
                int id = (int)reader["id"];
                int x = (int)reader["x"];
                int y = (int)reader["y"];

                biomes.Add(new Biome(id, x, y));
            }
            await connection.CloseAsync();

            _logger.LogInformation("Executed get all biomes, returned {0} results.", biomes.Count);
            return biomes;
        }
        public async Task UpdateBiomeTemperatureAsync(int id, double temp) {
            using SqlConnection connection = new SqlConnection(this._connectionString);
            await connection.OpenAsync();
            string cmdText = "UPDATE [FantasyTravel].[Biomes] SET temp = @temp WHERE id = @id";
            using SqlCommand cmd = new(cmdText, connection);

            cmd.Parameters.AddWithValue("@id", id);
            cmd.Parameters.AddWithValue("@temp", temp);

            await cmd.ExecuteNonQueryAsync();
            await connection.CloseAsync();

            _logger.LogInformation("executed updateBiomeTemperature ");

        }
    }
}
