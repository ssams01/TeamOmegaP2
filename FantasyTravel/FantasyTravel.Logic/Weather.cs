using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FantasyTravel.Logic
{
    public class Weather
    {
        public double temperature {get; set;}
        public int biomeType {get; set;}

        public Weather () {}

        public Weather (double temperature, int biomeType)
        {
            this.temperature = temperature;
            this.biomeType = biomeType;
        }

        public string ToString ()
        {
            return $"BiomeType: {this.biomeType}\nTemperature: {this.temperature}";
        }
    }
}
