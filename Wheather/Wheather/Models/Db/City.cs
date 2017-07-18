namespace Wheather.Models.Db
{
    using System.ComponentModel.DataAnnotations.Schema;

    public class City : BaseDbEntity<int>
    {
        public int Id { get; internal set; }
        public string Name { get; set; }
    }
}