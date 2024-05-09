using Guldfeldt.Model;
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
        MainViewModel mvm = new MainViewModel();
        EmployeeRepo er = new EmployeeRepo();

        public AddEmployee()
        {
            InitializeComponent();
            DataContext = mvm;

        }

        private void Cancel_Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Add_Button_Click(object sender, RoutedEventArgs e)
        {

            Employee employee = new Employee
            {
                FullName = FullName_TextBox.Text,
                PhoneNumber = int.Parse(PhoneNumber_TextBox.Text),
                Email = Email_TextBox.Text,
                SalaryNumber = int.Parse(SalaryNumber_TextBox.Text),
                CurrentWorkplace = CurrentWorkplace_TextBox.Text,
                SocialSecurityNumber = SocialSecurityNumber_TextBox.Text,
                IsApprentice = Apprentice_CheckBox.IsChecked,
                IsJourneyman = Journeyman_CheckBox.IsChecked,
                IsMentor = Mentor_Checkbox.IsChecked,
            };
            er.Create(employee);
            MessageBox.Show(" Medarbejder oprettet. ");
            Close();
        }

        private void Apprentice_CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            Journeyman_CheckBox.IsChecked = false;
            Mentor_Checkbox.Visibility = Visibility.Hidden;
            ApprenticeList_ComboBox.Visibility = Visibility.Hidden;
        }

        private void Journeyman_CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            Apprentice_CheckBox.IsChecked = false;
            Mentor_Checkbox.Visibility = Visibility.Visible;
            ApprenticeList_ComboBox.Visibility = Visibility.Visible;

        }

        private void Mentor_Checkbox_Checked(object sender, RoutedEventArgs e)
        {
            ApprenticeList_ComboBox.Visibility = Visibility.Visible;
        }
    }
}