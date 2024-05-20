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
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Guldfeldt.View.Notes
{
    /// <summary>
    /// Interaction logic for AddNote.xaml
    /// </summary>
    public partial class AddNote : Window
    {
        MainViewModel mvm = new MainViewModel();
        NoteRepo nr = new NoteRepo();

        public AddNote()
        {
            InitializeComponent();
        }

        private void Cancel_Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Add_Button_Click(object sender, RoutedEventArgs e)
        {
            Note note = new Note()
            {
            Title = WriteTitle_TextBox.Text,
            NoteDescription = WriteNote_TextBox.Text,
            MentorName = "Mentor",
            Date = DateTime.Now,
        };
            nr.Create(note);
            MessageBox.Show(" Note oprettet. ");
            Close();
        }
    }
}
