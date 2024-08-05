using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.DTO
{
    public class Tithe
    {
        public int SumOfExpenses { get; set; }


        public int SumOfRevenues { get; set; }

        public DateTime DateTithe { get; set; }

    }
    public class SerchTithe
    {
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }

        public bool AllDate { get; set; }


    }
}
