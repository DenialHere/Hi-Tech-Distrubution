using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Hi_TechDistributionInc.BLL;

namespace Hi_TechDistributionInc.DAL
{
    public static class UserDB
    {
        public static void SaveRecord(Users use)
        {
            SqlConnection connDB = UtilityDB.ConnectDB();
            SqlCommand cmd = new SqlCommand();

            if (use.EmployeeId != null)
            {
                cmd.CommandText = "INSERT INTO Users(UserName,Password,EmployeeId) " +
                              "VALUES(@UserName,@Password,@EmployeeId)";
                cmd.Parameters.AddWithValue("@EmployeeId", use.EmployeeId);
            }
            else
            {
                cmd.CommandText = "INSERT INTO Users(UserName,Password) " +
                                              "VALUES(@UserName,@Password)";
            }

            cmd.Parameters.AddWithValue("@UserName", use.UserName);
            cmd.Parameters.AddWithValue("@Password", use.Password);

            cmd.Connection = connDB;
            cmd.ExecuteNonQuery();

            connDB.Close();
        }
        public static List<Users> ListAllRecords()
        {
            List<Users> listUser = new List<Users>();
            SqlConnection connDB = UtilityDB.ConnectDB();
            connDB = UtilityDB.ConnectDB();


            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "Select * FROM Users ";
            cmd.Connection = connDB;
            SqlDataReader sqlReader = cmd.ExecuteReader();

            Users use;
            //Check if it exists
            while (sqlReader.Read())
            {
                //Store the data in the object
                use = new Users();
                use.UserName = sqlReader["UserName"].ToString();
                use.Password = sqlReader["Password"].ToString();
                use.EmployeeId = Int32.TryParse(sqlReader["EmployeeId"].ToString(), out var i) ? (int?)i : null;
                listUser.Add(use);
            }

            return listUser;
        }
        public static void UpdateRecord(Users use)
        {
            SqlConnection connDB = UtilityDB.ConnectDB();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connDB;
            if (use.EmployeeId != null)
            {
                cmd.CommandText = "UPDATE Users " +
                     "SET UserName =(@UserName), Password = (@Password), EmployeeId = (@EmployeeId)" +
                     "WHERE UserName =(@UserName);";
                cmd.Parameters.AddWithValue("@EmployeeId", use.EmployeeId);
            }
            else
            {
                cmd.CommandText = "UPDATE Users " +
                         "SET UserName =(@UserName), Password = (@Password)" +
                         "WHERE UserName =(@UserName);";
            }
            cmd.Parameters.AddWithValue("@UserName", use.UserName);
            cmd.Parameters.AddWithValue("@Password", use.Password);
            cmd.ExecuteNonQuery();
            connDB.Close();
        }
        public static void DeleteRecord(Users use)
        {
            SqlConnection connDB = UtilityDB.ConnectDB();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connDB;
            cmd.CommandText = ("DELETE FROM Users "
            + "WHERE UserName = (@UserName)");
            cmd.Parameters.AddWithValue("@UserName", use.UserName);
            cmd.ExecuteNonQuery();
            connDB.Close();
        }
        public static Users SearchRecord(string input)
        {
            Users use = new Users();
            SqlConnection connDB = UtilityDB.ConnectDB();
            connDB = UtilityDB.ConnectDB();


            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "Select * FROM Users " +
                              "WHERE UserName = (@UserName)";
            cmd.Parameters.AddWithValue("@UserName", input);
            cmd.Connection = connDB;
            SqlDataReader sqlReader = cmd.ExecuteReader();

            if (sqlReader.Read())
            {
                use = new Users();
                use.UserName = sqlReader["UserName"].ToString();
                use.Password = sqlReader["Password"].ToString();
                use.EmployeeId = Int32.TryParse(sqlReader["EmployeeId"].ToString(), out var i) ? (int?)i : null;
            }
            else
            {
                use = null;
            }

            return use;
        }
        public static Users Login(string uName, string pass)
        {
            Users use = new Users();
            SqlConnection connDB = UtilityDB.ConnectDB();
            connDB = UtilityDB.ConnectDB();


            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "Select * FROM Users " +
                              "WHERE UserName = (@UserName) " +
                              "AND Password = (@Password)";
            cmd.Parameters.AddWithValue("@UserName", uName);
            cmd.Parameters.AddWithValue("@Password", pass);
            cmd.Connection = connDB;
            SqlDataReader sqlReader = cmd.ExecuteReader();

            if (sqlReader.Read())
            {
                try
                {
                    use = new Users();
                    use.UserName = sqlReader["UserName"].ToString();
                    use.Password = sqlReader["Password"].ToString();
                    use.AccessLevel = Convert.ToInt32(sqlReader["AccessLevel"].ToString());
                    use.EmployeeId = Int32.TryParse(sqlReader["EmployeeId"].ToString(), out var i) ? (int?)i : null;
                }
                catch
                {
                    
                }
            }
            else
            {
                use = null;
            }

            return use;
        }
        public static List<Users> SearchRecordEmployeeId(int input)
        {
            List<Users> listUse = new List<Users>();
            SqlConnection connDB = UtilityDB.ConnectDB();
            connDB = UtilityDB.ConnectDB();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "Select * FROM Users " +
                              "WHERE EmployeeId = (@EmployeeId)";
            cmd.Parameters.AddWithValue("@EmployeeId", input);
            cmd.Connection = connDB;
            SqlDataReader sqlReader = cmd.ExecuteReader();

            Users use;

            while (sqlReader.Read())
            {
                use = new Users();
                use.UserName = sqlReader["UserName"].ToString();
                use.Password = sqlReader["Password"].ToString();
                use.EmployeeId = Int32.TryParse(sqlReader["EmployeeId"].ToString(), out var i) ? (int?)i : null;
                listUse.Add(use);
            }


            return listUse;

        }
        public static void  ChangePassword(Users use)
        {
            SqlConnection connDB = UtilityDB.ConnectDB();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connDB;
                cmd.CommandText = "UPDATE Users " +
                     "SET Password = (@Password) " +
                     "WHERE UserName =(@UserName);";
            cmd.Parameters.AddWithValue("@UserName", use.UserName);
            cmd.Parameters.AddWithValue("@Password", use.Password);
            cmd.ExecuteNonQuery();
            connDB.Close();

        }
    }
}