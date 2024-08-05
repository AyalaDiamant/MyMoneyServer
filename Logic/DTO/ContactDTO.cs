using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.DTO
{
    public class ContactDTO
    {
        public string Subject { get; set; }
        public string Name { get; set; }
        public string From { get; set; }
        public string Body { get; set; }
        public List<string> mails { get; set; }
        public ContactDTO()
        {
            this.mails = new List<string>();
        }

    }
}
