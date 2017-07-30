using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Weather.UwpApp.Models
{
    public class Weather
    {
        public string City { get; set; }

        public string Date { get; set; }

        public float Min { get; set; }

        public float Max { get; set; }

        public string Icon { get; set; }
    }
}
