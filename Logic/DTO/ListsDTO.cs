using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.DTO
{
    public class ListsDTO
    {
        public List<IdName> UserTypes { get; set; }
        public List<IdName> Cities { get; set; }
        public List<IdName> Areas { get; set; }
        public List<IdName> UrgencyDebts { get; set; }
        public List<IdName> Lenders { get;set; }
        public List<IdName> Managers { get; set; }
        public List<IdName> Statuses { get; set; }

        public List<IdName> PayOption { get; set; }
    }
}
