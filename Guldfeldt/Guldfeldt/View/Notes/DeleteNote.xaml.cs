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

namespace Guldfeldt.View.Notes
{
    /// <summary>
    /// Interaction logic for DeleteNote.xaml
    /// </summary>
    public partial class DeleteNote : Window
    {
        public DeleteNote()
        {
            InitializeComponent();
        }

        private void Cancel_Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
