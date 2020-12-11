using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Final
{
    public sealed class DBHandler
    {
        string ConString = ConfigurationManager.ConnectionStrings["ManagerConn"].ConnectionString;
        private DBHandler()
        {

        }
        static readonly DBHandler instance = new DBHandler();

        public static DBHandler Instance
        {
            get { return instance; }
        }
        List<Manager> manList = new List<Manager>();
        public List<Manager> View()
        {

            using (SqlConnection con = new SqlConnection(ConString))
            {               
                con.Open();

                SqlCommand cmd = new SqlCommand("Select * From Manage", con);
                using (SqlDataReader reader = cmd.ExecuteReader())
                {

                    while (reader.Read())
                    {
                        Manager man = new Manager();
                        if (Int32.TryParse(reader["ID"].ToString(), out int ID))
                        {
                            man.ID = ID;
                        }
                        man.Name = reader["Name"].ToString();
                        if (Int32.TryParse(reader["Phone"].ToString(), out int Phone))
                        {
                            man.Phone = Phone;
                        }
                        man.City = reader["City"].ToString();
                        if (Int32.TryParse(reader["Age"].ToString(), out int Age))
                        {
                            man.Age = Age;
                        }
                        man.Gender = reader["Gender"].ToString();
                        manList.Add(man);
                    }
                }
            }
            
            return manList;

        }


        public void AddContact(Manager contact)
        {

            using (SqlConnection conn = new SqlConnection(ConString))
            {
                using (SqlCommand cmd = new SqlCommand("Insert into Manage(Name, Phone, City, Age, Gender) Values(@name, @phone, @city, @age, @gender)", conn))
                {

                    cmd.Parameters.AddWithValue("@name", contact.Name);
                    cmd.Parameters.AddWithValue("@phone", Convert.ToInt64(contact.Phone));
                    cmd.Parameters.AddWithValue("@city", contact.City);
                    cmd.Parameters.AddWithValue("@age", Convert.ToInt32(contact.Age));
                    cmd.Parameters.AddWithValue("@gender", contact.Gender);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
        }


        public void EditContact(Manager contact)
        {
            SqlDataAdapter da = new SqlDataAdapter();

            using (SqlConnection con = new SqlConnection(ConString))
            {
                
                using (da.UpdateCommand = new SqlCommand("UPDATE Manage SET Name = @name, Phone = @phone, City = @city, Age = @age, Gender =@gender Where ID = @id", con))
                {
                    da.UpdateCommand.Parameters.AddWithValue("@id", SqlDbType.Int).Value = contact.ID;
                    da.UpdateCommand.Parameters.AddWithValue("@name", SqlDbType.NVarChar).Value = contact.Name;
                    da.UpdateCommand.Parameters.AddWithValue("@phone", SqlDbType.Int).Value = contact.Phone;
                    da.UpdateCommand.Parameters.AddWithValue("@city", SqlDbType.NVarChar).Value = contact.City;
                    da.UpdateCommand.Parameters.AddWithValue("@age", SqlDbType.Int).Value = contact.Age;
                    da.UpdateCommand.Parameters.AddWithValue("@gender", SqlDbType.NVarChar).Value = contact.Gender;

                    con.Open();
                    da.UpdateCommand.ExecuteNonQuery();
                    con.Close();

                    
                }
                
            }
        }


        public void ReadAllManagers()
        {
            List<Manager> manageList = new List<Manager>();

            using (SqlConnection con = new SqlConnection(ConString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("Select * From Manage");

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Manager contact = new Manager((int)reader["ID"], (string)reader["Name"], (int)reader["Phone"], (string)reader["City"], (int)reader["Age"], (string)reader["Gender"]);
                        manList.Add(contact);
                    }
                    con.Close();
                }
            }
        }



        public void RemoveContact(Manager contact)
        {
            int row = 0;
            SqlConnection con = new SqlConnection(ConString);
            SqlCommand cmd = new SqlCommand("delete from Manage where ID = @id", con);
            cmd.Parameters.AddWithValue("@id", contact.ID);
            try
            {
                con.Open();
                row = cmd.ExecuteNonQuery();
            }
            catch(SqlException ex)
            {
                Console.WriteLine("Error" + ex.ToString());
            }
            finally
            {
                con.Close();
            }
        }


        
    }
    
}
