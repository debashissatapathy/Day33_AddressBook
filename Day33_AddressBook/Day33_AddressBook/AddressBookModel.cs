using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day33_AddressBook
{
    public class AddressBookModel
    {
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string AddressBookName { get; set; }
        public string AddressBookType { get; set; }

    }

    public class AddressBook
    {
        //TableName in DB - AddressBook
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public int Zip { get; set; }
        public string PhoneNumber { get; set; }
        public string Email_ID { get; set; }

    }
    public class Address_Book1
    {
        //Table Name - Address_Book
        public int AddressBookId { get; set; }
        public string AddressBookName { get; set; }
    }
    public class PersonDetail1
    {
        //Table Name - PersonDetail

        public int PersonId { get; set; }
        public int AddressBookId { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public int Zip { get; set; }
        public long PhoneNumber { get; set; }
        //is in bigint
        public string Email_ID { get; set; }
    }

    public class PersonTypes1
    {
        //Table Name - PersonTypes
        public int PersonTypeId { get; set; }
        public string PersonType { get; set; }
    }
    public class PersonsDetail_Type1
    {
        //Table Name - PersonsDetail_Type
        public int PersonId { get; set; }
        public int PersonTypeId { get; set; }
    }
    public class Employee_Department1
    {
        //Table Name - Employee_Department
        public int PersonId { get; set; }
        public int EmployeeID { get; set; }
        public int DepartmentID { get; set; }
    }
}
