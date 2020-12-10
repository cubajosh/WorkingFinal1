using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Final
{
    public partial class AddContact : Window
    {
        string ConnString = ConfigurationManager.ConnectionStrings["ManagerConn"].ConnectionString;
        public AddContact()
        {
            InitializeComponent();
        }

        private void add(object sender, RoutedEventArgs e)
        {
            Manager contact = new Manager();
            contact.Name = Name.Text;
            contact.Phone = Convert.ToInt32(Phone.Text);
            contact.Gender = Gender.Text.ToString();
            contact.Age = Convert.ToInt32(Age.Text);
            contact.City = City.Text;
            DBHandler.Instance.AddContact(contact);

        }
    }
}
