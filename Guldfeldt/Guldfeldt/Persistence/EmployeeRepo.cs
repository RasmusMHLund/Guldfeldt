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
        string? connectionString = "Data Source = 10.56.8.35; Initial Catalog = DB_2024_72; Persist Security Info=True;User ID = STUDENT_2024_72; Password=OPENDB_72;Encrypt=True;Trust Server Certificate=True";

        private List<Employee> Employees;
        public EmployeeRepo()
        {
            Employees = new List<Employee>();
        }
        public List<Employee> GetEmployees()   { return Employees; }

        public void Create(Employee employeeToBeCreated)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("INSERT INTO EMPLOYEE (FullName, PhoneNumber, Email, SalaryNumber, CurrentWorkplace, SocialSecurityNumber, IsApprentice, IsJourneyman, IsMentor)" +
                                                 "VALUES(@FullName,@PhoneNumber,@Email,@SalaryNumber,@CurrentWorkplace,@SocialSecurityNumber,@IsApprentice,@IsJourneyman,@IsMentor)" +
                                                 "SELECT SalaryNumber FROM EMPLOYEE WHERE SalaryNumber = SCOPE_IDENTITY()", con);
                cmd.Parameters.Add("@FullName", SqlDbType.NVarChar).Value = (object)employeeToBeCreated.FullName ?? DBNull.Value;
                cmd.Parameters.Add("@PhoneNumber", SqlDbType.Int).Value = (object)employeeToBeCreated.PhoneNumber ?? DBNull.Value;
                cmd.Parameters.Add("@Email", SqlDbType.NVarChar).Value = (object)employeeToBeCreated.Email ?? DBNull.Value;
                cmd.Parameters.Add("@SalaryNumber", SqlDbType.Int).Value = (object)employeeToBeCreated.SalaryNumber ?? DBNull.Value;
                cmd.Parameters.Add("@CurrentWorkplace", SqlDbType.NVarChar).Value = (object)employeeToBeCreated.CurrentWorkplace ?? DBNull.Value;
                cmd.Parameters.Add("@SocialSecurityNumber", SqlDbType.NVarChar).Value = (object)employeeToBeCreated.SocialSecurityNumber ?? DBNull.Value;
                cmd.Parameters.Add("@IsApprentice", SqlDbType.Bit).Value = employeeToBeCreated.IsApprentice;
                cmd.Parameters.Add("@IsJourneyman", SqlDbType.Bit).Value = employeeToBeCreated.IsJourneyman;
                cmd.Parameters.Add("@IsMentor", SqlDbType.Bit).Value = employeeToBeCreated.IsMentor;
                employeeToBeCreated.SalaryNumber = Convert.ToInt32(cmd.ExecuteScalar());
                Employees.Add(employeeToBeCreated);
            }
        }

        public void RetrieveAll(string query)
        {
            List<Employee> employeeList = new List<Employee>();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();

                SqlCommand cmd = new SqlCommand(query, con);

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        Employee employee = new Employee()
                        {
                          
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
                        employeeList.Add(employee);
                    }
                }
            }
            employeeList = employeeList.OrderBy(e => e.FullName).ToList();

            Employees.Clear();

            foreach (Employee employee in employeeList)
            {
                Employees.Add(employee);
            }
        }
        public void RetrieveAll(string query, string parameterName, string parameterValue)
        {
            List<Employee> employeeList = new List<Employee>();

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
                            Employee employee = new Employee()
                            {

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
                            employeeList.Add(employee);
                        }
                    }
                }
            }
            employeeList = employeeList.OrderBy(e => e.FullName).ToList();

            Employees.Clear();

            foreach (Employee employee in employeeList)
            {
                Employees.Add(employee);
            }
        }

        public void Update(Employee employeeToBeUpdated)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();

                SqlCommand cmd = new SqlCommand("UPDATE EMPLOYEE SET FullName = @FullName, PhoneNumber = @PhoneNumber, Email = @Email, SalaryNumber = @SalaryNumber, CurrentWorkplace = @CurrentWorkplace, SocialSecurityNumber = @SocialSecurityNumber, IsApprentice = @IsApprentice, IsJourneyman = @IsJourneyman, IsMentor = @IsMentor WHERE SalaryNumber = @SalaryNumber", con);
                cmd.Parameters.Add("@FullName", SqlDbType.NVarChar).Value = (object)employeeToBeUpdated.FullName ?? DBNull.Value;
                cmd.Parameters.Add("@PhoneNumber", SqlDbType.Int).Value = (object)employeeToBeUpdated.PhoneNumber ?? DBNull.Value;
                cmd.Parameters.Add("@Email", SqlDbType.NVarChar).Value = (object)employeeToBeUpdated.Email ?? DBNull.Value;
                cmd.Parameters.Add("@SalaryNumber", SqlDbType.Int).Value = (object)employeeToBeUpdated.SalaryNumber ?? DBNull.Value;
                cmd.Parameters.Add("@CurrentWorkplace", SqlDbType.NVarChar).Value = (object)employeeToBeUpdated.CurrentWorkplace ?? DBNull.Value;
                cmd.Parameters.Add("@SocialSecurityNumber", SqlDbType.NVarChar).Value = (object)employeeToBeUpdated.SocialSecurityNumber ?? DBNull.Value;
                cmd.Parameters.Add("@IsApprentice", SqlDbType.Bit).Value = employeeToBeUpdated.IsApprentice;
                cmd.Parameters.Add("@IsJourneyman", SqlDbType.Bit).Value = employeeToBeUpdated.IsJourneyman;
                cmd.Parameters.Add("@IsMentor", SqlDbType.Bit).Value = employeeToBeUpdated.IsMentor;
                cmd.ExecuteNonQuery();
            }
        }

        public void Delete(Employee employeeToBeDeleted)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();

                SqlCommand cmd = new SqlCommand("DELETE FROM EMPLOYEE WHERE SalaryNumber = @SalaryNumber", con);
                cmd.Parameters.Add("@SalaryNumber", SqlDbType.NVarChar).Value = employeeToBeDeleted.SalaryNumber;
                cmd.ExecuteNonQuery();
            }
            Employees.Remove(employeeToBeDeleted);
        }
    }
}
