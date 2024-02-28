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
            /*
            var request = new HttpRequestMessage(HttpMethod.Get, $"{this._uri1}/{x},{y}/{this._uri2}")
            var response = await client.SendAsync(request);
            */
            string response = await client.GetStringAsync($"{this._uri1}/{x},{y}/{this._uri2}");
            //Console.WriteLine(response);
            var jsonResult1 = JsonSerializer.Deserialize<JsonElement>(response).GetProperty("properties");
            //Console.WriteLine(jsonResult1.ToString());
            var jsonResult2 = JsonSerializer.Deserialize<JsonElement>(jsonResult1.ToString()).GetProperty("temperature");
            var jsonResult3 = JsonSerializer.Deserialize<JsonElement>(jsonResult2.ToString()).GetProperty("values");
            //Console.WriteLine(jsonResult3);
            var jsonResult4 = JsonSerializer.Deserialize<JsonElement>(jsonResult3[0].ToString()).GetProperty("value");
            //Console.WriteLine(jsonResult4.ToString());
            //Weather weather = JsonSerializer.Deserialize<Weather>(response);
            Weather weather = new Weather (Math.Round(jsonResult4.GetDouble(), 2), 0);
            return weather;
        }
    }
}
