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


        public EditLocation(Location location)
        {
            InitializeComponent();
            DataContext = mvm;
            mvm.SelectedLocation = location;
            ApplyHoverEffect(Save_Button, defaultbrush, hoverbrush);
            ApplyHoverEffect(Delete_Button, defaultbrush, hoverbrush);
            ApplyHoverEffect(Cancel_Button, defaultbrush, hoverbrush);
        }
        SolidColorBrush defaultbrush = new SolidColorBrush(System.Windows.Media.Color.FromArgb(255, 94, 91, 91));
        SolidColorBrush hoverbrush = new SolidColorBrush(System.Windows.Media.Color.FromArgb(255, 136, 133, 133));
        private void ApplyHoverEffect(Button button, SolidColorBrush defaultBrush, SolidColorBrush hoverBrush)
        {
            button.Background = defaultBrush;
            button.MouseEnter += (sender, e) =>
            {
                button.Background = hoverBrush;
            };
            button.MouseLeave += (sender, e) =>
            {
                button.Background = defaultBrush;
            };
        }

        private void Cancel_Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Save_Button_Click(object sender, RoutedEventArgs e)
        {
            lr.Update(mvm.SelectedLocation);
            MessageBox.Show(" Ændring gemt. ");
            mvm.LoadLocationsFromDatabase();
            Close();
        }

        private void Delete_Button_Click(object sender, RoutedEventArgs e)
        {

            lr.Delete(mvm.SelectedLocation);
            MessageBox.Show(" Lokation slettet. ");
            mvm.LoadLocationsFromDatabase();
            Close();
        }
    }
}
