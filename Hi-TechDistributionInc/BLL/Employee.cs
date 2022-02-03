using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hi_TechDistributionInc.DAL;

namespace Hi_TechDistributionInc.BLL
{
    public class Employee
    {
        private int employeeId, jobId;
        private string firstName, lastName, phoneNumber, email;

        public int EmployeeId { get => employeeId; set => employeeId = value; }
        public string FirstName { get => firstName; set => firstName = value; }
        public string LastName { get => lastName; set => lastName = value; }
        public string PhoneNumber { get => phoneNumber; set => phoneNumber = value; }
        public string Email { get => email; set => email = value; }
        public int JobId { get => jobId; set => jobId = value; }
        public List<Employee> ListAllRecords()
        {
            return EmployeeDB.ListAllRecords();
        }
        public void SaveEmployee(Employee emp)
        {
            EmployeeDB.SaveRecord(emp);
        }
        public Employee SearchRecord(int empId)
        {
            EmployeeDB.SearchRecord(empId);
            return EmployeeDB.SearchRecord(empId);
        }
        public void UpdateRecord(Employee emp)
        {
            EmployeeDB.UpdateRecord(emp);
        }
        public void DeleteRecord(Employee emp)
        {
            EmployeeDB.DeleteRecord(emp);
        }
        public List<Employee> SearchRecord(string input)
        {
            return EmployeeDB.SearchRecord(input);
        }
        public List<Employee> SearchJobId(int input)
        {
            return EmployeeDB.SearchJobId(input);
        }
    }
}
