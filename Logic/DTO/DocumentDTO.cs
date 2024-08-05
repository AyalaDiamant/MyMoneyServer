using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.DTO
{
    public class DocumentDTO
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public byte[] Content { get; set; }
        public string ContentType { get; set; }
        public string Description { get; set; }
        public string FileName { get; set; }
        public string Src { get; set; }
    }

    public class FileFromClient
    {
        public IFormFile File { get; set; }
        public string Description { get; set; }

    }
    public class SerchDocument
    {
        public string  Name{ get; set; }
        public string Description { get; set; }

    }
}
