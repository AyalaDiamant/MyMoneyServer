using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.DTO
{
    public class UserDTO
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string Password { get; set; }
        public bool IsActive { get; set; }
        public IdName UserType { get; set; }
        public JWTResponseToken Token { get; set; }
        public bool IsYearlyPay { get; set; }
        public DateTime RegisterDate { get; set; }
        public DateTime? PayDate { get; set; }
        public IdName Lender { get; set; }
        public IdName Manager { get; set; }
        public bool Isbusiness { get; set; }


    }

    public class LenderParams
    {
        public int oldLender { get; set; }
        public int userType { get; set; }
        public int? newLender { get; set; }
    }

    public class UserSerach
    {
        public IdName usersType { get; set; }
        public IdName usersUnderLender { get; set; }
        public IdName lendersUnderManager { get; set; }

        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        
    }

}
