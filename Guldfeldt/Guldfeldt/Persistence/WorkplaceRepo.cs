using Guldfeldt.Model;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Guldfeldt.Persistence
{
    public class WorkplaceRepo
    {
        string? ConnectionString;

        private List<Workplace> Workplaces;
        public WorkplaceRepo()
        {
            Workplaces = new List<Workplace>();

            IConfigurationRoot config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();

            string? ConnectionString = config.GetConnectionString("MyDBConnection");

        }
        public void Create(Workplace workplaceToBeCreated)
        {
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("INSERT INTO WORKPLACE (Name, Address)" +
                                                 "VALUES(@Name,@Address)" +
                                                 "SELECT @@IDENTITY", con);
                cmd.Parameters.Add("@Name", SqlDbType.NVarChar).Value = workplaceToBeCreated.Name;
                cmd.Parameters.Add("@Address", SqlDbType.NVarChar).Value = workplaceToBeCreated.Address;
                workplaceToBeCreated.Id = Convert.ToInt32(cmd.ExecuteScalar());
                Workplaces.Add(workplaceToBeCreated);
            }

        }
        public void RetrieveAll()
        {
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                con.Open();

                SqlCommand cmd = new SqlCommand("SELECT Name, Address FROM WORKPLACE", con);

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        Workplace workplace = new Workplace()
                        {
                            Id = int.Parse(dr["Id"].ToString()),
                            Name = dr["Name"].ToString(),
                            Address = dr["Address"].ToString(),                    
                        };
                        Workplaces.Add(workplace);
                    }
                }
            }
        }
        public void Update(Workplace workplaceToBeUpdated)
        {
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                con.Open();

                SqlCommand cmd = new SqlCommand("UPDATE WORKPLACE SET Name = @Name, Address = @Address WHERE Id = @Id", con);
                cmd.Parameters.Add("@Id", SqlDbType.NVarChar).Value = workplaceToBeUpdated.Id;
                cmd.ExecuteNonQuery();
            }
        }
        public void Delete(Workplace workplaceToBeDeleted)
        {
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                con.Open();

                SqlCommand cmd = new SqlCommand("DELETE FROM WORKPLACE WHERE Id = @Id", con);
                cmd.Parameters.Add("@Id", SqlDbType.NVarChar).Value = workplaceToBeDeleted.Id;
                cmd.ExecuteNonQuery();
            }
            Workplaces.Remove(workplaceToBeDeleted);
        }
    }
}
