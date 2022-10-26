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
    /// Логика взаимодействия для TourAddEgitPage.xaml
    /// </summary>
    public partial class TourAddEgitPage : Page
    {
        private Tour_ _currentTour = new Tour_();
        public TourAddEgitPage(Tour_ selectedTour)
        {
            InitializeComponent();
            if (selectedTour != null)
                _currentTour = selectedTour;
            DataContext = _currentTour;
        }

        private void ButtonTourSave_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();

            if (_currentTour.tickestcount == null)
                errors.AppendLine("Введите количество билетов");
            if (string.IsNullOrEmpty(_currentTour.name))
                errors.AppendLine("Введите название");
            if (_currentTour.price == null)
                errors.AppendLine("Введите цену");
            //if (_currentTour.ItActual != true || _currentTour.ItActual != false)
            //    errors.AppendLine("Укажите актуальность");

            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }
            if (_currentTour.id == 0)
                ToursBaseEntities.GetContext().Tour_.Add(_currentTour);
            try
            {
                ToursBaseEntities.GetContext().SaveChanges();
                MessageBox.Show("Информация сохранена");
                Manager.MainFrame.GoBack();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }
    }
}
