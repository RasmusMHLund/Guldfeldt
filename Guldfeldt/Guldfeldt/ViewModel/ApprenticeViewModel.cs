using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Guldfeldt.Model;
using Guldfeldt.Persistence;

namespace Guldfeldt.ViewModel
{
    public class ApprenticeViewModel
    {
        ApprenticeRepo apprenticeRepo = new ApprenticeRepo();
        public string Name { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string SocialSecurityNumber { get; set; }
        public string Email { get; set; }
        public int PhoneNumber { get; set; }
        public int SalaryNumber { get; set; }

        public ApprenticeViewModel(Apprentice apprentice)
        {
            Name = apprentice.Name;
            DateOfBirth = apprentice.DateOfBirth;
            SocialSecurityNumber = apprentice.SocialSecurityNumber;
            Email = apprentice.Email;
            PhoneNumber = apprentice.PhoneNumber;
            SalaryNumber = apprentice.SalaryNumber;
        }
    }
}
