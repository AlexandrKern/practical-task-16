using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
using System.Windows.Shapes;

namespace online_store
{
    /// <summary>
    /// Логика взаимодействия для SellerWindow.xaml
    /// </summary>
    public partial class SellerWindow : Window
    {
        DataTable table;
        SqlDataAdapter adapter ;
        public SellerWindow()
        {
            InitializeComponent();
            LoadDataGrid();
        }
        private void LoadDataGrid()
        {
            var sqlConnectionStringBuilder = new SqlConnectionStringBuilder()
            {
                DataSource = "(localdb)\\MSSqlLocalDb",
                InitialCatalog = "MSSQLLocalOnlineStore",
                IntegratedSecurity = true,
                Pooling = true,
            };
            SqlConnection sqlConnection = new SqlConnection(sqlConnectionStringBuilder.ConnectionString);
            table = new DataTable();
            adapter = new SqlDataAdapter();
            var sql = @"UPDATE TableWorkingWithTheBuyer,TableInfoBuyer SET
                           ProductCode = @ProductCode
                           ProductName = @ProductName
                           id = @id";
            adapter.UpdateCommand = new SqlCommand(sql, sqlConnection);
            adapter.UpdateCommand.Parameters.Add("@id", SqlDbType.Int, 0, "id");
            adapter.UpdateCommand.Parameters.Add("@ProductCode", SqlDbType.NVarChar, 20, "ProductCode");
            adapter.UpdateCommand.Parameters.Add("@ProductName", SqlDbType.NVarChar, 20, "ProductName");
            sql = @"UPDATE TableInfoBuyer SET
                           Surname = @Surname
                           Name = @Name
                           MiddleName = @MiddleName
                           PhoneNumber = @PhoneNumber
                           Email = @Email";
            adapter.UpdateCommand = new SqlCommand(sql, sqlConnection);
            adapter.UpdateCommand.Parameters.Add("@Surname", SqlDbType.NVarChar, 20, "Surname");
            adapter.UpdateCommand.Parameters.Add("@Name", SqlDbType.NVarChar, 20, "Name");
            adapter.UpdateCommand.Parameters.Add("@MiddleName", SqlDbType.NVarChar, 20, "MiddleName");
            adapter.UpdateCommand.Parameters.Add("@PhoneNumber", SqlDbType.BigInt, 4, "PhoneNumber");
            adapter.UpdateCommand.Parameters.Add("@Email", SqlDbType.NVarChar, 20, "Email");
            sql = @"INSERT INTO TableWorkingWithTheBuyer(id,ProductCode,ProductName)
                                        VALUES(@id,@ProductCode,@ProductName)";
            adapter.InsertCommand = new SqlCommand(sql, sqlConnection);
            adapter.InsertCommand.Parameters.Add("@id", SqlDbType.BigInt, 4, "id");
            adapter.InsertCommand.Parameters.Add("@ProductCode", SqlDbType.NVarChar, 20, "ProductCode");
            adapter.InsertCommand.Parameters.Add("@ProductName", SqlDbType.NVarChar, 20, "ProductName");
            sql = @"INSERT INTO TableInfoBuyer (Surname,Name,MiddleName,PhoneNumber,Email)
                                        VALUES(@Surname,@Name,@MiddleName,@PhoneNumber,@Email)";
            adapter.InsertCommand = new SqlCommand(sql, sqlConnection);
            adapter.InsertCommand.Parameters.Add("@Surname", SqlDbType.NVarChar, 20, "Surname");
            adapter.InsertCommand.Parameters.Add("@Name", SqlDbType.NVarChar, 20, "Name");
            adapter.InsertCommand.Parameters.Add("@MiddleName", SqlDbType.NVarChar, 20, "MiddleName");
            adapter.InsertCommand.Parameters.Add("@PhoneNumber", SqlDbType.BigInt, 4, "PhoneNumber");
            adapter.InsertCommand.Parameters.Add("@Email", SqlDbType.NVarChar, 20, "Email");
            sql = @"select
 TableInfoBuyer.id,
 TableInfoBuyer.Surname,
 TableInfoBuyer.[Name],
 TableInfoBuyer.MiddleName,
 TableInfoBuyer.PhoneNumber,
 TableInfoBuyer.Email,
 TableWorkingWithTheBuyer.ProductCode,
 TableWorkingWithTheBuyer.ProductName
 from TableInfoBuyer,TableWorkingWithTheBuyer
 where TableInfoBuyer.id = TableWorkingWithTheBuyer.Id
 order by TableInfoBuyer.id";
            adapter.SelectCommand = new SqlCommand(sql, sqlConnection);
            adapter.Fill(table);
            gridView.DataContext = table.DefaultView;
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DataRow r = table.NewRow();
            AddProduct add = new AddProduct(r);
            add.ShowDialog();
            if (add.DialogResult.Value) table.Rows.Add(r);

        }
    }
}
