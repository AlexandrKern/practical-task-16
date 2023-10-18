using System;
using System.Collections.Generic;
using System.Data;
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
using System.Xml.Linq;

namespace online_store
{
    /// <summary>
    /// Логика взаимодействия для AddProduct.xaml
    /// </summary>
    public partial class AddProduct : Window
    {
        SqlConnection sqlConnection;
        SqlCommand command;
        new string  Name { get; set; }
        string Surname { get; set; }
        string MiddleName { get; set; }
        string Email { get; set; }
        string Id { get; set; }
        string PhoneNumber { get; set; }
        public AddProduct()
        {
            InitializeComponent();
        }
        public AddProduct(string id, string name, string surname ,string middlename ,string phoneNamber, string email)
        {
            this.Id = id;
            this.Name = name;
            this.Surname = surname;
            this.MiddleName = middlename;
            this.PhoneNumber = phoneNamber;
            this.Email = email;
        }
        public AddProduct(DataRow row) : this()
        {
            ConnectionBD();
            string select = "SELECT * FROM TableInfoBuyer";
            command = new SqlCommand(select, sqlConnection);
            SqlDataReader reader = command.ExecuteReader();
            List<AddProduct> addProducts = new List<AddProduct>();
            while (reader.Read())
            {
                addProducts.Add( new AddProduct (reader["id"].ToString(),reader["Name"].ToString(),
                reader["Surname"].ToString(), reader["MiddleName"].ToString(), 
                reader["PhoneNumber"].ToString() ,reader["Email"].ToString()));
            }
                    okBtn.Click += delegate
                    {
                        int x = 0;
                        for (int i = 0; i < addProducts.Count; i++)
                        {
                            if (txtId.Text == addProducts[i].Id)
                            {
                                x = i; break;
                            }
                        }
                        try
                        {
                            if (int.Parse(txtId.Text) > addProducts.Count)
                            {
                                MessageBox.Show("Покупатель с таким id не найден");
                                return;
                            }
                            row["Id"] = txtId.Text;
                            row["Name"] = addProducts[x].Name;
                            row["Surname"] = addProducts[x].Surname;
                            row["Middlename"] = addProducts[x].MiddleName;
                            row["Email"] = addProducts[x].Email;
                            row["PhoneNumber"] = addProducts[x].PhoneNumber;
                            row["ProductCode"] = txtProductCode.Text;
                            row["ProductName"] = txtProductName.Text;
                            string insert = $"INSERT INTO TableWorkingWithTheBuyer " +
                                                   $"( id, ProductCode, ProductName)" +
                                                   $" VALUES (N'{txtId.Text}',N'{txtProductCode.Text}',N'{txtProductName.Text}')";
                            command = new SqlCommand(insert, sqlConnection);
                            command.ExecuteNonQuery();
                            this.DialogResult = !false;
                        }
                        catch (Exception)
                        {
                            MessageBox.Show("Проверите правильность заполнения");
                            return;
                        }  
                    };
        }
        private void ConnectionBD()
        {
            var sqlConnectionStringBuilder = new SqlConnectionStringBuilder()
            {
                DataSource = "(localdb)\\MSSqlLocalDb",
                InitialCatalog = "MSSQLLocalOnlineStore",
                IntegratedSecurity = true,
                Pooling = true,
            };
            sqlConnection = new SqlConnection(sqlConnectionStringBuilder.ConnectionString);
            sqlConnection.Open();
        }
    }
}
