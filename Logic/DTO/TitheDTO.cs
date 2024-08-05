using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.DTO
{
    public class TitheDTO
    {
        public int SumOfExpenses { get; set; }


        public int SumOfRevenues { get; set; }

        public DateTime DateTithe { get; set; }

    }
    public class SerchTitheDTO
    {
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public bool AllDate { get; set; }
        public bool IsFullMonth { get; set; }



    }
    public class TithesDataDTO
    {
        public List<TitheDTO> TitheList { get; set; }
        public bool SkipMonth { get; set; }

    }
}
