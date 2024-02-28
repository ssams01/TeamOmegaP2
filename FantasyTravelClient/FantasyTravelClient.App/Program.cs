using System;
using System.Net.Http;
using System.Text.Json;
using FantasyTravelClient.DTO;

namespace FantasyTravelClient.App
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            Console.WriteLine("Your journey has begun");

            

            TravelMenu tmpMenu = new TravelMenu();

            tmpMenu.Menu();

            Console.WriteLine("Thank you for taking us along for the journey. See you next time!");
        }

        

       
    }
}
