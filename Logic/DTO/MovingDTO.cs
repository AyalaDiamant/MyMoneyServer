using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.DTO
{
    public class MovingDTO
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public IdName UserArea { get; set; }
        public string Common { get; set; }
        public int Sum { get; set; }
        public int index { get; set; }
        public IdName PayOption { get; set; }
        public bool Duplicate { get; set; }
        public bool IsDeviation { get; set; }


    }
    //public class User2AreaDTO
    //{
    //    public int Id { get; set; }
    //    public IdName Area { get; set; }
    //}
}
