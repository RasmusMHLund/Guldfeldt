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
    public class LocationRepo
    {
        string? ConnectionString;

        private List<Location> Locations;
        public LocationRepo()
        {
            Locations = new List<Location>();

            IConfigurationRoot config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();

            string? ConnectionString = config.GetConnectionString("MyDBConnection");

        }
        public void Create(Location locationToBeCreated)
        {
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("INSERT INTO LOCATION (Name, Address, IsConstructionSite, IsSchool)" +
                                                 "VALUES(@Name,@Address,@IsConstructionSite,@IsSchool)" +
                                                 "SELECT @@IDENTITY", con);
                cmd.Parameters.Add("@Name", SqlDbType.NVarChar).Value = locationToBeCreated.Name;
                cmd.Parameters.Add("@Address", SqlDbType.NVarChar).Value = locationToBeCreated.Address;
                cmd.Parameters.Add("@IsConstructionSite", SqlDbType.Bit).Value = locationToBeCreated.IsConstructionSite;
                cmd.Parameters.Add("@IsSchool", SqlDbType.Bit).Value = locationToBeCreated.IsSchool;
                locationToBeCreated.Id = Convert.ToInt32(cmd.ExecuteScalar());
                Locations.Add(locationToBeCreated);
            }

        }
        public void RetrieveAll()
        {
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                con.Open();

                SqlCommand cmd = new SqlCommand("SELECT Name, Address, IsConstructionSite, IsSchool FROM LOCATION", con);

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        Location workplace = new Location()
                        {
                            Id = int.Parse(dr["Id"].ToString()),
                            Name = dr["Name"].ToString(),
                            Address = dr["Address"].ToString(),
                            IsConstructionSite = bool.Parse(dr["IsConstructionSite"].ToString()),
                            IsSchool = bool.Parse(dr["IsSchool"].ToString()),
                        };
                        Locations.Add(workplace);
                    }
                }
            }
        }
        public void Update(Location locationToBeUpdated)
        {
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                con.Open();

                SqlCommand cmd = new SqlCommand("UPDATE LOCATION SET Name = @Name, Address = @Address, IsConstructionSite = @IsConstructionSite, IsSchool = @IsSchool WHERE Id = @Id", con);
                cmd.Parameters.Add("@Id", SqlDbType.NVarChar).Value = locationToBeUpdated.Id;
                cmd.ExecuteNonQuery();
            }
        }
        public void Delete(Location locationToBeDeleted)
        {
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                con.Open();

                SqlCommand cmd = new SqlCommand("DELETE FROM LOCATION WHERE Id = @Id", con);
                cmd.Parameters.Add("@Id", SqlDbType.NVarChar).Value = locationToBeDeleted.Id;
                cmd.ExecuteNonQuery();
            }
            Locations.Remove(locationToBeDeleted);
        }
    }
}
