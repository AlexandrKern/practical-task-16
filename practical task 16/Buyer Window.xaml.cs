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
    /// Логика взаимодействия для Buyer_Window.xaml
    /// </summary>
    public partial class Buyer_Window : Window
    {
        public Buyer_Window(string i)
        {
            int id = int.Parse(i);
            InitializeComponent();
            var sqlConnectionStringBuilder = new SqlConnectionStringBuilder()
            {
                DataSource = "(localdb)\\MSSqlLocalDb",
                InitialCatalog = "MSSQLLocalOnlineStore",
                IntegratedSecurity = true,
                Pooling = true,
            };
            SqlConnection sqlConnection = new SqlConnection(sqlConnectionStringBuilder.ConnectionString);
            DataTable table = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter();
            var sql = $@"select 
TableWorkingWithTheBuyer.ProductCode,
TableWorkingWithTheBuyer.ProductName
from TableWorkingWithTheBuyer
where TableWorkingWithTheBuyer.Id = { id } ";
            adapter.SelectCommand = new SqlCommand(sql, sqlConnection);
            adapter.Fill(table);
            gridView.DataContext = table.DefaultView;
            Title = $"Купленные товары(ваш id {id})";
        }
    }
}
