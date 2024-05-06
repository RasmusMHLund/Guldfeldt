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
    public  class EmployeeRepo
    {
        string? ConnectionString;

        private List<Employee> Employees;
        public EmployeeRepo()
        {
            Employees = new List<Employee>();

            IConfigurationRoot config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();

            string? ConnectionString = config.GetConnectionString("MyDBConnection");

        }

        public void Create(Employee employeeToBeCreated)
        {
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("INSERT INTO EMPLOYEE (FullName, PhoneNumber, Email, SalaryNumber, CurrentWorkplace, SocialSecurityNumber, IsApprentice, IsJourneyman, IsMentor)" +
                                                 "VALUES(@FullName,@PhoneNumber,@Email,@SalaryNumber,@CurrentWorkplace,@SocialSecurityNumber,@IsApprentice,@IsJourneyman,@IsMentor)" +
                                                 "SELECT @@IDENTITY", con);
                cmd.Parameters.Add("@FullName", SqlDbType.NVarChar).Value = employeeToBeCreated.FullName;
                cmd.Parameters.Add("@PhoneNumber", SqlDbType.Int).Value = employeeToBeCreated.PhoneNumber;
                cmd.Parameters.Add("@Email", SqlDbType.NVarChar).Value = employeeToBeCreated.Email;
                cmd.Parameters.Add("@SalaryNumber", SqlDbType.Int).Value = employeeToBeCreated.SalaryNumber;
                cmd.Parameters.Add("@CurrentWorkplace", SqlDbType.NVarChar).Value = employeeToBeCreated.CurrentWorkplace;
                cmd.Parameters.Add("@SocialSecurityNumber", SqlDbType.NVarChar).Value = employeeToBeCreated.SocialSecurityNumber;
                cmd.Parameters.Add("@IsApprentice", SqlDbType.Bit).Value = employeeToBeCreated.IsApprentice;
                cmd.Parameters.Add("@IsJourneyman", SqlDbType.Bit).Value = employeeToBeCreated.IsJourneyman;
                cmd.Parameters.Add("@IsMentor", SqlDbType.Bit).Value = employeeToBeCreated.IsMentor;
                employeeToBeCreated.Id = Convert.ToInt32(cmd.ExecuteScalar());
                Employees.Add(employeeToBeCreated);
            }
        }

        public void RetrieveAll()
        {
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                con.Open();

                SqlCommand cmd = new SqlCommand("SELECT FullName, PhoneNumber, Email, SalaryNumber, CurrentWorkplace, SocialSecurityNumber, IsApprentice, IsJourneyman, IsMentor FROM EMPLOYEE", con);

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        Employee employee = new Employee()
                        {
                            Id = int.Parse(dr["Id"].ToString()),
                            FullName = dr["FullName"].ToString(),
                            PhoneNumber = int.Parse(dr["PhoneNumber"].ToString()),
                            Email = dr["Email"].ToString(),
                            SalaryNumber = int.Parse(dr["SalaryNumber"].ToString()),
                            CurrentWorkplace = dr["CurrentWorkplace"].ToString(),
                            SocialSecurityNumber = dr["SocialSecurityNumber"].ToString(),
                            IsApprentice = bool.Parse(dr["IsApprentice"].ToString()),
                            IsJourneyman = bool.Parse(dr["IsJourneyman"].ToString()),
                            IsMentor = bool.Parse(dr["IsMentor"].ToString()),
                        };
                        Employees.Add(employee);
                    }
                }
            }
        }

        public void Update(Employee employeeToBeUpdated)
        {
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                con.Open();

                SqlCommand cmd = new SqlCommand("UPDATE EMPLOYEE SET FullName = @FullName, PhoneNumber = @PhoneNumber, Email = @Email, SalaryNumber = @SalaryNumber, CurrentWorkplace = @CurrentWorkplace, SocialSecurityNumber = @SocialSecurityNumber, IsApprentice = @IsApprentice, IsJourneyman = @IsJourneyman, IsMentor = @IsMentor WHERE Id = @Id", con);
                cmd.Parameters.Add("@Id", SqlDbType.NVarChar).Value = employeeToBeUpdated.Id;
                cmd.ExecuteNonQuery();
            }
        }

        public void Delete(Employee employeeToBeDeleted)
        {
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                con.Open();

                SqlCommand cmd = new SqlCommand("DELETE FROM EMPLOYEE WHERE Id = @Id", con);
                cmd.Parameters.Add("@Id", SqlDbType.NVarChar).Value = employeeToBeDeleted.Id;
                cmd.ExecuteNonQuery();
            }
            Employees.Remove(employeeToBeDeleted);
        }
    }
}
