﻿using Guldfeldt.Model;
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
            { _selectedPickListItem = value;
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
            SelectedLocation = new Location()
            {
                Name = "Vælg lokation",
                Address = null,
                IsConstructionSite = null,
                IsSchool = null,


            };

            LoadLocationsFromDatabase();
            LoadEmployeesFromDatabase();


        }

        private void LoadEmployeesFromDatabase()
        {
            Employees.Clear();

            using (SqlConnection con = new SqlConnection(connectionString)) {
                con.Open();

                switch (SelectedPickListItem)
                {
                    case "Alle medarbejdere":
                        EmployeeQuery(con, "SELECT * FROM EMPLOYEE");
                        break;
                    case "Alle lærlinge":
                        EmployeeQuery(con, "SELECT * FROM EMPLOYEE WHERE IsApprentice = 'True'");
                        break;
                    case "Alle svende":
                        EmployeeQuery(con, "SELECT * FROM EMPLOYEE WHERE IsJourneyman = 'True'");
                        break;
                    case "Alle mentorer":
                        EmployeeQuery(con, "SELECT * FROM EMPLOYEE WHERE IsMentor = 'True'");
                        break;
                    case "Lokation":
                        string chosenLocation = SelectedLocation.Name ?? "";
                        EmployeeFromLocationQuery(con, "SELECT * FROM EMPLOYEE WHERE CurrentWorkplace = @CurrentWorkplace", "@CurrentWorkplace", chosenLocation);
                        break;
                    default:
                        EmployeeQuery(con, "SELECT * FROM EMPLOYEE");
                        break;

                }
            }
        }
        private void EmployeeQuery(SqlConnection con, string query) {

            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
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
                }
            }
        }

        private void EmployeeFromLocationQuery(SqlConnection con, string query, string parameterName, string parameterValue)
        {
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue(parameterName, parameterValue);

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
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
                }
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
