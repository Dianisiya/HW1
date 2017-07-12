namespace Wheather.Models.Db
{
    using System;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Action
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Description { get; set; }

        public DateTime DateTime { get; set; }
    }
}