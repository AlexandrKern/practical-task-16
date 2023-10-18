using System;
using System.Collections.Generic;
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
    /// Логика взаимодействия для UserSelection.xaml
    /// </summary>
    public partial class UserSelection : Window
    {
        public UserSelection()
        {
            InitializeComponent();
        }
        private void ComboBox_Loaded(object sender, RoutedEventArgs e)
        {
            comboBox.SelectedIndex = 0;
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (comboBox.SelectedIndex == 0)
            {
                Authorization authorization = new Authorization();
                this.Close();
                authorization.Show();
            }
            if (comboBox.SelectedIndex == 1)
            {
                SellerWindow sellerWindow = new SellerWindow();
                this.Close();
                sellerWindow.Show();
            }
        }
    }
}
