using System;

namespace FantasyTravelClient.DTO
{
    public class Place
    {
        // fields
        public string name { get; set; }
        public int id { get; set; }
        private static int idSeed = 1;
        public string description { get; set; }
        public int language { get; set; }
        public int biomeType { get; set; }
        //constructors
        public Place() { }
        public Place(int language, int biomeType, string name = "", string description = "")
        {
            this.name = name;
            this.description = description;
            this.biomeType = biomeType;
            this.language = language;
            id = idSeed++;
        }
        public Place(int id, int language, int biomeType, string name = "", string description = "")
        {
            this.name = name;
            this.description = description;
            this.biomeType = biomeType;
            this.language = language;
            this.id = id;
        }
        //methods
        public override string ToString()
        {
            return $"Place\nName: {this.name}\nId: {this.id}\nDescription: {this.description}\bBiomeType: {this.biomeType}\n";
        }
    }
}
