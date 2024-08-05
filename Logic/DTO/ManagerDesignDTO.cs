using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.DTO
{
    public class ManagerDesignDTO
    {
        public int Id { get; set; }
        public int ManagerId { get; set; }
        public byte[]? ImageContent { get; set; }
        public string Title { get; set; }
        public string Slogan { get; set; }
        public string HeaderColor { get; set; }
        public string? TextColor { get; set; }
        public string? Src { get; set; }

        public string? FileName { get; set; }

        //רחל הוסיפה
        public IFormFile? File { get; set; }
    }

}
