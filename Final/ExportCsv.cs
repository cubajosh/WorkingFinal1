using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Final
{
    class ExportCsv
    {

        string ConString = ConfigurationManager.ConnectionStrings["ManagerConn"].ConnectionString;
        


        public string GetCsv()
        {
            using(SqlConnection conn = new SqlConnection(ConString))
            {
                conn.Open();
                return CreateCSV(new SqlCommand("select * from Manage", conn).ExecuteReader());
            }
        }
        


        public string CreateCSV(SqlDataReader reader)
        {

            // might needs to change file location
            string file = @"C:\ExporteData.csv";
            List<string> lines = new List<string>();

            string headerLine = "";
            if (reader.Read())
            {
                string[] column = new string[reader.FieldCount];
                
                for(int i=0; i<reader.FieldCount; i++)
                {
                    column[i] = reader.GetName(i);
                }
                headerLine = string.Join(",", column);
                lines.Add(headerLine);
            }

            // date
            while (reader.Read())
            {
                object[] values = new object[reader.FieldCount];
                reader.GetValues(values);
                lines.Add(string.Join(",", values));
            }

            //craete file
            System.IO.File.WriteAllLines(file, lines);

            return file;
        }

    }

}
