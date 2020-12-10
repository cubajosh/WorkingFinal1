using System;
using System.Collections.Generic;
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
using System.Data;
using System.Configuration;

namespace Final
{
    /// <summary>
    /// Interaction logic for EditContact.xaml
    /// </summary>
    public partial class EditContact : Window
    {
        string ConString = ConfigurationManager.ConnectionStrings["ManagerConn"].ConnectionString;
        public EditContact(Manager man)
        {
            InitializeComponent();
            Name.Text = man.Name;
            Phone.Text = man.Phone.ToString();
            City.Text = man.City;
            Age.Text = man.Age.ToString();
            Gender.Text = man.Gender;
        }

        private void edit(object sender, RoutedEventArgs e)
        {
            Manager contact = new Manager();
            contact.Name = Name.Text;
            contact.Phone = Convert.ToInt32(Phone.Text);
            contact.Gender = Gender.Text.ToString();
            contact.Age = Convert.ToInt32(Age.Text);
            contact.City = City.Text;
            DBHandler.Instance.EditContact(contact);




        }

    }
}
