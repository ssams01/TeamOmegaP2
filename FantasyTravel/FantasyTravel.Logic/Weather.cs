using System;

namespace FantasyTravel.Logic {
    public class Weather {
        // fields
          public DateOnly Date { get; set; }

          public int TemperatureC { get; set; }

          public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);

          public string? Summary { get; set; }
        
        //constructors
          public Weather(DateOnly date, int TemperatureC, string? summary) {
            this.Date = date;
            this.TemperatureC = TemperatureC;
            this.TemperatureF = 32 + (int)(this.TemperatureC/0.5556);
            this.Summary = summary;
          }

        //methods

        //could change like remove date maybe, and add Summary
        public override string ToString() {
            return $"Today's Date: {this.Date}\nTemperatuer (F): {this.TemperatureF}";
        }
    }
}
