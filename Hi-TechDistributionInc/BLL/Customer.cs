using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hi_TechDistributionInc.DAL;

namespace Hi_TechDistributionInc.BLL
{
    public class Customer
    {
        private string firstName, lastName, city, address, postalCode, phoneNumber, faxNumber;
        private float creditLimit;
        private int customerId;

        public int CustomerId { get => customerId; set => customerId = value;}
        public string FirstName { get => firstName; set => firstName = value;}
        public string LastName { get => lastName; set => lastName = value;}
        public string City { get => city; set => city = value;}
        public string Address { get => address; set => address = value;}
        public string PostalCode { get => postalCode; set => postalCode = value;}
        public string PhoneNumber { get => phoneNumber; set => phoneNumber = value;}
        public string FaxNumber { get => faxNumber; set => faxNumber = value;}
        public float CreditLimit { get => creditLimit; set => creditLimit = value; }
        public List<Customer> ListAllRecords()
        {
            return CustomerDb.ListAllRecords();
        }
    }

}
