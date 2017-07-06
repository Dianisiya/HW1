using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Wheather.Models
{
    public class Seven
    {
        public string cod { get; set; }
        public double message { get; set; }
        public int cnt { get; set; }
        public List<List> list { get; set; }
        public City city { get; set; }
    }
    public class Main2
    {
        public double temp { get; set; }
        public double temp_min { get; set; }
        public double temp_max { get; set; }
        public double pressure { get; set; }
        public double sea_level { get; set; }
        public double grnd_level { get; set; }
        public int humidity { get; set; }
        public double temp_kf { get; set; }
    }

    public class Weather2
    {
        public int id { get; set; }
        public string main { get; set; }
        public string description { get; set; }
        public string icon { get; set; }
    }

    public class Clouds2
    {
        public int all { get; set; }
    }

    public class Wind2
    {
        public double speed { get; set; }
        public double deg { get; set; }
    }

    public class Rain2
    {
    }

    public class Sys2
    {
        public string pod { get; set; }
    }

    public class List2
    {
        public int dt { get; set; }
        public Main2 main { get; set; }
        public List<Weather2> weather { get; set; }
        public Clouds2 clouds { get; set; }
        public Wind2 wind { get; set; }
        public Rain2 rain { get; set; }
        public Sys2 sys { get; set; }
        public string dt_txt { get; set; }
    }

    public class Coord2
    {
        public double lat { get; set; }
        public double lon { get; set; }
    }

    public class City2
    {
        public int id { get; set; }
        public string name { get; set; }
        public Coord coord { get; set; }
        public string country { get; set; }
    }


}