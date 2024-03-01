using System;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using FantasyTravel.Logic;
using Microsoft.Extensions.Logging;

namespace FantasyTravel.Data
{
    public class JsonRepository
    {
        private readonly string _uri1;
        private readonly string _uri2;
        private readonly ILogger<JsonRepository> _logger;

        public JsonRepository (string uri1, string uri2, ILogger<JsonRepository> logger)
        {
            this._uri1 = uri1;
            this._uri2 = uri2;
            this._logger = logger;
        }

        public async Task<Weather> GetWeatherByCoordinatesAsync (int x, int y)
        {
            using HttpClient client = new HttpClient ();
            var authorization = new ProductInfoHeaderValue("authKey", "Project2Testing");
            client.DefaultRequestHeaders.UserAgent.Add(authorization);
            
            //Console.WriteLine($"{this._uri1}/{x},{y}/{this._uri2}");
            string response = await client.GetStringAsync($"{this._uri1}/{x},{y}/{this._uri2}");

            var jsonResult1 = JsonSerializer.Deserialize<JsonElement>(response).GetProperty("properties");
            var jsonResult2 = JsonSerializer.Deserialize<JsonElement>(jsonResult1.ToString()).GetProperty("temperature");
            var jsonResult3 = JsonSerializer.Deserialize<JsonElement>(jsonResult2.ToString()).GetProperty("values");
            var jsonResult4 = JsonSerializer.Deserialize<JsonElement>(jsonResult3[0].ToString()).GetProperty("value");

            Weather weather = new Weather (Math.Round(jsonResult4.GetDouble(), 2), 0);
            return weather;
        }
        
    }
}
