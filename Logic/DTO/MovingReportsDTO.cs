using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.DTO
{
    public class MovingReportsDTO
    {
        
        public string Month { get; set; }
        public string Year { get; set; }
        public double? Expenses { get; set; }
        public double? Revenues { get; set; }
    }
}
