namespace FantasyTravel.Logic {
    public class Biome {
        public int id {get; set;}
        public  int x {get; set;}
        public int y {get; set;}

        public Biome () {}

        public Biome (int id, int x, int y) {
            this.id = id;
            this.x = x;
            this.y = y;
        }
    }

}