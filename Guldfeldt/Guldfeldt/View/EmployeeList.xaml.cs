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
    /// Interaction logic for EmployeeList.xaml
    /// </summary>
    public partial class EmployeeList : Window
    {
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
    }
}
