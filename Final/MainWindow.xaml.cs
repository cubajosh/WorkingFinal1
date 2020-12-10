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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Final
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string ConString = ConfigurationManager.ConnectionStrings["ManagerConn"].ConnectionString;
        public MainWindow()
        {
            InitializeComponent();
            fill_listbox();
        }
        public void fill_listbox()
        {

        }
        public void view(object sender, EventArgs e)
        {
            List<Manager> manage = DBHandler.Instance.View();
            list.ItemsSource = manage;

        }
        public void addContact(object sender, RoutedEventArgs e)
        {
            AddContact contact = new AddContact();
            contact.Show();
        }
        public void editContact(object sender, RoutedEventArgs e)
        {
            Manager selected = (Manager)list.SelectedItem;

            EditContact edit = new EditContact(selected);
            edit.Show();
        }

        private void infoBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            

        }

        private void exitProgram_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void removeContact_Click(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
