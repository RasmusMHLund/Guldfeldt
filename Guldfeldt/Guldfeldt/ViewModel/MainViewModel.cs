using Guldfeldt.Model;
using Guldfeldt.View;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Guldfeldt.ViewModel
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        string connectionString = "Data Source = 10.56.8.35; Initial Catalog = DB_2024_72; Persist Security Info=True;User ID = STUDENT_2024_72; Password=OPENDB_72;Encrypt=True;Trust Server Certificate=True";

        public ObservableCollection<string> PickListItems { get; } = new ObservableCollection<string>
        {
            "Alle medarbejdere",
            "Alle lærlinge",
            "Alle svende",
            "Alle mentorer",
            "Lokation"
        };
        private ObservableCollection<Employee> _employees;
        public ObservableCollection<Employee> Employees
        {
            get { return _employees; }
            set { _employees = value; 
                OnPropertyChanged(nameof(Employees)); }
        }

        private Employee _selectedEmployee;
        public Employee SelectedEmployee
        {
            get { return _selectedEmployee; }
            set { _selectedEmployee = value; 
                OnPropertyChanged(nameof(SelectedEmployee)); 
               }
        }

        private string _selectedPickListItem;
        public string SelectedPickListItem
        {
            get { return _selectedPickListItem; }
            set
            {   _selectedPickListItem = value;
                OnPropertyChanged(nameof(SelectedPickListItem));
                LoadEmployeesFromDatabase(); }
        }

        private ObservableCollection<Location> _locations;
        public ObservableCollection<Location> Locations
        {
            get { return _locations; }
            set { _locations = value; 
                OnPropertyChanged(nameof(Locations)); }
        }

        private Location _selectedLocation;

        public Location SelectedLocation
        {
            get { return _selectedLocation; }
            set { _selectedLocation = value; 
                OnPropertyChanged(nameof(SelectedLocation));
                LoadEmployeesFromDatabase();
            }
        }

        public MainViewModel()
        {
            Employees = new ObservableCollection<Employee>();
            Locations = new ObservableCollection<Location>();
            SelectedPickListItem = "Alle medarbejdere";
            LoadLocationsFromDatabase();
            
        }

        private void LoadEmployeesFromDatabase()
        {
            string query = "";
            Employees.Clear();

            switch (SelectedPickListItem)
            {
                case "Alle medarbejdere":
                    query = "SELECT * FROM EMPLOYEE";
                    break;
                case "Alle lærlinge":
                    query = "SELECT * FROM EMPLOYEE WHERE IsApprentice = 'True'";
                    break;
                case "Alle svende":
                    query = "SELECT * FROM EMPLOYEE WHERE IsJourneyman = 'True'";
                    break;
                case "Alle mentorer":
                    query = "SELECT * FROM EMPLOYEE WHERE IsMentor = 'True'";
                    break;
                case "Lokation":
                    query = "SELECT * FROM EMPLOYEE WHERE CurrentWorkplace = 'SelectedLocation.Name'";
                    break;
                default:
                    query = "SELECT * FROM EMPLOYEE";
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
        private void LoadLocationsFromDatabase()
        {
            string query = "SELECT * FROM LOCATION";

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(query, con);
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    Location location = new Location 
                    {
                        Name = dr["Name"].ToString(),
                        Address = dr["Address"].ToString(),
                        IsConstructionSite = bool.Parse(dr["IsConstructionSite"].ToString()),
                        IsSchool = bool.Parse(dr["IsSchool"].ToString()),
                    };
                    Locations.Add(location);
                }
                con.Close();
            }
        }

    
            protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }   
}
