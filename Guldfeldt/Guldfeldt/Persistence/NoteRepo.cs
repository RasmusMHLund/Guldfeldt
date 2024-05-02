using Guldfeldt.Model;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using System.Data;

namespace Guldfeldt.Persistence
{
    public class NoteRepo
    {
        string? ConnectionString;

        private List<Note> Notes;
        public NoteRepo()
        {
            Notes = new List<Note>();

            IConfigurationRoot config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();

            string? ConnectionString = config.GetConnectionString("MyDBConnection");

        }
        public void Create(Note noteToBeCreated)
        {
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("INSERT INTO NOTE (Title, NoteDescription, MentorName, Date)" +
                                                 "VALUES(@Title,@NoteDescription,@MentorName,@Date)" +
                                                 "SELECT @@IDENTITY", con);
                cmd.Parameters.Add("@Title", SqlDbType.NVarChar).Value = noteToBeCreated.Title;
                cmd.Parameters.Add("@NoteDescription", SqlDbType.NVarChar).Value = noteToBeCreated.NoteDescription;
                cmd.Parameters.Add("@MentorName", SqlDbType.NVarChar).Value = noteToBeCreated.MentorName;
                cmd.Parameters.Add("@Date", SqlDbType.DateTime2).Value = noteToBeCreated.Date;

                noteToBeCreated.Id = Convert.ToInt32(cmd.ExecuteScalar());
                Notes.Add(noteToBeCreated);
            }

        }
        public void RetrieveAll()
        {
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                con.Open();

                SqlCommand cmd = new SqlCommand("SELECT Title, NoteDescription, MentorName, Date FROM NOTE", con);

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        Note note = new Note()
                        {
                            Id = int.Parse(dr["Id"].ToString()),
                            Title = dr["Title"].ToString(),
                            NoteDescription = dr["NoteDescription"].ToString(),
                            MentorName = dr["MentorName"].ToString(),
                            Date = DateTime.Parse(dr["Date"].ToString())
                        };
                        Notes.Add(note);
                    }
                }
            }
        }
        public void Update(Note noteToBeUpdated)
        {
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                con.Open();

                SqlCommand cmd = new SqlCommand("UPDATE NOTE SET Title = @Title, NoteDescription = @NoteDescription, MentorName = @MentorName, Date = @Date WHERE Id = @Id", con);
                cmd.Parameters.Add("@Id", SqlDbType.NVarChar).Value = noteToBeUpdated.Id;
                cmd.ExecuteNonQuery();
            }
        }
        public void Delete(Note noteToBeDeleted)
        {
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                con.Open();

                SqlCommand cmd = new SqlCommand("DELETE FROM NOTE WHERE Id = @Id", con);
                cmd.Parameters.Add("@Id", SqlDbType.NVarChar).Value = noteToBeDeleted.Id;
                cmd.ExecuteNonQuery();
            }
            Notes.Remove(noteToBeDeleted);
        }
    }
}
