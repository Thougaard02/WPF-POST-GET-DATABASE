using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfProgram
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        static string source = "Server=ZBC-E-NA-UBUY10; Database=Customer; Trusted_Connection=True;";
        public SqlConnection con = new SqlConnection(source);

        //Takes the class1.cs puts the table customers in a list
        ObservableCollection<Customer> customers = new ObservableCollection<Customer>();
        public MainWindow()
        {
            InitializeComponent();
            lv_mainView.ItemsSource = customers;
            GetQueryFromDB();

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            PostDataToDB();
            GetQueryFromDB();

        }

        void PostDataToDB()
        {

            string sqlPostQuery = $"INSERT INTO Customers(FirstName,LastName,HomeAddress,ZipCode)VALUES (@FirstName, @LastName, @HomeAddress, @Zipcode)";
            SqlCommand cmd = new SqlCommand(sqlPostQuery, con);
            con.Open();
            cmd.CommandText = sqlPostQuery;
            cmd.Parameters.AddWithValue("@FirstName", textBox2.Text);
            cmd.Parameters.AddWithValue("@LastName", textBox3.Text);
            cmd.Parameters.AddWithValue("@HomeAddress", textBox4.Text);
            cmd.Parameters.AddWithValue("@ZipCode", textBox5.Text);
            cmd.ExecuteNonQuery();
            con.Close();
        }

        void GetQueryFromDB()
        {
            customers.Clear();
            con.Open();
            string query = "SELECT * FROM Customers";           
            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                Customer customer = new Customer();

                customer.FirstName = (dr["FirstName"].ToString());
                customer.LastName = (dr["LastName"].ToString());
                customer.HomeAddress = (dr["HomeAddress"].ToString());
                customer.ZipCode = (dr["ZipCode"].ToString());

                customers.Add(customer);
            }
            con.Close();
        }
    }
}

//Ny knap som opretter bruger og en knap som updater siden/opdater sigen af sig selv.


























