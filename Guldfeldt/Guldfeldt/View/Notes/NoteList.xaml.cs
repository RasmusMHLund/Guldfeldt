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

namespace Guldfeldt.View.Notes
{
    public partial class NoteList : Window
    {
        MainViewModel mvm = new MainViewModel();
        NoteRepo nr = new NoteRepo();

        public NoteList()
        {
            InitializeComponent();
            DataContext = mvm;
            ApplyHoverEffect(AddNote_Button, defaultbrush, hoverbrush);
            ApplyHoverEffect(DeleteNote_Button, defaultbrush, hoverbrush);
            ApplyHoverEffect(EditNote_Button, defaultbrush, hoverbrush);

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

        private void AddNote_Button_Click(object sender, RoutedEventArgs e)
        {
            AddNote addNote = new AddNote();
            addNote.ShowDialog();
           
           
            mvm.LoadNotesFromDatabase();
        }

        private void DeleteNote_Button_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Bekræft sletning af note", "Bekræft", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                nr.Delete(mvm.SelectedNote);
                MessageBox.Show(" Note slettet. ");
            }
            mvm.LoadNotesFromDatabase();
        }

        private void EditNote_Button_Click(object sender, RoutedEventArgs e)
        {
            Title_TextBox.IsReadOnly = false;
            NoteDescription_TextBox.IsReadOnly = false;
            Save_Button.IsEnabled = true;
            Save_Button.Visibility = Visibility.Visible;
        }

        private void Back_Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void SortBy_ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (SortBy_ComboBox.SelectedItem != null)
            {
                switch (SortBy_ComboBox.SelectedItem)
                {
                    case "A-Z":

                        break;
                    case "Z-A":

                        break;
                    case "Ældste":

                        break;
                    case "Nyeste":

                        break;
                }
            }
        }

        private void Save_Button_Click(object sender, RoutedEventArgs e)
        {
            Title_TextBox.IsReadOnly = true;
            NoteDescription_TextBox.IsReadOnly = true;  
            Save_Button.IsEnabled = false;
            Save_Button.Visibility = Visibility.Hidden;


            nr.Update(mvm.SelectedNote);
            mvm.LoadNotesFromDatabase();


            MessageBox.Show(" Ændring gemt. ");
        }
    }
}
