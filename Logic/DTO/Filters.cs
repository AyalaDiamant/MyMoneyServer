using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Logic.DTO
{
    public class Filters
    {
        public List<AreaDTO> Areas { get; set; }
        public List<IdName> PayOptions { get; set; }
    }
}
