using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Guldfeldt.Model
{
    public class Journeyman
    {
        public int Id { get; set; }
        public string Name { get; set; }  
        public DateTime? DateOfBirth { get; set; }
        public string SocialSecurityNumber { get; set; }
        public string Email {  get; set; }
        public int PhoneNumber { get; set; }
        public bool? MentorStatus { get; set; }
        public int SalaryNumber { get; set; }

        public Journeyman(string name, DateTime? dateOfBirth, string socialSecurityNumber, string email, int phoneNumber, bool? mentorStatus, int salaryNumber ) 
        { 
            Name = name;
            DateOfBirth = dateOfBirth;
            SocialSecurityNumber = socialSecurityNumber;
            Email = email;
            PhoneNumber = phoneNumber;
            MentorStatus = mentorStatus;
            SalaryNumber = salaryNumber;
        }
        public Journeyman() : this(null, null, null, null, 0, false, 0)
        {

        }
    }
}
