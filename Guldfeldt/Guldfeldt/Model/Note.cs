using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Guldfeldt.Model
{
    public class Note
    {
        public int NoteId { get; set; }
        public string Title { get; set; }
        public string NoteDescription { get; set; }
        public string MentorName { get; set; }
        public DateTime? Date { get; set; }

        public Note(int noteId, string title, string noteDescription, string mentorName, DateTime? date) 
        {
            NoteId = noteId;
            Title = title;
            NoteDescription = noteDescription;
            MentorName = mentorName;
            Date = date;
        }
        public Note() : this(0, null, null, null, null) { }
    }
}
