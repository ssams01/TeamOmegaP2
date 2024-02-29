using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Net.Http;
using FantasyTravelClient.DTO;

namespace FantasyTravelClient.App
{
    public static class TravelMenu

    {

        private static readonly string uri = "https://localhost:7288";

        public static async Task Menu()
        {
            bool loop = true;
            while (loop)
            {
                Console.WriteLine("Select an option: ");
                Console.WriteLine("1. List all places.");
                Console.WriteLine("2. Display a specific place.");
                Console.WriteLine("0. Exit program.");

                string selection = Console.ReadLine();

                string tmpuri = "";
                switch (selection)
                {
                    case "1":
                        tmpuri = uri + "/api/place/Place";
                        //Console.WriteLine(tmpuri);
                        Console.WriteLine(await ListAllPlacesAsync(tmpuri));
                        break;
                    case "2":
                        int id;
                        Console.WriteLine("Please enter the id# of the desired place: ");
                        Int32.TryParse(Console.ReadLine(), out id);
                        tmpuri = uri + "/api/place/Place/" + id;
                        //Console.WriteLine(tmpuri);
                        Console.WriteLine(await ListPlaceByIdAsync(tmpuri));
                        break;
                    case "0":
                        loop = false;
                        break;
                }
            }
        }
        private static async Task<string> ListAllPlacesAsync(string uri)
        {
            HttpClient client = new HttpClient();
            try
            {
                string response = await client.GetStringAsync(uri);
                Console.WriteLine(response);
                List<Place> places = JsonSerializer.Deserialize<List<Place>>(response);
                string result = "";
                foreach (var p in places)
                {
                    result += p + "\n";
                }
                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return null;
        }
        private static async Task<string> ListPlaceByIdAsync(string uri)
        {
            HttpClient client = new HttpClient();
            try
            {
                string response = await client.GetStringAsync(uri);
                Place place = JsonSerializer.Deserialize<Place>(response);
                return place.ToString();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return null;
        }
    }
}
