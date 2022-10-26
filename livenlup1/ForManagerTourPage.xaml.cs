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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace livenlup1
{
    /// <summary>
    /// Логика взаимодействия для ForManagerTourPage.xaml
    /// </summary>
    public partial class ForManagerTourPage : Page
    {
        public ForManagerTourPage(bool isadmin)
        {

            InitializeComponent();
            if (isadmin == true)
            {
                buttonAdd.Visibility = Visibility.Visible;
                buttonDelete.Visibility = Visibility.Visible;
            }
            dataGridTours.ItemsSource = ToursBaseEntities.GetContext().Tour_.ToList();
        }

        private void TourPage_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (Visibility == Visibility.Visible)
            {
                ToursBaseEntities.GetContext().ChangeTracker.Entries().ToList().ForEach(p => p.Reload());
                dataGridTours.ItemsSource = ToursBaseEntities.GetContext().Tour_.ToList().ToList();
            }
        }

        private void buttonAdd_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new TourAddEgitPage(null));
        }

        private void buttonDelete_Click(object sender, RoutedEventArgs e)
        {
            var TourForRemoving = dataGridTours.SelectedItems.Cast<Tour_>().ToList();

            if (MessageBox.Show($"Вы точно хотите удалить следующие {TourForRemoving.Count()} элементов?", "Внимание",
               MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    ToursBaseEntities.GetContext().Tour_.RemoveRange(TourForRemoving);
                    ToursBaseEntities.GetContext().SaveChanges();
                    MessageBox.Show("Данные удалены!");

                    dataGridTours.ItemsSource = ToursBaseEntities.GetContext().Tour_.ToList();

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }
        }

        private void buttonEdit_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new TourAddEgitPage((sender as Button).DataContext as Tour_));
        }
    }
}
