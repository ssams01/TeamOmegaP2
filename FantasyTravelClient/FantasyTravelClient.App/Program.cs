using System;
using System.Net.Http;
using System.Text.Json;
using FantasyTravelClient.DTO;

namespace FantasyTravelClient.App
{
    public class Program
    {
        public static async Task Main (string[] args)
        {
            Console.WriteLine("-----------");
            Console.WriteLine("starting...");
            Console.WriteLine("-----------");

            string uri = "https://localhost:7288";

            Console.WriteLine("URI: " + uri);

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
                        Console.WriteLine(tmpuri);
                        Console.WriteLine(await ListAllPlacesAsync(tmpuri));
                        break;
                    case "2":
                        Console.WriteLine("option 2 selected");
                        break;
                    case "0":
                        loop = false;
                        break;
                }
            }
        }

        private static async Task<string> ListAllPlacesAsync (string uri)
        {
            HttpClient client = new HttpClient ();
            Console.WriteLine("httpClient created");
            string response = await client.GetStringAsync(uri);
            Console.WriteLine(response);
            List<Place> places = JsonSerializer.Deserialize<List<Place>>(response);
            string result = "";
            foreach (var p in places)
            {
                result += p + "\n";
            }
            return result;
            /*
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
            */
        }
    }        
}