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
            ApplyHoverEffect(Add_Button, defaultbrush, hoverbrush);
            ApplyHoverEffect(Cancel_Button, defaultbrush, hoverbrush);
        }
        SolidColorBrush defaultbrush = new SolidColorBrush(System.Windows.Media.Color.FromArgb(255, 94, 91, 91));
        SolidColorBrush hoverbrush = new SolidColorBrush(System.Windows.Media.Color.FromArgb(255, 136, 133, 133));
        private void ApplyHoverEffect(Button button, SolidColorBrush defaultBrush, SolidColorBrush hoverBrush)
        {
            button.Background = defaultBrush;
            button.MouseEnter += (sender, e) =>
            {
                button.Background = hoverBrush;
            };
            button.MouseLeave += (sender, e) =>
            {
                button.Background = defaultBrush;
            };
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
                CurrentWorkplace = CurrentWorkplace_ComboBox.Text,
                SocialSecurityNumber = SocialSecurityNumber_TextBox.Text,
                IsApprentice = Apprentice_CheckBox.IsChecked,
                IsJourneyman = Journeyman_CheckBox.IsChecked,
                IsMentor = Mentor_Checkbox.IsChecked,
            };
            er.Create(employee);
            MessageBox.Show(" Medarbejder oprettet. ");
            Close();
        }
        private void TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox? textBox = sender as TextBox;
            mvm.SelectedBox = textBox?.Name;

            string final = "";
            foreach (char c in textBox.Text)
            {
                if (Char.IsDigit(c))
                {
                    final += c;
                }
                else
                {
                    string error = "Forkert input. Kun tal accepteret.";
                    MessageBox.Show(error);
                }
            }
            List<int> salaryNumbers = new List<int>();
            foreach (var employee in er.GetEmployees())
            {
                salaryNumbers.Add(employee.SalaryNumber);
            }
            int salaryNumber;
            bool isNumber = int.TryParse(textBox.Text, out salaryNumber);

                if (isNumber && salaryNumbers.Contains(salaryNumber))
                {
                    string error = " Lønnummeret eksisterer allerede. ";
                    MessageBox.Show(error);
                }
                else
                {
                    MessageBox.Show("Salary number not found or invalid input.");
                }
            }
        
        private void Apprentice_CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            Journeyman_CheckBox.IsChecked = false;
            Mentor_Checkbox.Visibility = Visibility.Hidden;
        }

        private void Journeyman_CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            Apprentice_CheckBox.IsChecked = false;
            Mentor_Checkbox.Visibility = Visibility.Visible;

        }


    }
}