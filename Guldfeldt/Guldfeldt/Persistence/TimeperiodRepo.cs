using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Guldfeldt.Model;
using Microsoft.Data.SqlClient;
using System.Data;

namespace Guldfeldt.Persistence
{
    public class TimeperiodRepo
    {
        string? connectionString = "Data Source = 10.56.8.35; Initial Catalog = DB_2024_72; Persist Security Info=True;User ID = STUDENT_2024_72; Password=OPENDB_72;Encrypt=True;Trust Server Certificate=True";

        private List<Timeperiod> Timeperiods;
        public TimeperiodRepo() 
        {
            Timeperiods = new List<Timeperiod>();
        }
        public List<Timeperiod> GetTimeperiods()
        {
            return Timeperiods;
        }

        public void Create(Timeperiod timeperiodToBeCreated)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("INSERT INTO TIMEPERIOD (Period)" +
                                                 "VALUES(@Period)" +
                                                 "SELECT @@IDENTITY", con);
                cmd.Parameters.Add("@Period", SqlDbType.DateTime2).Value = timeperiodToBeCreated.Period;
                timeperiodToBeCreated.TimeperiodId = Convert.ToInt32(cmd.ExecuteScalar());
                Timeperiods.Add(timeperiodToBeCreated);
            }
        }
        public void RetrieveAll()
        {
            List<Timeperiod> timeperiodList = new List<Timeperiod>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();

                SqlCommand cmd = new SqlCommand("SELECT * FROM TIMEPERIOD", con);

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        Timeperiod timeperiod = new Timeperiod()
                        {
                            TimeperiodId = int.Parse(dr["TimeperiodId"].ToString()),
                            Period = DateTime.Parse(dr["Period"].ToString())
                        };

                        Timeperiods.Add(timeperiod);
                    }
                }
            }
        }
        public void Update(Timeperiod timeperiodToBeUpdated)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();

                SqlCommand cmd = new SqlCommand("UPDATE TIMEPERIOD SET Period = @Period WHERE TimeperiodId = @TimeperiodId", con);
                cmd.Parameters.Add("@TimeperiodId", SqlDbType.Int).Value = timeperiodToBeUpdated.TimeperiodId;
                cmd.Parameters.Add("@Period", SqlDbType.DateTime2).Value = timeperiodToBeUpdated.Period;
                cmd.ExecuteNonQuery();
            }
        }
        public void Delete(Timeperiod timeperiodToBeDeleted)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();

                SqlCommand cmd = new SqlCommand("DELETE FROM TIMEPERIOD WHERE TimeperiodId = @TimeperiodId", con);
                cmd.Parameters.Add("@TimeperiodId", SqlDbType.Int).Value = timeperiodToBeDeleted.TimeperiodId;
                cmd.ExecuteNonQuery();
            }

            Timeperiods.Remove(timeperiodToBeDeleted);

        }
    }
}