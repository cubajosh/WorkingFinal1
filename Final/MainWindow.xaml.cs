using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
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
        ListHandler lh = ListHandler.Instance;

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
            List<Manager> contacts = DBHandler.Instance.View();
            lh.ContactList = new ObservableCollection<Manager>(contacts);
            list.ItemsSource = lh.ContactList;

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
            Manager selected = (Manager)list.SelectedItem;
            DBHandler.Instance.RemoveContact(selected);
            var found = ListHandler.Instance.ContactList.FirstOrDefault(x => x.ID == selected.ID);
            ListHandler.Instance.ContactList.Remove(selected);

            if (selected.ID != 0)
            {
                MessageBox.Show("Contact Has Been Removed", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }else if(selected.ID == 0)
            {
                MessageBox.Show("User Was Not Found", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        ExportCsv exp = new ExportCsv();
        private void importCSV_Click(object sender, RoutedEventArgs e)
        {
            var lineNum = 0;
            using(SqlConnection conn = new SqlConnection(ConString))
            {
                conn.Open();
                // here again it depends where you save the file
                using (StreamReader reader = new StreamReader(@"C:\ExporteData.csv"))
                {
                    while (!reader.EndOfStream)
                    {
                        var line = reader.ReadLine();

                        if(lineNum != 0) {
                        var values = line.Split(',');

                        string sql = "INSERT INTO Manage VALUES('" + values[0] + "','" + values[1] + "', '" + values[2] + "', '" + values[3] + "', '" + values[4] + ")";

                        SqlCommand cmd = new SqlCommand();
                        cmd.CommandText = sql;
                        cmd.CommandType = System.Data.CommandType.Text;
                        cmd.Connection = conn;
                        cmd.ExecuteNonQuery();
                        }
                        lineNum++;
                    }
                }
                conn.Close();
            }
        }


        public void exportCSV_Click(object sender, RoutedEventArgs e)
        {
                exp.GetCsv();
        }

    }
}
