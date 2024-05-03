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
    /// Interaction logic for NoteList.xaml
    /// </summary>
    public partial class NoteList : Window
    {
        public NoteList()
        {
            InitializeComponent();
        }

        private void AddNote_Button_Click(object sender, RoutedEventArgs e)
        {
            AddNote addNote = new AddNote();
            addNote.ShowDialog();
        }

        private void DeleteNote_Button_Click(object sender, RoutedEventArgs e)
        {
            DeleteNote deleteNote = new DeleteNote();
            deleteNote.ShowDialog();
        }

        private void EditNote_Button_Click(object sender, RoutedEventArgs e)
        {
            EditNote editNote = new EditNote();
            editNote.ShowDialog();
        }

        private void Back_Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
