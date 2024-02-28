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
    public class TravelMenu

    {

        private readonly string uri = "https://localhost:7288";

        public TravelMenu()
        {
        }
        public async Task Menu()
        {
            bool loop = true;
            while (loop)
            {
                Console.WriteLine(@"Fantasy Travel App:
                1: List All Travel Locations
                2: List A Specific Travel Location
                3: Create a New Travel Location
                4: Delete a Travel Location
                0: Exit");
                string selection = Console.ReadLine();

                string tmpuri = "";
                switch (selection)
                {
                    case "1":
                        tmpuri = uri + "/api/place/Place";
                        Console.WriteLine(tmpuri);
                        Console.WriteLine(await ListAllPlacesAsync(tmpuri));
                        Console.WriteLine("All travel locations listed!");
                        break;

                    case "2":
                        Console.WriteLine("The specified travel location was returned");
                        break;

                    case "3":
                        Console.WriteLine("That new travel location was created");
                        break;

                    case "4":
                        Console.WriteLine("That travel location was deleted");
                        break;

                    case "0":
                        loop = false;
                        break;

                    default:
                        break;
                }
            }
        }

        public static async Task<string> ListAllPlacesAsync(string tmpuri)
        {
            Console.WriteLine("1");
            HttpClient client = new HttpClient();
            Console.WriteLine("2");
            string response = await client.GetStringAsync(tmpuri);
            Console.WriteLine("3");
            List<Place> places = JsonSerializer.Deserialize<List<Place>>(response);
            Console.Write("4");
            string result = "";

            foreach (var p in places)
            {
                result += p.ToString() + "\n";
            }

            return result;

        }
    }
}
