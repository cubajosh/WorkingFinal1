using System;
using System.Collections.Generic;
using System.Configuration;
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
        public List<Manager> View()
        {
            List<Manager> manList = new List<Manager>();

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

            using (SqlConnection con = new SqlConnection(ConString))
            {
                con.Open();


                using (SqlCommand cmd = new SqlCommand("Update Manage Set Gender=@gender, Phone=@phone, City=@city, Age=@age Where Name=@name", con))
                {

                    cmd.UpdatePr("@name", contact.Name);
                    cmd.Parameters.AddWithValue("@phone", Convert.ToInt64(contact.Phone));
                    cmd.Parameters.AddWithValue("@city", contact.City);
                    cmd.Parameters.AddWithValue("@age", Convert.ToInt32(contact.Age));
                    cmd.Parameters.AddWithValue("@gender", contact.Gender);
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
    
}
