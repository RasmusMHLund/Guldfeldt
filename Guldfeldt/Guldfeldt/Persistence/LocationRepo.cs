using Guldfeldt.Model;
using Guldfeldt.ViewModel;
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
        string? connectionString = "Data Source = 10.56.8.35; Initial Catalog = DB_2024_72; Persist Security Info=True;User ID = STUDENT_2024_72; Password=OPENDB_72;Encrypt=True;Trust Server Certificate=True";

        private List<Location> Locations;
        public LocationRepo()
        {
            Locations = new List<Location>();
        }
        public void Create(Location locationToBeCreated)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("INSERT INTO LOCATION (Name, Address, IsConstructionSite, IsSchool)" +
                                                 "VALUES(@Name,@Address,@IsConstructionSite,@IsSchool)" +
                                                 "SELECT @@IDENTITY", con);
                cmd.Parameters.Add("@Name", SqlDbType.NVarChar).Value = locationToBeCreated.Name;
                cmd.Parameters.Add("@Address", SqlDbType.NVarChar).Value = locationToBeCreated.Address;
                cmd.Parameters.Add("@IsConstructionSite", SqlDbType.Bit).Value = locationToBeCreated.IsConstructionSite;
                cmd.Parameters.Add("@IsSchool", SqlDbType.Bit).Value = locationToBeCreated.IsSchool;
                locationToBeCreated.LocationId = Convert.ToInt32(cmd.ExecuteScalar());
                Locations.Add(locationToBeCreated);
            }
        }
        public void RetrieveAll()
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();

                SqlCommand cmd = new SqlCommand("SELECT Name, Address, IsConstructionSite, IsSchool FROM LOCATION", con);

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        Location location = new Location()
                        {
                            Name = dr["Name"].ToString(),
                            Address = dr["Address"].ToString(),
                            IsConstructionSite = bool.Parse(dr["IsConstructionSite"].ToString()),
                            IsSchool = bool.Parse(dr["IsSchool"].ToString()),
                        };
                        Locations.Add(location);
                    }
                }
            }
        }
        public void Update(Location locationToBeUpdated)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();

                SqlCommand cmd = new SqlCommand("UPDATE LOCATION SET Name = @Name, Address = @Address, IsConstructionSite = @IsConstructionSite, IsSchool = @IsSchool WHERE LocationId = @LocationId", con);
                cmd.Parameters.Add("@LocationId", SqlDbType.Int).Value = locationToBeUpdated.LocationId;
                cmd.Parameters.Add("@Name", SqlDbType.NVarChar).Value = locationToBeUpdated.Name;
                cmd.Parameters.Add("@Address", SqlDbType.NVarChar).Value = locationToBeUpdated.Address;
                cmd.Parameters.Add("@IsConstructionSite", SqlDbType.Bit).Value = locationToBeUpdated.IsConstructionSite;
                cmd.Parameters.Add("@IsSchool", SqlDbType.Bit).Value = locationToBeUpdated.IsSchool;
                cmd.ExecuteNonQuery();
            }
        }
        public void Delete(Location locationToBeDeleted)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();

                SqlCommand cmd = new SqlCommand("DELETE FROM LOCATION WHERE LocationId = @LocationId", con);
                cmd.Parameters.Add("@LocationId", SqlDbType.Int).Value = locationToBeDeleted.LocationId;
                cmd.ExecuteNonQuery();
            }
            Locations.Remove(locationToBeDeleted);
        }
    }
}
