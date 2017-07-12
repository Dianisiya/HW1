using System.Collections.Generic;
using Wheather.Models.Db;

namespace Wheather.Models
{
    public class IndexModel
    {
        public IEnumerable<string> Cities { get; set; }
    }
}