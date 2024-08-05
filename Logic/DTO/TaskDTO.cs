using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.DTO
{
    public class TaskDTO
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public int UserId { get; set; }
        public IdName Status { get; set; }
        public DateTime CreateDate { get; set; }
        public string Comments { get; set; }
        public IdName Urgency { get; set; }
        public DateTime? DoDate { get; set; }

    }

    public class TaskSearch
    {
        public string Description { get; set; }
        public string Comments { get; set; }
        public string Urgency { get; set; }
        public string Status { get; set; }
        public DateTime CreateDate { get; set; }

        public DateTime DoDate { get; set; }

    }


}
