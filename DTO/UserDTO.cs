using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class UserDTO
    {
        public long IDUser { get; set; }
        public int RoleID { get; set; }
        public string FullName { get; set; }
        public string Login { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }


        public override string ToString()
        {
            string str =  "ID: " + IDUser.ToString() + " Name: " + FullName +
                " Lg: " + Login + " Em: " + Email;
            return str;
        }
    }
}