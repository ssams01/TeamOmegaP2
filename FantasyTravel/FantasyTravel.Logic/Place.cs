using System;

namespace FantasyTravel.Logic {
    public class Place {
        // fields
        public string name {get; set;}
        public int id {get; set;}
        private static int idSeed = 1;
        public string description {get; set;}
        public int language {get; set;}
        public int biomType {get; set;}
        //constructors
        public Place () {}
        public Place (int language, int biomType, string name = "", string description = "") {
            this.name = name;
            this.description = description;
            this.biomType = biomType;
            this.language = language;
            id = idSeed++;
        }
        //methods
        public override string ToString()
        {
            return $"Place\nName: {this.name}\nId: {this.id}\nDescription: {this.description}\biomType: {this.biomType}\n";
        }
    }
}


