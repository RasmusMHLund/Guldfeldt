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

        SqlConnection con = new SqlConnection("Data Source = 10.56.8.35; Initial Catalog = DB_2024_72; Persist Security Info=True;User ID = STUDENT_2024_72; Password=OPENDB_72;Encrypt=True;Trust Server Certificate=True");
        public EmployeeList()
        {
            InitializeComponent();

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
                EmployeeList_ListBox.Items.Clear();

                using (SqlConnection con = new SqlConnection("Data Source=10.56.8.35;Initial Catalog=DB_2024_72;Persist Security Info=True;User ID=STUDENT_2024_72;Password=OPENDB_72;Encrypt=True;Trust Server Certificate=True"))
                {
                    con.Open();

                    string query = "";
                    switch (PickList_ComboBox.SelectedItem)
                    {
                        case ComboBoxItem item1 when item1.Name == "_item1":
                            query = "SELECT Name FROM APPRENTICE UNION SELECT Name FROM JOURNEYMAN";
                            DisableWorkplaceControls();
                            break;
                        case ComboBoxItem item2 when item2.Name == "_item2":
                            query = "SELECT * FROM APPRENTICE";
                            DisableWorkplaceControls();
                            break;
                        case ComboBoxItem item3 when item3.Name == "_item3":
                            query = "SELECT * FROM JOURNEYMAN";
                            DisableWorkplaceControls();
                            break;
                        case ComboBoxItem item4 when item4.Name == "_item4":
                            query = "SELECT * FROM JOURNEYMAN WHERE MentorStatus = 'True'";
                            DisableWorkplaceControls();
                            break;
                        case ComboBoxItem item5 when item5.Name == "_item5":
                            query = "SELECT * FROM APPRENTICE";
                            EnableWorkplaceControls();

                            break;
                    }
                    if (!string.IsNullOrEmpty(query))
                    {
                        using (con) { 
                        using (SqlCommand cmd = new SqlCommand(query, con))
                        {
                            using (SqlDataReader dr = cmd.ExecuteReader())
                            {
                                while (dr.Read())
                                {
                                        EmployeeList_ListBox.Items.Add(dr["Name"]);
                                }
                            }
                        }
                    }
                    }
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

        private void Journeyman_CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            Mentor_Checkbox.IsEnabled = true;
            Mentor_Checkbox.Visibility = Visibility.Visible;
        }

        private void Mentor_Checkbox_Checked(object sender, RoutedEventArgs e)
        {
            ApprenticeList_ComboBox.IsEnabled = true;
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

            Save_Button.IsEnabled = false;
            Save_Button.Visibility = Visibility.Hidden;

            MessageBox.Show("Ændring gemt.");
        }

        private void EmployeeList_ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
        }
    }
}