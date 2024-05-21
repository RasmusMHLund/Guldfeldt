using Guldfeldt.Model;
using Guldfeldt.Persistence;
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
using System.Windows;
using System.Windows.Controls;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Guldfeldt.ViewModel
{
    public class MainViewModel : INotifyPropertyChanged
    {
        EmployeeRepo er = new EmployeeRepo();
        LocationRepo lr = new LocationRepo();
        NoteRepo nr = new NoteRepo();

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
        private ObservableCollection<Note> _notes;
        public ObservableCollection<Note> Notes
        {
            get { return _notes; }
            set
            {
                _notes = value;
                OnPropertyChanged(nameof(Notes));
            }
        }
        private Note _selectedNote;
        public Note SelectedNote
        {
            get { return _selectedNote; }
            set
            {
                _selectedNote = value;
                OnPropertyChanged(nameof(SelectedNote));
            }
        }

        public MainViewModel()
        {
            Employees = new ObservableCollection<Employee>();
            Locations = new ObservableCollection<Location>();
            Notes = new ObservableCollection<Note>();
            SelectedPickListItem = "Alle medarbejdere";
            
            LoadLocationsFromDatabase();
            LoadEmployeesFromDatabase();
            LoadNotesFromDatabase();
        }

        public void LoadEmployeesFromDatabase()
        {
            Employees.Clear();

            using (SqlConnection con = new SqlConnection(connectionString)) {
                con.Open();

                switch (SelectedPickListItem)
                {
                    case "Alle medarbejdere":
                        EmployeeQuery("SELECT * FROM EMPLOYEE");
                        break;
                    case "Alle lærlinge":
                        EmployeeQuery("SELECT * FROM EMPLOYEE WHERE IsApprentice = 'True'");
                        break;
                    case "Alle svende":
                        EmployeeQuery("SELECT * FROM EMPLOYEE WHERE IsJourneyman = 'True'");
                        break;
                    case "Alle mentorer":
                        EmployeeQuery("SELECT * FROM EMPLOYEE WHERE IsMentor = 'True'");
                        break;
                    case "Lokation":
                       if (SelectedLocation != null)
                        {
                        string chosenLocation = SelectedLocation.Name;
                        EmployeeFromLocationQuery("SELECT * FROM EMPLOYEE WHERE CurrentWorkplace = @CurrentWorkplace", "@CurrentWorkplace", chosenLocation);

                        } else
                        {
                            MessageBox.Show(" Vælg en lokation. ");
                        } 
                        break;
                    default:
                        EmployeeQuery("SELECT * FROM EMPLOYEE");
                        break;

                }
            }
        }
        private void EmployeeQuery(string query)
        { 
            er.RetrieveAll(query);

            foreach (var employee in er.GetEmployees())
            {
                Employees.Add(employee);
            }
        }

        private void EmployeeFromLocationQuery(string query, string parameterName, string parameterValue)
        {
            er.RetrieveAll(query, parameterName, parameterValue);

            foreach (var employee in er.GetEmployees())
            {
                Employees.Add(employee);
            }
        }

        public void LoadLocationsFromDatabase()
        {
            Locations.Clear();

            lr.RetrieveAll();

            foreach (var location in lr.GetLocations())
            {
                Locations.Add(location);
            }
        }

        public void LoadNotesFromDatabase()
        {
            Notes.Clear();

            nr.RetrieveAll();

            foreach (var note in nr.GetNotes())
            {
                Notes.Add(note);
            }
        }
    
            protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }   
}
