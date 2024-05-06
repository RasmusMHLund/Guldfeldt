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

namespace Guldfeldt.View
{
    /// <summary>
    /// Interaction logic for EmployeeList.xaml
    /// </summary>
    public partial class EmployeeList : Window
    {
        private EmployeeViewModel evm;
        SqlConnection con = new SqlConnection("Data Source = 10.56.8.35; Initial Catalog = DB_2024_72; Persist Security Info=True;User ID = STUDENT_2024_72; Password=OPENDB_72;Encrypt=True;Trust Server Certificate=True");
        public EmployeeList()
        {
            InitializeComponent();
            evm = new EmployeeViewModel();
            DataContext = evm;

        }

        private void CreateWorkplace_Button_Click(object sender, RoutedEventArgs e)
        {
            AddWorkplace addWorkplace = new AddWorkplace();
            addWorkplace.ShowDialog();
        }

        private void AddEmployee_Button_Click(object sender, RoutedEventArgs e)
        {
            AddEmployee addEmployee = new AddEmployee();
            addEmployee.ShowDialog();
        }

        private void NoteList_Button_Click(object sender, RoutedEventArgs e)
        {
            NoteList noteList = new NoteList();
            noteList.ShowDialog();
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
                    case "Arbejdsplads":
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
            ChooseWorkplace_ComboBox.IsEnabled = false;
            WorkplaceSchedule_Button.IsEnabled = false;
            CreateWorkplace_Button.IsEnabled = false;
            EditWorkplace_Button.IsEnabled = false;
            ChooseWorkplace_ComboBox.Visibility = Visibility.Collapsed;
            WorkplaceSchedule_Button.Visibility = Visibility.Collapsed;
            CreateWorkplace_Button.Visibility = Visibility.Collapsed;
            EditWorkplace_Button.Visibility = Visibility.Collapsed;
        }
        private void EnableWorkplaceControls()
        {
            ChooseWorkplace_ComboBox.IsEnabled = true;
            WorkplaceSchedule_Button.IsEnabled = true;
            CreateWorkplace_Button.IsEnabled = true;
            EditWorkplace_Button.IsEnabled = true;
            ChooseWorkplace_ComboBox.Visibility = Visibility.Visible;
            WorkplaceSchedule_Button.Visibility = Visibility.Visible;
            CreateWorkplace_Button.Visibility = Visibility.Visible;
            EditWorkplace_Button.Visibility = Visibility.Visible;
        }

        private void Apprentice_CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            
            Mentor_Checkbox.Visibility = Visibility.Hidden;
            ApprenticeList_ComboBox.Visibility = Visibility.Hidden;
        }
        private void Journeyman_CheckBox_Checked(object sender, RoutedEventArgs e)
        {
           
            Mentor_Checkbox.Visibility = Visibility.Visible;
            ApprenticeList_ComboBox.Visibility = Visibility.Visible;
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

            MessageBox.Show("Ændring gemt.");
        }

        private void EmployeeList_ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //var listBox = sender as ListBox;
            //var selectedEmployeeName = listBox.SelectedItem as string;

            //if (selectedEmployeeName != null)
            //{
            //    using (SqlConnection con = new SqlConnection("Data Source=10.56.8.35;Initial Catalog=DB_2024_72;Persist Security Info=True;User ID=STUDENT_2024_72;Password=OPENDB_72;Encrypt=True;Trust Server Certificate=True"))
            //    {
            //        con.Open();
            //        string query = $"SELECT * FROM EMPLOYEE WHERE FullName = '{selectedEmployeeName}'";
            //        using (SqlCommand cmd = new SqlCommand(query, con))
            //        {
            //            using (SqlDataReader dr = cmd.ExecuteReader())
            //            {
            //                if (dr.Read())
            //                {
            //                    FullName_TextBox.Text = dr["FullName"].ToString();
            //                    PhoneNumber_TextBox.Text = dr["PhoneNumber"].ToString();
            //                    Email_TextBox.Text = dr["Email"].ToString();
            //                    SalaryNumber_TextBox.Text = dr["SalaryNumber"].ToString();
            //                    CurrentWorkplace_TextBox.Text = dr["CurrentWorkplace"].ToString();
            //                    SocialSecurityNumber_TextBox.Text = dr["SocialSecurityNumber"].ToString();
            //                    Apprentice_CheckBox.IsChecked = (bool)dr["IsApprentice"];
            //                    Journeyman_CheckBox.IsChecked = (bool)dr["IsJourneyman"];
            //                    Mentor_Checkbox.IsChecked = (bool)dr["IsMentor"];
            //                }
            //            }
            //        }
            //    }
            //}
        }

    }
    }

