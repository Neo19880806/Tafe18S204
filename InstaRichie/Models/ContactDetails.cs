
/*
Author Daniel Heath Bialous.
Rev 1.0.0.0 for Contact Details cs.
*/


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite.Net.Attributes;

namespace StartFinance.Models
{
    class ContactDetails
    {

        [PrimaryKey, AutoIncrement]
        public int contactID { get; set; }


        public string firstName { get; set; }


        public string lastName { get; set; }

        public string companyName { get; set; }
        public string mobilePhone { get; set; }
        public string email { get; set; }
        //Contact Details(e.g.ContactID, FirstName, LastName, CompanyName, MobilePhone)

    }
}
