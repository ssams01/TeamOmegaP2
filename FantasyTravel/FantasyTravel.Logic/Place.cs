using System;

namespace FantasyTravel.Logic {
    public class Place {
        // fields
        public string name {get; set;}
        public int id {get; set;}
        private static int idSeed = 1;
        public string description {get; set;}
        public List<string> languages {get; set;}
        public int biomType {get; set;}
        //constructors
        public Place () {}
        public Place (List<string> languages, int biomType, string name = "", string description = "") {
            this.name = name;
            this.description = description;
            this.biomType = biomType;
            this.languages = languages;
            id = idSeed++;
        }
        //methods
        public override string ToString()
        {
            return $"Place\nName: {this.name}\nId: {this.id}\nDescription: {this.description}\biomType: {this.biomType}\n";
        }
    }
}


