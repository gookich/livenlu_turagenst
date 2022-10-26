using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Core.Objects;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.InteropServices;
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

namespace livenlup1
{
    /// <summary>
    /// Логика взаимодействия для LoginPage.xaml
    /// </summary>
    public partial class LoginPage : Page
    {
        ToursBaseEntities db = new ToursBaseEntities();
        private user _users = new user();
        user authuser = null;
        user adminunser = null;
        private string TypeUser;


        public static int TypeID { get; set; }
        public LoginPage() {
            InitializeComponent();
            DataContext = _users;
            HotelPage = new HotelsPage();

        }
        HotelsPage HotelPage = null;







        private void Button_Auth_Click(object sender, RoutedEventArgs e)
        {
            var authuser = db.users.FirstOrDefault(x => x.login == TextBox_Login.Text && x.password == TextBox_Password.Text);
            var adminuser = db.users.FirstOrDefault(x => x.login == TextBox_Login.Text && x.type_user == "Admin");
            if (authuser != null)
            {

                if (adminuser != null)
                {
                    Manager.MainFrame.Navigate(new ToursPage(true));
                    admin.isadmin = true;
                }
                else
                {
                    MessageBox.Show("Вы вошли как рофлорыба");
                    Manager.MainFrame.Navigate(new ToursPage(false));
                    admin.isadmin = false;
                }
            }
            else
            {
                MessageBox.Show("Ошибка!!!\nВведите данные");
            }
        }

        private void TextBox_Login_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
