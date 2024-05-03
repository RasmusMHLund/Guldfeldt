using Guldfeldt.Model;
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
    public class ApprenticeRepo 
    {
        string? ConnectionString;

        private List<Apprentice> Apprentices;
        public ApprenticeRepo()
        {
            Apprentices = new List<Apprentice>();

            IConfigurationRoot config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();

            string? ConnectionString = config.GetConnectionString("MyDBConnection");

        }

        public void Create(Apprentice apprenticeToBeCreated)
        {
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("INSERT INTO APPRENTICE (Name, DateOfBirth, SocialSecurityNumber, Email, PhoneNumber, SalaryNumber)" +
                                                 "VALUES(@Name,@DateOfBirth,@SocialSecurityNumber,@Email,@PhoneNumber,@SalaryNumber)" +
                                                 "SELECT @@IDENTITY", con);
                cmd.Parameters.Add("@Name", SqlDbType.NVarChar).Value = apprenticeToBeCreated.Name;
                cmd.Parameters.Add("@DateOfBirth", SqlDbType.DateTime2).Value = apprenticeToBeCreated.DateOfBirth;
                cmd.Parameters.Add("@SocialSecurityNumber", SqlDbType.NVarChar).Value = apprenticeToBeCreated.SocialSecurityNumber;
                cmd.Parameters.Add("@Email", SqlDbType.NVarChar).Value = apprenticeToBeCreated.Email;
                cmd.Parameters.Add("@PhoneNumber", SqlDbType.Int).Value = apprenticeToBeCreated.PhoneNumber;
                cmd.Parameters.Add("@SalaryNumber", SqlDbType.Int).Value = apprenticeToBeCreated.SalaryNumber;
                apprenticeToBeCreated.Id = Convert.ToInt32(cmd.ExecuteScalar());
                Apprentices.Add(apprenticeToBeCreated);
            }
        }

        public void RetrieveAll()
        {
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                con.Open();

                SqlCommand cmd = new SqlCommand("SELECT Name, DateOfBirth, SocialSecurityNumber, Email, PhoneNumber, SalaryNumber FROM APPRENTICE", con);

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        Apprentice apprentice = new Apprentice()
                        {
                            Id = int.Parse(dr["Id"].ToString()),
                            Name = dr["Name"].ToString(),
                            DateOfBirth = DateTime.Parse(dr["DateOfBirth"].ToString()),
                            SocialSecurityNumber = dr["SocialSecurityNumber"].ToString(),
                            Email = dr["Email"].ToString(),
                            PhoneNumber = int.Parse(dr["PhoneNumber"].ToString()),
                            SalaryNumber = int.Parse(dr["SalaryNumber"].ToString())
                        };
                        Apprentices.Add(apprentice);
                    }
                }
            }
        }

        public void Update(Apprentice apprenticeToBeUpdated)
        {
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                con.Open();

                SqlCommand cmd = new SqlCommand("UPDATE APPRENTICE SET Name = @Name, DateOfBirth = @DateOfBirth, SocialSecurityNumber = @SocialSecurityNumber, Email = @Email, PhoneNumber = @PhoneNumber, SalaryNumber = @SalaryNumber WHERE Id = @Id", con);
                cmd.Parameters.Add("@Id", SqlDbType.NVarChar).Value = apprenticeToBeUpdated.Id;
                cmd.ExecuteNonQuery();
            }
        }

        public void Delete(Apprentice apprenticeToBeDeleted)
       {
           using (SqlConnection con = new SqlConnection(ConnectionString))
           {
               con.Open();

               SqlCommand cmd = new SqlCommand("DELETE FROM APPRENTICE WHERE Id = @Id", con);
               cmd.Parameters.Add("@Id", SqlDbType.NVarChar).Value = apprenticeToBeDeleted.Id;
               cmd.ExecuteNonQuery();
           }
           Apprentices.Remove(apprenticeToBeDeleted);
       }

    }
}
