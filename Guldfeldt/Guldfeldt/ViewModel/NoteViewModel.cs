using Guldfeldt.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Guldfeldt.ViewModel
{
    public class NoteViewModel
    {
        public string Title { get; set; }
        public string NoteDescription { get; set; }
        public string MentorName { get; set; }
        public DateTime? Date { get; set; }

        public NoteViewModel(Note note) 
        {
            Title = note.Title;
            NoteDescription = note.NoteDescription;
            MentorName = note.MentorName;
            Date = note.Date;
        }
    }
}
