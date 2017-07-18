namespace Wheather.Models.Db
{
    using System.ComponentModel.DataAnnotations.Schema;

    public class City : BaseDbEntity<int>
    {

        public string Name { get; set; }
    }
}