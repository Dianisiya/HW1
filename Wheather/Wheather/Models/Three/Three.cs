using System.Collections.Generic;

namespace Wheather.Models.Three
{

    public class Three
    {
        public City City { get; set; }
        public string Cod { get; set; }
        public float Message { get; set; }
        public int Cnt { get; set; }
        public List[] List { get; set; }
    }

    public class City
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Coord Coord { get; set; }
        public string Country { get; set; }
        public int Population { get; set; }
    }

    public class Coord
    {
        public float Lon { get; set; }
        public float Lat { get; set; }
    }

    public class List
    {
        public int Dt { get; set; }
        public Temp Temp { get; set; }
        public float Pressure { get; set; }
        public int Humidity { get; set; }
        public Weather[] Weather { get; set; }
        public float Speed { get; set; }
        public int Deg { get; set; }
        public int Clouds { get; set; }
        public float Rain { get; set; }
    }

    public class Temp
    {
        public float Day { get; set; }
        public float Min { get; set; }
        public float Max { get; set; }
        public float Night { get; set; }
        public float Eve { get; set; }
        public float Morn { get; set; }
    }

    public class Weather
    {
        public int Id { get; set; }
        public string Main { get; set; }
        public string Description { get; set; }
        public string Icon { get; set; }
    }
}