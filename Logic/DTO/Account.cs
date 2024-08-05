using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.DTO
{
    public class JWTResponseToken
    {
        public int Id { get; set; }
        public string Token { get; set; }
        public DateTime Expiration { get; set; }
        public JWT JWT { get; set; }
        public string AllowAllPermissions { get; set; }
        public string FullNameUser { get; set; }
        public string ElectionCycleId { get; set; }
    }

    public class JWT
    {
        public string Aud { get; set; }
        public long Exp { get; set; }
        public string Iss { get; set; }
    }
}
