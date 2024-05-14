using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLibrary
{
    public class Admin : User
    {
        private static int increment = 0;

        public Admin()
        {
            UserID = increment++;
        }
        public Admin(string userName, string password) : base(userName, password)
        {
            UserID = increment++;

            UserName = userName;

            Password = password;

            Role = Role.Admin;
        }
    }
}
