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
            Console.WriteLine("--------------------------------");
            Console.WriteLine("FantasyTravel client starting...");
            Console.WriteLine("--------------------------------");

            await TravelMenu.Menu();

            Console.WriteLine("-------------------------------");
            Console.WriteLine("FantasyTravel client closing...");
            Console.WriteLine("-------------------------------");
        }
    }
}