using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Hi_TechDistributionInc.BLL;

namespace Hi_TechDistributionInc.DAL
{
    public static class EmployeeDB
    {
        public static List<Employee> ListAllRecords()
        {
            List<Employee> listEmp = new List<Employee>();
            SqlConnection connDB = UtilityDB.ConnectDB();
            //Connect
            connDB = UtilityDB.ConnectDB();

            //Create new sql command
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "Select * FROM Employees ";
            cmd.Connection = connDB;
            SqlDataReader sqlReader = cmd.ExecuteReader(); //Found

            Employee emp;
            //Check if it exists
            while (sqlReader.Read())
            {
                //Store the data in the object
                emp = new Employee();
                emp.EmployeeId = Convert.ToInt32(sqlReader["EmployeeId"]);
                emp.FirstName = sqlReader["FirstName"].ToString();
                emp.LastName = sqlReader["LastName"].ToString();
                emp.PhoneNumber = sqlReader["PhoneNumber"].ToString();
                emp.Email = sqlReader["Email"].ToString();
                emp.JobId = Convert.ToInt32(sqlReader["JobId"].ToString());
                listEmp.Add(emp);
            }

            return listEmp;

        }
        public static void SaveRecord(Employee emp)
        {
            SqlConnection connDB = UtilityDB.ConnectDB();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "INSERT INTO Employees(EmployeeId,FirstName,LastName,PhoneNumber,Email,JobId) " +
                              "VALUES(@employeeId,@FirstName,@LastName,@PhoneNumber,@Email, @JobId)";
            cmd.Parameters.AddWithValue("@EmployeeId", emp.EmployeeId);
            cmd.Parameters.AddWithValue("@FirstName", emp.FirstName);
            cmd.Parameters.AddWithValue("@LastName", emp.LastName);
            cmd.Parameters.AddWithValue("@PhoneNumber", emp.PhoneNumber);
            cmd.Parameters.AddWithValue("@Email", emp.Email);
            cmd.Parameters.AddWithValue("@JobId", emp.JobId);

            cmd.Connection = connDB;
            cmd.ExecuteNonQuery();

            connDB.Close();
        }
        public static Employee SearchRecord(int Id)
        {

            Employee emp = new Employee();
            SqlConnection connDB = UtilityDB.ConnectDB();

            connDB = UtilityDB.ConnectDB();


            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "Select * FROM Employees " +
                              "WHERE EmployeeId = (@EmployeeId)";
            cmd.Parameters.AddWithValue("@EmployeeId", Id);
            cmd.Connection = connDB;
            SqlDataReader sqlReader = cmd.ExecuteReader();


            if (sqlReader.Read())
            {
                //Store the data in the object
                emp = new Employee();
                emp.EmployeeId = Convert.ToInt32(sqlReader["EmployeeId"]);
                emp.FirstName = sqlReader["FirstName"].ToString();
                emp.LastName = sqlReader["LastName"].ToString();
                emp.PhoneNumber = sqlReader["PhoneNumber"].ToString();
                emp.Email = sqlReader["Email"].ToString();
                emp.JobId = Convert.ToInt32(sqlReader["JobId"]);
            }
            else
            {
                emp = null;
            }

            return emp;
        }
        public static void UpdateRecord(Employee emp)
        {
            SqlConnection connDB = UtilityDB.ConnectDB();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connDB;
            cmd.CommandText = "UPDATE Employees " +
                     "SET EmployeeId =(@employeeId), FirstName = (@FirstName), LastName = (@LastName), PhoneNumber = (@PhoneNumber), Email = (@Email), JobId = (@JobId) " +
                     "WHERE EmployeeId =(@employeeId);";
            cmd.Parameters.AddWithValue("@EmployeeId", emp.EmployeeId);
            cmd.Parameters.AddWithValue("@FirstName", emp.FirstName);
            cmd.Parameters.AddWithValue("@LastName", emp.LastName);
            cmd.Parameters.AddWithValue("@PhoneNumber", emp.PhoneNumber);
            cmd.Parameters.AddWithValue("@Email", emp.Email);
            cmd.Parameters.AddWithValue("@JobId", emp.JobId);
            cmd.ExecuteNonQuery();
            connDB.Close();
        }
        public static void DeleteRecord(Employee emp)
        {
            SqlConnection connDB = UtilityDB.ConnectDB();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connDB;
            cmd.CommandText = ("DELETE FROM Employees "
            + "WHERE EmployeeId = (@EmployeeId)");
            cmd.Parameters.AddWithValue("@EmployeeId", emp.EmployeeId);
            cmd.ExecuteNonQuery();
            connDB.Close();
        }
        public static List<Employee> SearchRecord(string input)
        {
            List<Employee> listEmp = new List<Employee>();
            SqlConnection connDB = UtilityDB.ConnectDB();
            connDB = UtilityDB.ConnectDB();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "Select * FROM Employees " +
                              "WHERE FirstName = @FirstName "
                              + "OR LastName = @LastName";
            cmd.Parameters.AddWithValue("@FirstName", input);
            cmd.Parameters.AddWithValue("@LastName", input);
            cmd.Connection = connDB;
            SqlDataReader sqlReader = cmd.ExecuteReader();

            Employee emp;
            while (sqlReader.Read())
            {
                emp = new Employee();
                emp.EmployeeId = Convert.ToInt32(sqlReader["EmployeeId"]);
                emp.FirstName = sqlReader["FirstName"].ToString();
                emp.LastName = sqlReader["LastName"].ToString();
                emp.PhoneNumber = sqlReader["PhoneNumber"].ToString();
                emp.Email = sqlReader["Email"].ToString();
                emp.JobId = Convert.ToInt32(sqlReader["JobId"]);
                listEmp.Add(emp);
            }

            return listEmp;
        }
        public static List<Employee> SearchJobId(int input)
        {
            List<Employee> listEmp = new List<Employee>();
            SqlConnection connDB = UtilityDB.ConnectDB();
            connDB = UtilityDB.ConnectDB();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "Select * FROM Employees " +
                              "WHERE JobId = @JobId";
            cmd.Parameters.AddWithValue("@JobId", input);
            cmd.Connection = connDB;
            SqlDataReader sqlReader = cmd.ExecuteReader();

            Employee emp;
            while (sqlReader.Read())
            {
                emp = new Employee();
                emp.EmployeeId = Convert.ToInt32(sqlReader["EmployeeId"]);
                emp.FirstName = sqlReader["FirstName"].ToString();
                emp.LastName = sqlReader["LastName"].ToString();
                emp.PhoneNumber = sqlReader["PhoneNumber"].ToString();
                emp.Email = sqlReader["Email"].ToString();
                emp.JobId = Convert.ToInt32(sqlReader["JobId"]);
                listEmp.Add(emp);
            }

            return listEmp;
        }
    }
}
