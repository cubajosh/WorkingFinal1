using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Final
{
    class Reader
    {
        string ConString = ConfigurationManager.ConnectionStrings["ManagerConn"].ConnectionString;
        public List<Manager> ReadAllManagers()
        {
            List<Manager> manageList = new List<Manager>();

            using (SqlConnection con = new SqlConnection(ConString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("Select * From ManagerTable");

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Manager manager = new Manager();
                        manager.Name = reader["Name"].ToString();
                        if (Int32.TryParse(reader["Phone"].ToString(), out int Phone))
                        {
                            manager.Phone = Phone;
                        }
                        manager.City = reader["City"].ToString();
                        if (Int32.TryParse(reader["Age"].ToString(), out int Age))
                        {
                            manager.Age = Age;
                        }
                        manager.Gender = reader["Gender"].ToString();
                        manageList.Add(manager);
                    }
                    reader.Close();
                }
                return manageList;
            }
        }
    }
}

