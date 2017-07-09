namespace Wheather.Models
{
    public class Now
    {
        public Coord1 Coord { get; set; }
        public Weather1[] Weather { get; set; }
        public string _base { get; set; }
        public Main1 Main { get; set; }
        public int visibility { get; set; }
        public Wind1 wind { get; set; }
        public Clouds1 clouds { get; set; }
        public int dt { get; set; }
        public Sys1 sys { get; set; }
        public int id { get; set; }
        public string name { get; set; }
        public int cod { get; set; }
    }

    public class Coord1
    {
        public float lon { get; set; }
        public float lat { get; set; }
    }

    public class Main1
    {
        public float temp { get; set; }
        public float pressure { get; set; }
        public float humidity { get; set; }
        public float temp_min { get; set; }
        public float temp_max { get; set; }
    }

    public class Wind1
    {
        public float speed { get; set; }
        public float deg { get; set; }
    }

    public class Clouds1
    {
        public int all { get; set; }
    }

    public class Sys1
    {
        public int type { get; set; }
        public int id { get; set; }
        public float message { get; set; }
        public string country { get; set; }
        public float sunrise { get; set; }
        public float sunset { get; set; }
    }

    public class Weather1
    {
        public int id { get; set; }
        public string main { get; set; }
        public string description { get; set; }
        public string icon { get; set; }
    }

}