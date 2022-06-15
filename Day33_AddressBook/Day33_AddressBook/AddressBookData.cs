using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day33_AddressBook
{
    public class AddressBookData
    {
        public string connectionString = @"Data Source=DESKTOP-195K6F7; Initial Catalog =master; Integrated Security = True;";
        AddressBookModel model = new AddressBookModel();
        public void Create_Database()
        {
            try
            {
                SqlConnection connection = new SqlConnection(connectionString);
                connection.Open();
                SqlCommand cmd = new SqlCommand("Create database AddressBookForADO;", connection);
                cmd.ExecuteNonQuery();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("AddressbookService Database created successfully!");
                Console.ResetColor();
                connection.Close();                
            }
            catch (Exception e)
            {
                Console.WriteLine("exception occured while creating database:" + e.Message + "\t");
            }
        }

        /// Created Table in addressbook service database <summary>
        /// Created Table in addressbook service database
        /// </summary>
        public void CreateTable()
        {
            try
            {
                SqlConnection connection = new SqlConnection(connectionString);
                connection.Open();
                SqlCommand cmd = new SqlCommand("Create table AddressBook(id int identity(1,1)primary key,FirstName varchar(200),LastName varchar(200),Address varchar(200), City varchar(200), State varchar(200), Zip varchar(200), PhoneNumber varchar(50), Email varchar(200)); ", connection);
                cmd.ExecuteNonQuery();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("AddressBook table has been created successfully!");
                Console.ResetColor();
                connection.Close();
            }
            catch(Exception e)
            {
                Console.WriteLine("Exception occure while creating table: {0}", e.Message + "\t");
            }
            
        }
        //Created Connection file        
        /// <summary>
        /// Method to insert data in database
        /// </summary>
        /// <param name="model"></param>
        public bool AddContact(AddressBookModel model)
        {
            try
            {
                SqlConnection connection = new SqlConnection(connectionString);
                
                using (connection)
                {
                    SqlCommand cmd = new SqlCommand("SpAddressBook", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@FirstName", model.FirstName);
                    cmd.Parameters.AddWithValue("@LastName", model.LastName);
                    cmd.Parameters.AddWithValue("@Address", model.Address);
                    cmd.Parameters.AddWithValue("@City", model.City);
                    cmd.Parameters.AddWithValue("@State", model.State);
                    cmd.Parameters.AddWithValue("@Zip", model.Zip);
                    cmd.Parameters.AddWithValue("@PhoneNumber", model.PhoneNumber);
                    cmd.Parameters.AddWithValue("@Email", model.Email);

                    connection.Open();
                    

                    var result = cmd.ExecuteNonQuery();
                    connection.Close();
                   
                    if (result != 0)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkGreen;
                        Console.WriteLine("Commands completed successfully.");
                        Console.ResetColor();
                        return true;
                    }
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Error.");
                    Console.ResetColor();
                    return false;

                    
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return false;
        }

        /// Fetching all data from Database
        public void ViewContact()
        {
            try
            {
                SqlConnection connection = new SqlConnection(connectionString);
                connection.Open();
                using(connection)
                {
                    string Query = @"SELECT * from AddressBook";
                    SqlCommand cmd = new SqlCommand(Query, connection);
                    connection.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while(reader.Read())
                        {
                            model.ID = reader.GetInt32(0);
                            model.FirstName = reader.GetString(1);
                            model.LastName = reader.GetString(2);
                            model.Address = reader.GetString(3);
                            model.City = reader.GetString(4);
                            model.State = reader.GetString(5);
                            model.Zip = reader.GetString(6);
                            model.PhoneNumber = reader.GetString(7);
                            model.Email = reader.GetString(8);

                            Console.WriteLine(model.FirstName + " " +
                            model.LastName + " " +
                            model.Address + " " +
                            model.City + " " +
                            model.State + " " +
                            model.Zip + " " +
                            model.PhoneNumber + " " +
                            model.Email + " ");
                            Console.WriteLine();
                        }
                    }
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        public bool UpdateContact(AddressBookModel model)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                
                using (connection)
                {
                    SqlCommand cmd = new SqlCommand("SpAddressBookUpdate", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@FirstName", model.FirstName);
                    cmd.Parameters.AddWithValue("@LastName", model.LastName);
                    cmd.Parameters.AddWithValue("@Address", model.Address);
                    cmd.Parameters.AddWithValue("@City", model.City);
                    cmd.Parameters.AddWithValue("@State", model.State);
                    cmd.Parameters.AddWithValue("@Zip", model.Zip);
                    cmd.Parameters.AddWithValue("@PhoneNumber", model.PhoneNumber);
                    cmd.Parameters.AddWithValue("@Email", model.Email);

                    connection.Open();
                    var result = cmd.ExecuteNonQuery();
                    connection.Close();
                    if(result != 0)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("Contact updated successfully.");
                        Console.ResetColor();
                    }
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Error");
                    Console.ResetColor();
                    return false;

                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                connection.Close();  
            }
            return false;
        }
        // method to Update detail of already existing details
        public string UpdateEmployeeDetails()
        {
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            SqlCommand command = new SqlCommand("update AddressBook set Address='ChandiPur' where FirstName='Shravanthi'", connection);

            int effectedRow = command.ExecuteNonQuery();
            if (effectedRow == 1)
            {
                string query = @"Select Address from AddressBook where FirstName='Shravanthi';";
                SqlCommand cmd = new SqlCommand(query, connection);
                object res = cmd.ExecuteScalar();
                connection.Close();
                model.Address = (string)res;
            }
            connection.Close();
            return (model.Address);

        }
        public bool DeleteContact(AddressBookModel model)
        {
            SqlConnection connection = new SqlConnection(connectionString);   
            try
            {
                using (connection)
                {
                    SqlCommand cmd = new SqlCommand("SpAddressBook_Delete", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@FirstName", model.FirstName);
                    connection.Open();
                    var result = cmd.ExecuteNonQuery();
                    connection.Close();
                    if (result != 0)
                    {
                        return true;
                    }
                    return false;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                connection.Close();
            }
            return false;
        }
        //UC7-Size of AddressBook
        public int CountOfEmployeeDetailsByCity()
        {
            SqlConnection connection = new SqlConnection(connectionString);
            int count;            
            connection.Open();
            string query = @"Select count(*) from AddressBook where City='Bhadrak';";
            SqlCommand command = new SqlCommand(query, connection);
            object res = command.ExecuteScalar();
            connection.Close();
            int Count = (int)res;
            return Count;
        }
        public int CountOfEmployeeDetailsByState()
        {
            int count;
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            string query = @"Select count(*) from AddressBook where State='Moharastra';";
            SqlCommand command = new SqlCommand(query, connection);
            object res = command.ExecuteScalar();
            connection.Close();
            int Count = (int)res;
            return Count;
        }


        public void GetAllContactByState()
        {
            try
            {
                
                SqlConnection connection = new SqlConnection(connectionString);
                using (connection)
                {
                    string Query = @"Select * from AddressBook where State='Odisha';";
                    SqlCommand cmd = new SqlCommand(Query, connection);
                    connection.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            model.ID = reader.GetInt32(0);
                            model.FirstName = reader.GetString(1);
                            model.LastName = reader.GetString(2);
                            model.Address = reader.GetString(3);
                            model.City = reader.GetString(4);
                            model.State = reader.GetString(5);
                            model.Zip = reader.GetString(6);
                            model.PhoneNumber = reader.GetString(7);
                            model.Email = reader.GetString(8);

                            Console.WriteLine(model.FirstName + " " +
                                model.LastName + " " +
                                model.Address + " " +
                                model.City + " " +
                                model.State + " " +
                                model.Zip + " " +
                                model.PhoneNumber + " " +
                                model.Email + " ");
                                
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        public void GetAllContactByCity()
        {
            try
            {
                SqlConnection connection = new SqlConnection(connectionString);
                using (connection)
                {
                    string Query = @"Select * from AddressBook where City='Balasore';";
                    SqlCommand cmd = new SqlCommand(Query, connection);
                    connection.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            model.ID = reader.GetInt32(0);
                            model.FirstName = reader.GetString(1);
                            model.LastName = reader.GetString(2);
                            model.Address = reader.GetString(3);
                            model.City = reader.GetString(4);
                            model.State = reader.GetString(5);
                            model.Zip = reader.GetString(6);
                            model.PhoneNumber = reader.GetString(7);
                            model.Email = reader.GetString(8);

                            Console.WriteLine(model.FirstName + " " +
                                model.LastName + " " +
                                model.Address + " " +
                                model.City + " " +
                                model.State + " " +
                                model.Zip + " " +
                                model.PhoneNumber + " " +
                                model.Email + " "
                                );
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        public void GetAllContacsSortByName()
        {
            try
            {
                AddressBookModel model = new AddressBookModel();
                SqlConnection connection = new SqlConnection(connectionString);
                using (connection)
                {
                    string Query = @"Select * from AddressBook where City='Bhadrak' order by FirstName;";
                    SqlCommand cmd = new SqlCommand(Query, connection);
                    connection.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            model.ID = reader.GetInt32(0);
                            model.FirstName = reader.GetString(1);
                            model.LastName = reader.GetString(2);
                            model.Address = reader.GetString(3);
                            model.City = reader.GetString(4);
                            model.State = reader.GetString(5);
                            model.Zip = reader.GetString(6);
                            model.PhoneNumber = reader.GetString(7);
                            model.Email = reader.GetString(8);

                            Console.WriteLine(model.FirstName + " " +
                                model.LastName + " " +
                                model.Address + " " +
                                model.City + " " +
                                model.State + " " +
                                model.Zip + " " +
                                model.PhoneNumber + " " +
                                model.Email + " "
                                );
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        //Add AddressBookName and Type as Columns
        public void AddAddressBookNameAndType()
        {
            SqlConnection connection = new SqlConnection(connectionString);
            string query = @"alter table AddressBook add AddressBookName Varchar(50), AddressBookType Varchar(50);";
            SqlCommand command = new SqlCommand(query, connection);
            object res = command.ExecuteScalar();
            connection.Close();
        }
        //uc_10 get number of contacts by addressbook type first displayed contact details by addressbook type
        public void GetContactsByAddressBookType()
        {
            try
            {                
                SqlConnection connection = new SqlConnection(connectionString);
                using (connection)
                {
                    string Query = @"select * from addressbook where addressbooktype='friend';";
                    SqlCommand cmd = new SqlCommand(Query, connection);
                    connection.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            model.ID = reader.GetInt32(0);
                            model.FirstName = reader.GetString(1);
                            model.LastName = reader.GetString(2);
                            model.Address = reader.GetString(3);
                            model.City = reader.GetString(4);
                            model.State = reader.GetString(5);
                            model.Zip = reader.GetString(6);
                            model.PhoneNumber = reader.GetString(7);
                            model.Email = reader.GetString(8);
                            model.AddressBookName = reader.GetString(9);
                            model.AddressBookType = reader.GetString(10);

                            Console.WriteLine(model.FirstName + " " +
                                model.LastName + " " +
                                model.Address + " " +
                                model.City + " " +
                                model.State + " " +
                                model.Zip + " " +
                                model.PhoneNumber + " " +
                                model.Email + " " +
                                model.AddressBookName + " " +
                                model.AddressBookType + " "

                                );
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        //Count Records by AddressBookType
        public int CountOfEmployeeDetailsByType()
        {
            
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            string Query = @"Select count(*) from AddressBook where AddressBookType='Colleague';";
            SqlCommand command = new SqlCommand(Query, connection);
            object res = command.ExecuteScalar();
            connection.Close();
            int Count = (int)res;
            return Count;
        }
        //UC_11_Adding a Person to Both Friend and Family Type
        public void AddContactAsFriendAndFamily()
        {
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            string query = @"Insert into AddressBook Values ('Krishna','Dash','GDVL','NSTATION','Andhra Pradesh','520012','121413711821','Krishna@gmail.com','School','Friend'),
                            ('Shyam','Sunder','GDVL','NSTATION','Andhra Pradesh','520012','121413711821','sunder@gmail.com','Family','Sister');";
            SqlCommand command = new SqlCommand(query, connection);
            object res = command.ExecuteScalar();
            connection.Close();
        }
    }
}
