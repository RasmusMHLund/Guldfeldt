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
    /// Interaction logic for AddWorkplace.xaml
    /// </summary>
    public partial class AddWorkplace : Window
    {
        WorkplaceViewModel wvm;
        public AddWorkplace()
        {
            InitializeComponent();
            DataContext = wvm;
        }

        private void Cancel_Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Add_Button_Click(object sender, RoutedEventArgs e)
        {
          
          wvm.Name = WorkplaceName_TextBox.Text;
          wvm.Address = WorkplaceAddress_TextBox.Text;

        }
    }
}
