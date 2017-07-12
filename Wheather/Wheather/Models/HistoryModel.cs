using System.Collections.Generic;
using Wheather.Models.Db;

namespace Wheather.Models
{

    public class HistoryModel
    {
        public IEnumerable<Action> History { get; set; }
    }
}