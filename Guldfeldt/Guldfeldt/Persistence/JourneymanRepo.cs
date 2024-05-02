using Guldfeldt.Model;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Configuration;

namespace Guldfeldt.Persistence
{
    public class JourneymanRepo
    {
        string? ConnectionString;

        private List<Journeyman> Journeymen;
        public JourneymanRepo()
        {
            Journeymen = new List<Journeyman>();

            IConfigurationRoot config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();

            string? ConnectionString = config.GetConnectionString("MyDBConnection");

        }
        public void Create(Journeyman journeymanToBeCreated)
        {
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("INSERT INTO JOURNEYMAN (Name, DateOfBirth, SocialSecurityNumber, Email, PhoneNumber, MentorStatus, SalaryNumber)" +
                                                 "VALUES(@Name,@DateOfBirth,@SocialSecurityNumber,@Email,@PhoneNumber,@MentorStatus,@SalaryNumber)" +
                                                 "SELECT @@IDENTITY", con);
                cmd.Parameters.Add("@Name", SqlDbType.NVarChar).Value = journeymanToBeCreated.Name;
                cmd.Parameters.Add("@DateOfBirth", SqlDbType.DateTime2).Value = journeymanToBeCreated.DateOfBirth;
                cmd.Parameters.Add("@SocialSecurityNumber", SqlDbType.NVarChar).Value = journeymanToBeCreated.SocialSecurityNumber;
                cmd.Parameters.Add("@Email", SqlDbType.NVarChar).Value = journeymanToBeCreated.Email;
                cmd.Parameters.Add("@PhoneNumber", SqlDbType.Int).Value = journeymanToBeCreated.PhoneNumber;
                cmd.Parameters.Add("@MentorStatus", SqlDbType.Bit).Value = journeymanToBeCreated.MentorStatus;
                cmd.Parameters.Add("@SalaryNumber", SqlDbType.Int).Value = journeymanToBeCreated.SalaryNumber;
                journeymanToBeCreated.Id = Convert.ToInt32(cmd.ExecuteScalar());
                Journeymen.Add(journeymanToBeCreated);
            }
        }

        public void RetrieveAll()
        {
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                con.Open();

                SqlCommand cmd = new SqlCommand("SELECT Name, DateOfBirth, SocialSecurityNumber, Email, PhoneNumber, MentorStatus, SalaryNumber FROM JOURNEYMAN", con);

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        Journeyman journeyman = new Journeyman()
                        {
                            Id = int.Parse(dr["Id"].ToString()),
                            Name = dr["Name"].ToString(),
                            DateOfBirth = DateTime.Parse(dr["DateOfBirth"].ToString()),
                            SocialSecurityNumber = dr["SocialSecurityNumber"].ToString(),
                            Email = dr["Email"].ToString(),
                            PhoneNumber = int.Parse(dr["PhoneNumber"].ToString()),
                            MentorStatus = bool.Parse(dr["MentorStatus"].ToString()),
                            SalaryNumber = int.Parse(dr["SalaryNumber"].ToString())
                        };
                        Journeymen.Add(journeyman);
                    }
                }
            }
        }

        public void Update(Journeyman journeymanToBeUpdated)
        {
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                con.Open();

                SqlCommand cmd = new SqlCommand("UPDATE JOURNEYMAN SET Name = @Name, DateOfBirth = @DateOfBirth, SocialSecurityNumber = @SocialSecurityNumber, Email = @Email, PhoneNumber = @PhoneNumber, MentorStatus = @MentorStatus, SalaryNumber = @SalaryNumber WHERE Id = @Id", con);
                cmd.Parameters.Add("@Id", SqlDbType.NVarChar).Value = journeymanToBeUpdated.Id;
                cmd.ExecuteNonQuery();
            }
        }

        public void Delete(Journeyman journeymanToBeDeleted)
       {
           using (SqlConnection con = new SqlConnection(ConnectionString))
           {
               con.Open();

               SqlCommand cmd = new SqlCommand("DELETE FROM JOURNEYMAN WHERE Id = @Id", con);
               cmd.Parameters.Add("@Id", SqlDbType.NVarChar).Value = journeymanToBeDeleted.Id;
               cmd.ExecuteNonQuery();
           }
           Journeymen.Remove(journeymanToBeDeleted);
       }

    }
}
