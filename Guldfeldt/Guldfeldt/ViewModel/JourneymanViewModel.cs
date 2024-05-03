using Guldfeldt.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Guldfeldt.ViewModel
{
    public  class JourneymanViewModel
    {
        public string Name { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string SocialSecurityNumber { get; set; }
        public string Email { get; set; }
        public int PhoneNumber { get; set; }
        public int SalaryNumber { get; set; }
        public bool? MentorStatus { get; set; }

        public JourneymanViewModel(Journeyman journeyman)
        {
            Name = journeyman.Name;
            DateOfBirth = journeyman.DateOfBirth;
            SocialSecurityNumber = journeyman.SocialSecurityNumber;
            Email = journeyman.Email;
            PhoneNumber = journeyman.PhoneNumber;
            SalaryNumber = journeyman.SalaryNumber;
            MentorStatus = journeyman.MentorStatus;
        }
    }
}
