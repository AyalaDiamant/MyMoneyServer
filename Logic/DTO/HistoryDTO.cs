using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.DTO
{
    public class HistoryDTO
    {
        public int Id { get; set; }
        public int? UserId { get; set; }
        public DateTime? DateofChange { get; set; }
        public string? OldDomain { get; set; }
        public string? NewDomain { get; set; }
        public int? OldAmount { get; set; }
        public int? NewAmount { get; set; }
        public ActionOptions ActionOption { get; set; }

    }
}
