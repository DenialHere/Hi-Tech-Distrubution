using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hi_TechDistributionInc.DAL;

namespace Hi_TechDistributionInc.BLL
{
    public class Users
    {
        private string username, password;
        private int? employeeId;
        private int accessLevel;

        public string UserName { get => username; set => username = value; }
        public string Password { get => password; set => password = value; }
        public int? EmployeeId { get => employeeId; set => employeeId = value; }
        public int AccessLevel { get => accessLevel; set => accessLevel = value; }
        public List<Users> ListAllRecords()
        {
            return UserDB.ListAllRecords();
        }
        public void SaveUser(Users use)
        {
            UserDB.SaveRecord(use);
        }
        public void UpdateRecord(Users use)
        {
            UserDB.UpdateRecord(use);
        }
        public void DeleteRecord(Users use)
        {
            UserDB.DeleteRecord(use);
        }
        public Users SearchRecord(string input)
        {
            UserDB.SearchRecord(input);
            return UserDB.SearchRecord(input);
        }
        public Users Login(string uName, string pass)
        {
            UserDB.Login(uName, pass);
            return UserDB.Login(uName, pass);
        }
        public void ChangePassword(Users use)
        {
            UserDB.ChangePassword(use);
        }
        public List<Users> SearchRecord(int input)
        {
            return UserDB.SearchRecordEmployeeId(input);
        }
    }
}
