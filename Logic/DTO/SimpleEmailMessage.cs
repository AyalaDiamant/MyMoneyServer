using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.DTO
{
    public class SimpleEmailMessage
    {
        public string From { get; set; }
        public string ReplayTo { get; set; }
        public List<string> To { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public bool IsBodyHtml { get; set; }
        public List<MailAttachment> Attachments { get; set; } = new List<MailAttachment>();
    }

    public class MailAttachment
    {
        public MemoryStream Attachment { get; set; }
        public string FileName { get; set; }
    }
}
