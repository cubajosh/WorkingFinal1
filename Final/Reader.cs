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
        public void ReadAllManagers()
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
                        Manager contact = new Manager((int)reader["ID"], (string)reader["Name"], (int)reader["Phone"], (string)reader["City"], (int)reader["Age"], (string)reader["Gender"]);
                        manageList.Add(contact);
                    }
                    con.Close();
                }
            }
        }
    }
}

