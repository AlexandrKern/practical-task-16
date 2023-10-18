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
using static System.Net.Mime.MediaTypeNames;

namespace online_store
{
    /// <summary>
    /// Логика взаимодействия для СustomerВataEntry.xaml
    /// </summary>
    public partial class СustomerВataEntry : Window
    {
        SqlConnection sqlConnection;
        SqlCommand command;
        SqlDataReader reader;
        public СustomerВataEntry()
        {
            InitializeComponent();
            ConnectionBD();
        }
        private List<ListAuthorization> LoadList()
        {
            string select = "SELECT * FROM TableLoginPassword";
            command = new SqlCommand(select, sqlConnection);
            reader = command.ExecuteReader();
            List<ListAuthorization> authorizationList = new List<ListAuthorization>();
            while (reader.Read())
            {
                authorizationList.Add(new ListAuthorization(reader["id"].ToString(), reader["Login"].ToString(),
                    reader["Password"].ToString()));
            }
            return authorizationList;
        }
        /// <summary>
        /// Подключение к базе данных
        /// </summary>
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
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            List<ListAuthorization> authorizationList = LoadList();
            if (string.IsNullOrEmpty(textBoxLogin.Text)||
                string.IsNullOrEmpty(textBoxPassword.Text) ||
                string.IsNullOrEmpty(txtName.Text) ||
                string.IsNullOrEmpty(txtMiddleName.Text) ||
                string.IsNullOrEmpty(txtSurName.Text) ||
                string.IsNullOrEmpty(txtEmail.Text))
            {
                MessageBox.Show("Заполните все поля");
                return;
            }
            for (int i = 0; i < authorizationList.Count; i++)
            {
                if (textBoxLogin.Text == authorizationList[i].login)
                {
                    MessageBox.Show("Пользователь с таким логином уже есть!");
                    return;
                }
            }
            string insert = $"INSERT INTO TableLoginPassword ( Login, Password)" +
                $" VALUES (N'{textBoxLogin.Text}',N'{textBoxPassword.Text}')";
            command = new SqlCommand(insert, sqlConnection);
            reader.Close();
            command.ExecuteNonQuery();
            string insert1 = $"INSERT INTO TableInfoBuyer (Surname,Name,MiddleName,PhoneNumber,Email)" +
               $" VALUES (N'{txtSurName.Text}',N'{txtName.Text}',N'{txtMiddleName.Text}',N'{txtPhoneNumber.Text}',N'{txtEmail.Text}')";
            command = new SqlCommand(insert1, sqlConnection);
            try
            {
                command.ExecuteNonQuery();
            }
            catch (Exception)
            {
                MessageBox.Show("Проверите правильность заполнения");
                return;
            }
            List<ListAuthorization> authorizationList1 = LoadList();
            for (int i = 0; i < authorizationList1.Count; i++)
            {
                if (textBoxLogin.Text == authorizationList1[i].login && textBoxPassword.Text == authorizationList1[i].password)
                {
                    Buyer_Window buyer = new Buyer_Window(authorizationList1[i].id);
                    buyer.Show();
                    this.Close();
                    return;
                }
            }
        }
    }
}
