namespace Wheather.Models.Db
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Action : BaseDbEntity<int>
    {
        public string Description { get; set; }

        public DateTime DateTime { get; set; }

        public ICollection<Weather> Result { get; set; }
    }
}