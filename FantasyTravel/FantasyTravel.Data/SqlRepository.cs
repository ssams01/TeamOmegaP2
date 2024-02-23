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
            string cmdText = "SELECT * FROM [FantasyTravel].[Places];";
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
                int biomType = (int)reader["BiomType"];

                places.Add(new Place(language, biomType, name, description));
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
                int biomType = (int)reader["BiomType"];

                tmpPlace = new Place(Id, language, biomType, name, description);
            }
            await connection.CloseAsync();
            return tmpPlace;
        }

        public async Task EnterNewPlaceAsync (Place place)
        {
            throw new NotImplementedException();
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
    }
}
