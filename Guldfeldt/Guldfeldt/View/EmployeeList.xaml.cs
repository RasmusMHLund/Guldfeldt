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

namespace Guldfeldt.View
{
    /// <summary>
    /// Interaction logic for EmployeeList.xaml
    /// </summary>
    public partial class EmployeeList : Window
    {
        SqlConnection con = new SqlConnection("Data Source=10.56.8.35;Persist Security Info=True;User ID=STUDENT_2024_72;Password=OPENDB_72;Trust Server Certificate=True");
        SqlCommand cmd;
        SqlDataReader dr;

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
            if (apprentices)
            {
                cmd = new SqlCommand();
                con.Open();
                cmd.Connection = con;
                cmd.CommandText = "SELECT * FROM APPRENTICE";
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    EmployeeList_ListBox.Items.Add(dr[Name]);
                }
            } else if (all employees) //mangler linking table mellem lærling og svend
             {
                cmd = new SqlCommand();
                con.Open();
                cmd.Connection = con;
                cmd.CommandText = "SELECT * FROM APPRENTICEJOURNEYMAN";
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    EmployeeList_ListBox.Items.Add(dr[Name]);
                }
            } else if (journeymen)
            {
                cmd = new SqlCommand();
                con.Open();
                cmd.Connection = con;
                cmd.CommandText = "SELECT * FROM JOURNEYMAN";
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    EmployeeList_ListBox.Items.Add(dr[Name]);
                }
            }
            else if (workplace) //mangler linking table til lærling og svend
            {
                cmd = new SqlCommand();
                con.Open();
                cmd.Connection = con;
                cmd.CommandText = "SELECT * FROM WORKPLACE";
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    EmployeeList_ListBox.Items.Add(dr[Name]);
                }
            }
        }

    
    }
}
