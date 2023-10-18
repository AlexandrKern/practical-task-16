using practical_task_16;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
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

namespace online_store
{
    /// <summary>
    /// Логика взаимодействия для Authorization.xaml
    /// </summary>
    public partial class Authorization : Window
    {
        SqlConnection sqlConnection;
        SqlCommand command;
        public Authorization()
        {
            InitializeComponent();
            ConnectionBD();
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
        /// <summary>
        /// Регистрация нового пользователя
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            
            СustomerВataEntry сustomer = new СustomerВataEntry();
            сustomer.Show();
            this.Close();
        }
        /// <summary>
        /// Проверка логина и пароля
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            string select = "SELECT * FROM TableLoginPassword";
            command = new SqlCommand(select, sqlConnection);
            SqlDataReader reader = command.ExecuteReader();
            List<ListAuthorization> authorizationList = new List<ListAuthorization>();
            while (reader.Read())
            {
                authorizationList.Add(new ListAuthorization(reader["id"].ToString(), reader["Login"].ToString(),
                    reader["Password"].ToString()));
            }
            for (int i = 0; i < authorizationList.Count; i++)
            {
                if (textBoxLogin.Text == authorizationList[i].login && textBoxPassword.Text == authorizationList[i].password) 
                {
                    Buyer_Window buyer = new Buyer_Window  (authorizationList[i].id);
                    buyer.Show();
                    this.Close();
                    return;
                }
            }
            MessageBox.Show("Не верный логин или пароль");
        }
       
    }
}
