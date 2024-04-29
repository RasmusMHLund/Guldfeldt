﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Guldfeldt.Model
{
    public class Note
    {
        public string Title { get; set; }
        public string NoteDescription { get; set; }
        public string MentorName { get; set; }
        public DateTime Date { get; set; }

        public Note(string title, string noteDescription, string mentorName, DateTime date) 
        { 
            Title = title;
            NoteDescription = noteDescription;
            MentorName = mentorName;
            Date = date;
        }
    }
}
