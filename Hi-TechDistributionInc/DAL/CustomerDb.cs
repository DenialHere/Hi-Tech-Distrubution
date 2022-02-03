using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Hi_TechDistributionInc.BLL;

namespace Hi_TechDistributionInc.DAL
{
    public static class CustomerDb
    {
        public static List<Customer> ListAllRecords()
        {
            List<Customer> listUser = new List<Customer>();
            SqlConnection connDB = UtilityDB.ConnectDB();
            connDB = UtilityDB.ConnectDB();


            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "Select * FROM Customers ";
            cmd.Connection = connDB;
            SqlDataReader sqlReader = cmd.ExecuteReader();

            Customer cus;

            while (sqlReader.Read())
            {
                //Store the data in the object
                cus = new Customer();
                cus.CustomerId = Convert.ToInt32(sqlReader["CustomerId"]);
                cus.FirstName = sqlReader["FirstName"].ToString();
                cus.LastName = sqlReader["LastName"].ToString();
                cus.City = sqlReader["City"].ToString();
                cus.Address = sqlReader["Address"].ToString();
                cus.PostalCode = sqlReader["PostalCode"].ToString();
                cus.PhoneNumber = sqlReader["PhoneNumber"].ToString();
                cus.FaxNumber = sqlReader["FaxNumber"].ToString();
                cus.CreditLimit = Convert.ToInt32(sqlReader["CreditLimit"]);
                listUser.Add(cus);
            }

            return listUser;

        }
    }
}
