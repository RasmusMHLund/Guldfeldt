using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Guldfeldt.Model
{
    public class Apprentice
    {
        public string Name { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string SocialSecurityNumber { get; set; }
        public string Email {  get; set; }
        public int PhoneNumber { get; set; }
        public int SalaryNumber { get; set; }

        public Apprentice(string name, DateTime dateOfBirth, string socialSecurityNumber, string email, int phoneNumber, int salaryNumber)
        {
            this.Name = name;
            this.DateOfBirth = dateOfBirth;
            this.SocialSecurityNumber = socialSecurityNumber;
            this.Email = email;
            this.PhoneNumber = phoneNumber;
            this.SalaryNumber = salaryNumber;
        }
    }
}
