using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Logic.DTO
{
    public class DebtDTO
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public int Payments { get; set; }
        public IdName Urgency { get; set; }
        public int UserId { get; set; }
        public bool IsActive { get; set; }
        public int Sum { get; set; }
        public Area Areaid { get; set; }
        //כרגע אין צורך ב UserId 
        //האדם שנכנס הוא בעל החוב, בהמשך יהיה גם מלווה שיוכל להכנס
    }

    public class debtSearchDetails
    {
        public string Description { get; set; }
        public string Payments { get; set; }
        public string Urgency { get; set; }
        public string Sum { get; set; }

    }
}
