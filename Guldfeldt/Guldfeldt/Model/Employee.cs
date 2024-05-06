using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace Guldfeldt.Model
{
    public class Employee
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public int PhoneNumber { get; set; }
        public string Email { get; set; }
        public int SalaryNumber { get; set; }
        public string CurrentWorkplace { get; set; }
        public string SocialSecurityNumber { get; set; }
        public bool? IsApprentice { get; set; }
        public bool? IsJourneyman { get; set; }
        public bool? IsMentor { get; set; }


        public Employee(string fullName, int phoneNumber, string email, int salaryNumber, string currentWorkplace, string socialSecurityNumber, bool? isApprentice, bool? isJourneyman, bool? isMentor)
        {
            FullName = fullName;
            PhoneNumber = phoneNumber;
            Email = email;
            SalaryNumber = salaryNumber;
            CurrentWorkplace = currentWorkplace;
            SocialSecurityNumber = socialSecurityNumber;
            IsApprentice = isApprentice;
            IsJourneyman = isJourneyman;
            IsMentor = isMentor;

        }
        public Employee() : this(null, 0, null, 0, null, null, false, false, false)
        {

        }
    }
}
