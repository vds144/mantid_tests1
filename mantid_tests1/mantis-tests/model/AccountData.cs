using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mantis_tests
{
    public class AccountData
    {
        public AccountData(string name, string password)
        {
            this.Name = name;
            this.Password = password;
        }

        public AccountData()
        {
        }
        public string Name { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
    }
}
