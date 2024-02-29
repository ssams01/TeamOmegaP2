using System;
using System.Net.Http;

using System.Text.Json;
using FantasyTravelClient.DTO;

namespace FantasyTravelClient.App
{
    public class Program
    {
        private static readonly string uri = "https://localhost:7288";
        public static async Task Main(string[] args)
        {
            Console.WriteLine("-----------");
            Console.WriteLine("starting...");
            Console.WriteLine("-----------");

            await Menu();

            Console.WriteLine("Program finishing");

        }

        //  MENU METHODS

        public static async Task Menu()
        {
            bool loop = true;
            while (loop)
            {
                Console.WriteLine("Select an option: ");
                Console.WriteLine("1. List all places.");
                Console.WriteLine("2. Display a specific place.");
                Console.WriteLine("3. Share a new place.");
                Console.WriteLine("4. Delete a travel place.");
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
                        int idToGet;
                        Console.WriteLine("Please enter the id# of the desired place: ");
                        Int32.TryParse(Console.ReadLine(), out idToGet);
                        tmpuri = uri + "/api/place/Place/" + idToGet;
                        //Console.WriteLine(tmpuri);
                        Console.WriteLine(await ListPlaceByIdAsync(tmpuri));
                        break;
                    case "3":
                        tmpuri = uri + "/api/place/Place";
                        Place tmpPlace = BuildUserPlace();
                        Console.WriteLine(await EnterNewPlaceAsync(tmpuri, tmpPlace));
                        break;
                    case "4":
                        int idToDelete;
                        Console.WriteLine("Please enter the id# of the place you wish to delete: ");
                        Int32.TryParse(Console.ReadLine(), out idToDelete);
                        tmpuri = uri + "/api/place/Place/" + idToDelete;
                        Console.WriteLine(await DeletePlaceByIdAsync(tmpuri));
                        break;
                    case "0":
                        loop = false;
                        break;
                    default:
                        Console.WriteLine("Huh?");
                        break;
                }
            }
        }

        private static Place BuildUserPlace ()
        {
            bool nameNeeded = true, descNeeded = true, langNeeded = true, biomeNeeded = true;
            string name = null, desc = null;
            int lang = -1, biomeType = -1;

            while (nameNeeded)
            {
                Console.WriteLine("Enter the name of the location (max 100 characters): ");
                name = Console.ReadLine();
                if (name is null || name.Length <= 0)
                {
                    Console.WriteLine("Don't be like that.");
                }
                else
                {
                    nameNeeded = false;
                }
            }
            while (descNeeded)
            {
                Console.WriteLine("Enter in a description for the place (max 255): ");
                desc = Console.ReadLine();
                if (desc is null || desc.Length <= 0)
                {
                    Console.WriteLine("No description? Alrighty then.");
                }
                descNeeded = false;
            }
            while (langNeeded)
            {
                Console.WriteLine("Which language is spoken there? Enter it by ID:");
                if (!Int32.TryParse(Console.ReadLine(), out lang))
                {
                    Console.WriteLine("The ID of the language, please.");
                }
                else
                {
                    langNeeded = false;
                }
            }
            while (biomeNeeded)
            {
                Console.WriteLine("Which biome type (1-5) does it fall under?");
                if (!Int32.TryParse (Console.ReadLine(), out biomeType))
                {
                    Console.WriteLine("At least give us a number.");
                }
                else if (biomeType < 1 ||  biomeType > 5)
                {
                    Console.WriteLine("Not a currently recorded biome type! Please try again.");
                }
                else
                {
                    biomeNeeded = false;
                }
            }
            return new Place(lang, biomeType, name, desc);
        }

        //  HTTP CALLS

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

        private static async Task<bool> EnterNewPlaceAsync (string uri, Place place)
        {
            HttpClient client = new HttpClient();
            try
            {
                var placeContent = new StringContent(JsonSerializer.Serialize(place));
                Console.WriteLine("placeContent: \n" + JsonSerializer.Serialize(place) + "\n");
                placeContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
                var response = await client.PostAsync(uri, placeContent);
                Console.WriteLine(response);
                if (response.IsSuccessStatusCode)
                {
                    //var result = await response.Content.ReadAsAsync<bool>();
                    //return result;
                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);                
            }
            return false;
        }

        private static async Task<bool> DeletePlaceByIdAsync (string uri)
        {
            HttpClient client = new HttpClient();
            try
            {
                var response = await client.DeleteAsync(uri);
                Console.WriteLine(response);
                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return false;
        }
    }
}