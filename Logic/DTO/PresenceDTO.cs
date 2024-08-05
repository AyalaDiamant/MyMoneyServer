using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.DTO
{
    public class PresenceDTO
    {
        public int Id { get; set; }
        public DateTime Start { get; set; }
        public DateTime Finish { get; set; }
        public string Note { get; set; }
        public int UserId { get; set; }
    }
    public class PresenceSearch
    {
        public DateTime? From { get; set; }
        public DateTime? To { get; set; }
    }
}
