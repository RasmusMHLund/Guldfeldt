using Guldfeldt.Persistence;
using Guldfeldt.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Guldfeldt.View
{
    /// <summary>
    /// Interaction logic for AddEmployee.xaml
    /// </summary>
    public partial class AddEmployee : Window
    {
        public AddEmployee()
        {
            InitializeComponent();
            
        }

        private void Cancel_Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Add_Button_Click(object sender, RoutedEventArgs e)
        {
            //if (Apprentice_CheckBox.IsChecked == true)
            //{
            //    avm.Name = FullName_TextBox.Text;
            //    avm.PhoneNumber = int.Parse(PhoneNumber_TextBox.Text);
            //    avm.Email = Email_TextBox.Text;
            //    avm.SalaryNumber = int.Parse(PhoneNumber_TextBox.Text);
            //    avm.SocialSecurityNumber = SocialSecurityNumber_TextBox.Text;


            //    apprenticeRepo.Create(apprentice);
            //}
            //else if (Journeyman_CheckBox.IsChecked == true)
            //{
            //    jvm.Name = FullName_TextBox.Text;
            //    jvm.PhoneNumber = int.Parse(PhoneNumber_TextBox.Text);
            //    jvm.Email = Email_TextBox.Text;
            //    jvm.SalaryNumber = int.Parse(PhoneNumber_TextBox.Text);
            //    jvm.SocialSecurityNumber = SocialSecurityNumber_TextBox.Text;
            //    jvm.MentorStatus = Mentor_Checkbox.IsChecked;

            //    journeymanRepo.Create(journeyman);
            //}
            //else if ((Apprentice_CheckBox.IsChecked & Journeyman_CheckBox.IsChecked) == false)
            //{
            //    try
            //    {
            //        MessageBox.Show("Vælg medarbejder status");
            //    }
            //    catch { }
            //}
        }
    }
}
