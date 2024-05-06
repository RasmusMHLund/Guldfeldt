    using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.ComponentModel;
using Guldfeldt.Model;
using Microsoft.Data.SqlClient;
using Guldfeldt.View;
using System.Windows.Input;
using System.Runtime.CompilerServices;
using System.Windows.Controls;

namespace Guldfeldt.ViewModel
{
    public class EmployeeViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableCollection<string> PickListItems { get; } = new ObservableCollection<string>
        {
            "Alle medarbejdere",
            "Alle lærlinge",
            "Alle svende",
            "Alle mentorer",
            "Arbejdsplads"
        };
        public ICommand ComboBoxSelectionChangedCommand { get; }

        private ObservableCollection<Employee> _employees;
        public ObservableCollection<Employee> Employees
        {
            get { return _employees; }
            set { _employees = value; OnPropertyChanged(nameof(Employees)); }
        }
        private Employee _selectedEmployee;

        public Employee SelectedEmployee
        {
            get { return _selectedEmployee; }
            set { _selectedEmployee = value; OnPropertyChanged(nameof(SelectedEmployee)); }
        }

        private string _selectedPickListItem;

        public string SelectedPickListItem
        {
            get { return _selectedPickListItem; }
            set
            {
                _selectedPickListItem = value;
                OnPropertyChanged(nameof(SelectedPickListItem));
                LoadEmployeesFromDatabase();
            }
        }


        public EmployeeViewModel()
        {
            Employees = new ObservableCollection<Employee>();
            SelectedPickListItem = "Alle medarbejdere";
        }



        private void LoadEmployeesFromDatabase()
        {
            string connectionString = "Data Source = 10.56.8.35; Initial Catalog = DB_2024_72; Persist Security Info=True;User ID = STUDENT_2024_72; Password=OPENDB_72;Encrypt=True;Trust Server Certificate=True";
            string query = "SELECT * FROM EMPLOYEE";

            switch (SelectedPickListItem)
            {
                case "Alle medarbejdere":
                    query = "SELECT * FROM EMPLOYEE";
                    Employees.Clear();
                    break;
                case "Alle lærlinge":
                    query = "SELECT * FROM EMPLOYEE WHERE IsApprentice = 'True'";
                    Employees.Clear();
                    break;
                case "Alle svende":
                    query = "SELECT * FROM EMPLOYEE WHERE IsJourneyman = 'True'";
                    Employees.Clear();
                    break;
                case "Alle mentorer":
                    query = "SELECT * FROM EMPLOYEE WHERE IsMentor = 'True'";
                    Employees.Clear();
                    break;
                case "Arbejdsplads":
                    query = "SELECT * FROM EMPLOYEE";
                    Employees.Clear();
                    break;
                default:
                    query = "SELECT * FROM EMPLOYEE";
                    Employees.Clear();
                    break;
            }

            using (SqlConnection con = new SqlConnection(connectionString))
            {

                SqlCommand cmd = new SqlCommand(query, con);

                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    Employee employee = new Employee
                    {
                        FullName = dr["FullName"].ToString(),
                        PhoneNumber = int.Parse(dr["PhoneNumber"].ToString()),
                        Email = dr["Email"].ToString(),
                        SalaryNumber = int.Parse(dr["SalaryNumber"].ToString()),
                        CurrentWorkplace = dr["CurrentWorkplace"].ToString(),
                        SocialSecurityNumber = dr["SocialSecurityNumber"].ToString(),
                        IsApprentice = bool.Parse(dr["IsApprentice"].ToString()),
                        IsJourneyman = bool.Parse(dr["IsJourneyman"].ToString()),
                        IsMentor = bool.Parse(dr["IsMentor"].ToString()),
                    };
                    Employees.Add(employee);
                }
                dr.Close();
            }
        }
    
         
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }    
}

