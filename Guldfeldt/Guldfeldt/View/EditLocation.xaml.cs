using Guldfeldt.Model;
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
    /// Interaction logic for EditWorkplace.xaml
    /// </summary>
    public partial class EditLocation : Window
    {
        MainViewModel mvm = new MainViewModel();
        LocationRepo lr = new LocationRepo();

        public EditLocation()
        {
            InitializeComponent();
            DataContext = mvm;
        }

        private void Cancel_Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Save_Button_Click(object sender, RoutedEventArgs e)
        {
            Location location = new Location
            {
                Name = LocationName_TextBox.Text,
                Address = LocationAddress_TextBox.Text,
                IsSchool = IsSchool_CheckBox.IsChecked,
                IsConstructionSite = IsConstructionSite_CheckBox.IsChecked,
            };
            lr.Update(location);
        }

        private void Delete_Button_Click(object sender, RoutedEventArgs e)
        {
            
            //lr.Delete(mvm.SelectedLocation);
        }
    }
}
