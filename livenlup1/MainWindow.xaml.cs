using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace livenlup1
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        
        public MainWindow()
        {
            InitializeComponent();
            MainFrame.Navigate(new LoginPage());
            Manager.MainFrame = MainFrame;
           // ImportTours();
        }

        private void ImportTours() {
            var fileData = File.ReadAllLines(@"C:\Users\Gookich\Desktop\import\Туры.txt");
            var images = Directory.GetFiles(@"C:\Users\Gookich\Desktop\import\Туры фото");

            foreach (var line in fileData)
            {
                var data = line.Split('\t');

                var tempTour = new Tour_
                {
                    name = data[0].Replace("\"", ""),
                    tickestcount = int.Parse(data[2]),
                    price = decimal.Parse(data[3]),
                    isactual = (data[4] == "0") ? false : true
                };

                foreach (var tourType in data[5].Replace("\" ", ""          ).Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries))
                {
                    var currentType = ToursBaseEntities.GetContext().Types.ToList().FirstOrDefault(p => p.name == tourType);
                    if (currentType != null)
                        tempTour.Types.Add(currentType);
                }

                try
                {
                    tempTour.imagepreview = File.ReadAllBytes(images.FirstOrDefault(p => p.Contains(tempTour.name)));
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

                ToursBaseEntities.GetContext().Tour_.Add(tempTour);
                ToursBaseEntities.GetContext().SaveChanges();
            }














        }








        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.GoBack();
        }

        private void MainFrame_ContentRendered(object sender, EventArgs e)
        {
            if (MainFrame.CanGoBack)
            {
                Button_Login.Visibility = Visibility.Hidden;
                BtnBack.Visibility = Visibility.Visible;
                Button_Hotel.Visibility = Visibility.Visible;

            }
            else
            {
                Button_Login.Visibility = Visibility.Visible;
                BtnBack.Visibility = Visibility.Hidden;
                Button_Hotel.Visibility = Visibility.Hidden;
            }
        }

        private void Button_Login_Click(object sender, RoutedEventArgs e)
        {
            LoginPage.TypeID = 0;
            Manager.MainFrame.Navigate(new ToursPage(false));
        }

        private void MainFrame_Navigated(object sender, System.Windows.Navigation.NavigationEventArgs e)
        {

        }

        private void Button_Hotel_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new HotelsPage());
        }
    }
}
