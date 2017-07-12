namespace Wheather.Models.Db
{
    using System.ComponentModel.DataAnnotations.Schema;

    public class City
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Name { get; set; }
    }
}