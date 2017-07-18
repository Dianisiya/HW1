namespace Wheather.Models.Db
{
    using System;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Weather :BaseDbEntity<int>
    {
        public string City { get; set; }

        public DateTime DateTime { get; set; }

        public string IconNumber { get; set; }

        public float Min { get; set; }
        
        public float Max { get; set; }
        
        [ForeignKey("Action")]
        public int ActionId { get; set; }

        public Action Action { get; set; }
    }
}