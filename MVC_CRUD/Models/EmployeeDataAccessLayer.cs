using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace MVC_CRUD.Models
{
    public class EmployeeDataAccessLayer
    {
        string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=DBmvcCRUD;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        // View all Employee Details
        public IEnumerable<Employee> GetAllEmployees()            
        {
            List<Employee> lstEmployee = new List<Employee>();
            
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("spReadAllEmployees", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while(reader.Read())
                {
                    Employee employee = new Employee();
                    employee.ID = Convert.ToInt32(reader["EmployeeId"]);
                    employee.Name = reader["Name"].ToString();
                    employee.City = reader["City"].ToString();
                    employee.Language = reader["Language"].ToString();

                    lstEmployee.Add(employee);
                }
                conn.Close();
            }
            return lstEmployee;
        }

        public void AddEmployee(Employee employee)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("spCreateEmployee", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Name", employee.Name);
                cmd.Parameters.AddWithValue("@City", employee.City);
                cmd.Parameters.AddWithValue("@Language", employee.Language);
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
            }
        }

        // Update particluar employee
        public void UpdateEmployee(Employee employee)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("spUpdateEmployee", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@EmployeeId", employee.ID);
                cmd.Parameters.AddWithValue("@Name", employee.Name);
                cmd.Parameters.AddWithValue("@City", employee.City);
                cmd.Parameters.AddWithValue("@Language", employee.Language);
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
            }
        }

        // Read details of a particular employee
        public Employee GetEmployeeDate(int? id)
        {
            Employee employee = new Employee();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string sql = "SELECT * " +
                             "FROM [dbo].[Employees] " +
                             "WHERE EmployeeId = " + id;
                SqlCommand cmd = new SqlCommand(sql, conn);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    employee.ID = Convert.ToInt32(reader["EmployeeId"]);
                    employee.Name = reader["Name"].ToString();
                    employee.City = reader["City"].ToString();
                    employee.Language = reader["Language"].ToString();
                }
            }
            return employee;
        }

        // Delete particular employee
        public void DeleteEmployee(int? id)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("spDeleteEmployee", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@EmployeeId", id);
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
            }
        }
    }
}
