using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
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

namespace User_Administrator
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            test();
        }
        public void test()
        {
            try
            {
                MySqlConnection con = new MySqlConnection();
                MySqlDataAdapter ad = new MySqlDataAdapter();
                MySqlCommand cmd = new MySqlCommand();
                string str = "SELECT Benutzername, Passwort FROM logindaten";
                cmd.CommandText = str;
                ad.SelectCommand = cmd;
                con.ConnectionString = @"server=62.75.253.50;uid=WebBotIkariam;pwd=WebBotIkariam;database=WebBotIkariamAccounts;";
                cmd.Connection = con;
                DataTable dt = new DataTable();
                ad.Fill(dt);
                ListViewEmployeeDetails.DataContext = dt.DefaultView;
                con.Close();
            }
            catch(Exception e)
            {
                MessageBox.Show("Error 404:" + "\n" + e);
            }
        }

        private void button_close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void button_add_Click(object sender, RoutedEventArgs e)
        {
            Boolean adding = true;
            adduser add = new adduser(adding);
            add.Owner = this;
            add.Show();
        }

        private void button_delete_Click(object sender, RoutedEventArgs e)
        {
            Boolean adding = false;
            adduser add = new adduser(adding);
            add.Owner = this;
            add.Show();
        }
    }
}
