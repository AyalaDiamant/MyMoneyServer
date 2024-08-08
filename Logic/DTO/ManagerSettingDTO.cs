using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.DTO
{
    public class ManagerSettingDTO
    {
        public int Id { get; set; }

        public int ManagerId { get; set; }

        public string? OrganizationName { get; set; }

        public string? Phon { get; set; }

        public string? Email { get; set; }

        public string? Adress { get; set; }

        public string? ContactMen { get; set; }

        public int? OrganizationType { get; set; }

    }

}
