using System;
using System.Collections.Generic;

namespace Weather.UwpApp.Models
{
    public class HistoryItem
    {
        public string Description { get; set; }

        public DateTime DateTime { get; set; }

        public ICollection<Weather> Result { get; set; }
    }
}
