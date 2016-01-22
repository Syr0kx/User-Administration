using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
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

namespace User_Administrator
{
    /// <summary>
    /// Interaktionslogik für adduser.xaml
    /// </summary>
    public partial class adduser : Window
    {
        public Boolean adding;
        public adduser(Boolean add)
        {
            InitializeComponent();
            adding = add;
            if (adding)
            {
                passwordBox.Visibility = Visibility.Visible;
                button_add.Content = "add";
                label_password.Visibility = Visibility.Visible;
            }
            else
            {
                passwordBox.Visibility = Visibility.Hidden;
                button_add.Content = "delete";
                label_password.Visibility = Visibility.Hidden;
            }
        }

        private void button_close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        static string GetMd5Hash(MD5 md5Hash, string input)
        {
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

            StringBuilder sBuilder = new StringBuilder();

            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }
            return sBuilder.ToString();
        }

        private void button_add_Click(object sender, RoutedEventArgs e)
        {
            if (adding)
            {
                if (textBox_username.Text.Equals("") || passwordBox.Password.Equals(""))
                {
                    MessageBox.Show("Benutzername oder Passwort dürfen nicht leer sein !");
                }
                else
                {
                    using (MD5 md5Hash = MD5.Create())
                    {
                        MySqlConnection con = new MySqlConnection();
                        MySqlDataAdapter ad = new MySqlDataAdapter();
                        MySqlCommand cmd = new MySqlCommand();
                        string str = "INSERT INTO logindaten(Benutzername,Passwort) VALUES('" + textBox_username.Text + "','" + GetMd5Hash(md5Hash, passwordBox.Password) + "');";
                        cmd.CommandText = str;
                        ad.SelectCommand = cmd;
                        con.ConnectionString = @"server=62.75.253.50;uid=WebBotIkariam;pwd=WebBotIkariam;database=WebBotIkariamAccounts;";
                        cmd.Connection = con;
                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();
                        ((MainWindow)Owner).test();
                        this.Close();
                    }
                }
            }
            else // delete from database
            {
                if (textBox_username.Text.Equals(""))
                {
                    MessageBox.Show("Benutzername darf nicht leer sein !");
                }
                else
                {
                    using (MD5 md5Hash = MD5.Create())
                    {
                        MySqlConnection con = new MySqlConnection();
                        MySqlDataAdapter ad = new MySqlDataAdapter();
                        MySqlCommand cmd = new MySqlCommand();
                        string str = "DELETE FROM logindaten Where Benutzername = '" + textBox_username.Text + "';";
                        cmd.CommandText = str;
                        ad.SelectCommand = cmd;
                        con.ConnectionString = @"server=62.75.253.50;uid=WebBotIkariam;pwd=WebBotIkariam;database=WebBotIkariamAccounts;";
                        cmd.Connection = con;
                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();
                        ((MainWindow)Owner).test();
                        this.Close();
                    }
                }
            }
        }
    }
}
