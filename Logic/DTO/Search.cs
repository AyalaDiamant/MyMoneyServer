using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.DTO
{
    public class Search
    {
        public int Type { get; set; }
        public int? User2AreaId { get; set; }
        public int? PayOptionId { get; set; }

        public DateTime? From { get; set; }
        public DateTime? To { get; set; }
        public bool IsToFullMaaser { get; set; }



    }
}
