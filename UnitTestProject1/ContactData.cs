using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumTests
{
    public class ContactData
    {
        public ContactData(string firstname, string middlename, string lastname) 
        {
            Firstname = firstname;
            Middlename = middlename;
            Lastname = lastname;
        }

        public string Firstname { get; set; }

        public string Middlename { get; set; }

        public string Lastname { get; set; }

    }
}
