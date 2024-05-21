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
using Microsoft.Data.SqlClient;
using Guldfeldt.View.Notes;
using System.Diagnostics.Eventing.Reader;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualBasic.ApplicationServices;
using System.Drawing;
using Guldfeldt.Persistence;

namespace Guldfeldt.View
{
        //SqlConnection con = new SqlConnection("Data Source = 10.56.8.35; Initial Catalog = DB_2024_72; Persist Security Info=True;User ID = STUDENT_2024_72; Password=OPENDB_72;Encrypt=True;Trust Server Certificate=True");
    public partial class EmployeeList : Window
    {
        MainViewModel mvm = new MainViewModel();
        EmployeeRepo er = new EmployeeRepo();
        LocationRepo lr = new LocationRepo();

        public EmployeeList()
        {
            InitializeComponent();
            DataContext = mvm;
        }

        private void CreateLocation_Button_Click(object sender, RoutedEventArgs e)
        {
            AddLocation addLocation = new AddLocation();
            addLocation.ShowDialog();
            mvm.LoadLocationsFromDatabase();
        }

        private void AddEmployee_Button_Click(object sender, RoutedEventArgs e)
        {
            AddEmployee addEmployee = new AddEmployee();
            addEmployee.ShowDialog();
            mvm.LoadEmployeesFromDatabase();
            
        }

        private void DeleteEmployee_Button_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Bekræft sletning af medarbejder","Bekræft",MessageBoxButton.YesNo,MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
            er.Delete(mvm.SelectedEmployee);
            MessageBox.Show(" Medarbejder slettet. ");

            }
            mvm.LoadEmployeesFromDatabase();
        }
        private void NoteList_Button_Click(object sender, RoutedEventArgs e)
        {
            NoteList noteList = new NoteList();
            noteList.ShowDialog();
        }

        private void EditLocation_Button_Click(object sender, RoutedEventArgs e)
        {
            EditLocation editLocation = new EditLocation();
            editLocation.ShowDialog();
        }
        private void PickList_ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            if (PickList_ComboBox.SelectedItem != null)
            {
                switch (PickList_ComboBox.SelectedItem)
                {
                    case "Alle medarbejdere":
                        DisableWorkplaceControls();
                        break;
                    case "Alle lærlinge":
                        DisableWorkplaceControls();
                        break;
                    case "Alle svende":
                        DisableWorkplaceControls();
                        break;
                    case "Alle mentorer":
                        DisableWorkplaceControls();
                        break;
                    case "Lokation":
                        EnableWorkplaceControls();
                        break;
                }
            }
        }  
        private void EditEmployeeInformation_Button_Click(object sender, RoutedEventArgs e)
        {
            FullName_TextBox.IsEnabled = true;
            PhoneNumber_TextBox.IsEnabled = true;
            Email_TextBox.IsEnabled = true;
            SalaryNumber_TextBox.IsEnabled = true;
            CurrentWorkplace_TextBox.IsEnabled = true;
            SocialSecurityNumber_TextBox.IsEnabled = true;
            Apprentice_CheckBox.IsEnabled = true;
            Journeyman_CheckBox.IsEnabled = true;

            if (Journeyman_CheckBox.IsEnabled)
            {
                Mentor_Checkbox.IsEnabled = true;
                ApprenticeList_ComboBox.IsEnabled = true;
            }

            Save_Button.IsEnabled = true;
            Save_Button.Visibility = Visibility.Visible;

        }
        private void DisableWorkplaceControls()
        {
            ChooseLocation_ComboBox.IsEnabled = false;
            LocationSchedule_Button.IsEnabled = false;
            CreateLocation_Button.IsEnabled = false;
            EditLocation_Button.IsEnabled = false;
            ChooseLocation_ComboBox.Visibility = Visibility.Collapsed;
            LocationSchedule_Button.Visibility = Visibility.Collapsed;
            CreateLocation_Button.Visibility = Visibility.Collapsed;
            EditLocation_Button.Visibility = Visibility.Collapsed;
        }
        private void EnableWorkplaceControls()
        {
            ChooseLocation_ComboBox.IsEnabled = true;
            LocationSchedule_Button.IsEnabled = true;
            CreateLocation_Button.IsEnabled = true;
            EditLocation_Button.IsEnabled = true;
            ChooseLocation_ComboBox.Visibility = Visibility.Visible;
            LocationSchedule_Button.Visibility = Visibility.Visible;
            CreateLocation_Button.Visibility = Visibility.Visible;
            EditLocation_Button.Visibility = Visibility.Visible;
        }

        private void Apprentice_CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            
            Mentor_Checkbox.Visibility = Visibility.Hidden;
            ApprenticeList_ComboBox.Visibility = Visibility.Hidden;
            NoteList_Button.Visibility = Visibility.Visible;
        }
        private void Journeyman_CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            
            Mentor_Checkbox.Visibility = Visibility.Visible;
            ApprenticeList_ComboBox.Visibility = Visibility.Visible;
            NoteList_Button.Visibility = Visibility.Hidden;
        }

        private void Mentor_Checkbox_Checked(object sender, RoutedEventArgs e)
        {
            
            ApprenticeList_ComboBox.Visibility = Visibility.Visible;
        }

        private void Save_Button_Click(object sender, RoutedEventArgs e)
        {
            FullName_TextBox.IsEnabled = false;
            PhoneNumber_TextBox.IsEnabled = false;
            Email_TextBox.IsEnabled = false;
            SalaryNumber_TextBox.IsEnabled = false;
            CurrentWorkplace_TextBox.IsEnabled = false;
            SocialSecurityNumber_TextBox.IsEnabled = false;
            Apprentice_CheckBox.IsEnabled = false;
            Journeyman_CheckBox.IsEnabled = false;

            Mentor_Checkbox.IsEnabled = false;
            ApprenticeList_ComboBox.IsEnabled = false;

            Save_Button.IsEnabled = false;
            Save_Button.Visibility = Visibility.Hidden;

            MessageBox.Show(" Ændring gemt. ");
        }



        private void LocationSchedule_Button_Click(object sender, RoutedEventArgs e)
        {
            

            lr.Delete(mvm.SelectedLocation);
            MessageBox.Show(" Lokation slettet. ");
            mvm.LoadLocationsFromDatabase();

        }
    }
}

