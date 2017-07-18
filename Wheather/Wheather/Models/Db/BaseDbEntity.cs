namespace Wheather.Models.Db
{
    using System.ComponentModel.DataAnnotations.Schema;

    public class BaseDbEntity<TKey>
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public TKey Id { get; set; }
    }
}