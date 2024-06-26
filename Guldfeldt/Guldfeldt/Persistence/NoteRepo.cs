﻿using Guldfeldt.Model;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Windows.Forms;

namespace Guldfeldt.Persistence
{
    public class NoteRepo
    {
        string? connectionString = "Data Source = 10.56.8.35; Initial Catalog = DB_2024_72; Persist Security Info=True;User ID = STUDENT_2024_72; Password=OPENDB_72;Encrypt=True;Trust Server Certificate=True";

        private List<Note> Notes;
        public NoteRepo()
        {
            Notes = new List<Note>();
        }
        public List<Note> GetNotes()
        {
            return Notes;
        }

        public void Create(Note noteToBeCreated)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("INSERT INTO NOTE (Title, NoteDescription, MentorName, Date)" +
                                                 "VALUES(@Title,@NoteDescription,@MentorName,@Date)" +
                                                 "SELECT @@IDENTITY", con);
                cmd.Parameters.Add("@Title", SqlDbType.NVarChar).Value = noteToBeCreated.Title;
                cmd.Parameters.Add("@NoteDescription", SqlDbType.NVarChar).Value = noteToBeCreated.NoteDescription;
                cmd.Parameters.Add("@MentorName", SqlDbType.NVarChar).Value = noteToBeCreated.MentorName;
                cmd.Parameters.Add("@Date", SqlDbType.DateTime2).Value = noteToBeCreated.Date;
                noteToBeCreated.NoteId = Convert.ToInt32(cmd.ExecuteScalar());
                Notes.Add(noteToBeCreated);
            }
        }
        public void RetrieveAll(string sortOrder)
        {
            List<Note> noteList = new List<Note>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();

                SqlCommand cmd = new SqlCommand("SELECT NoteId, Title, NoteDescription, MentorName, Date FROM NOTE", con);

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        Note note = new Note()
                        {
                            NoteId = int.Parse(dr["NoteId"].ToString()),
                            Title = dr["Title"].ToString(),
                            NoteDescription = dr["NoteDescription"].ToString(),
                            MentorName = dr["MentorName"].ToString(),
                            Date = DateTime.Parse(dr["Date"].ToString())
                        };
                        
                        noteList.Add(note);
                    }
                }
            }
            switch (sortOrder )
            {
                case "Nyeste":
                    noteList = noteList.OrderByDescending(n => n.Date).ToList();
                    break;
                case "Ældste":
                    noteList = noteList.OrderBy(n => n.Date).ToList();
                    break;
                case "A-Z":
                    noteList = noteList.OrderBy(n => n.Title).ToList();
                    break;
                case "Z-A":
                    noteList = noteList.OrderByDescending(n => n.Title).ToList();
                    break;
            }
            Notes.Clear();

            foreach (Note note in noteList)
            {
                Notes.Add(note);
            }
        }

        public void RetrieveAll(string query, string parameterName, string parameterValue, string sortOrder)
        {
            List<Note> noteList = new List<Note>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue(parameterName, parameterValue);

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            Note note = new Note()
                            {
                                NoteId = int.Parse(dr["NoteId"].ToString()),
                                Title = dr["Title"].ToString(),
                                NoteDescription = dr["NoteDescription"].ToString(),
                                MentorName = dr["MentorName"].ToString(),
                                Date = DateTime.Parse(dr["Date"].ToString())
                            };

                            noteList.Add(note);
                        }
                    }
                }
                switch (sortOrder)
                {
                    case "Nyeste":
                        noteList = noteList.OrderByDescending(n => n.Date).ToList();
                        break;
                    case "Ældste":
                        noteList = noteList.OrderBy(n => n.Date).ToList();
                        break;
                    case "A-Z":
                        noteList = noteList.OrderBy(n => n.Title).ToList();
                        break;
                    case "Z-A":
                        noteList = noteList.OrderByDescending(n => n.Title).ToList();
                        break;
                }
                Notes.Clear();

                foreach (Note note in noteList)
                {
                    Notes.Add(note);
                }
            }
        }
            public void Update(Note noteToBeUpdated)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();

                SqlCommand cmd = new SqlCommand("UPDATE NOTE SET Title = @Title, NoteDescription = @NoteDescription, MentorName = @MentorName, Date = @Date WHERE NoteId = @NoteId", con);
                cmd.Parameters.Add("@NoteId", SqlDbType.Int).Value = noteToBeUpdated.NoteId;
                cmd.Parameters.Add("@Title", SqlDbType.NVarChar).Value = noteToBeUpdated.Title;
                cmd.Parameters.Add("@NoteDescription", SqlDbType.NVarChar).Value = noteToBeUpdated.NoteDescription;
                cmd.Parameters.Add("@MentorName", SqlDbType.NVarChar).Value = noteToBeUpdated.MentorName;
                cmd.Parameters.Add("@Date", SqlDbType.DateTime2).Value = noteToBeUpdated.Date;
                cmd.ExecuteNonQuery();
            }
        }
        public void Delete(Note noteToBeDeleted)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();

                SqlCommand cmd = new SqlCommand("DELETE FROM NOTE WHERE NoteId = @NoteId", con);
                cmd.Parameters.Add("@NoteId", SqlDbType.Int).Value = noteToBeDeleted.NoteId;
                cmd.ExecuteNonQuery();
            }

            Notes.Remove(noteToBeDeleted);

            }
        }
    }
