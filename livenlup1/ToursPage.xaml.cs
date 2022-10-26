﻿using System;
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
    /// Логика взаимодействия для ToursPage.xaml
    /// </summary>
  

    public partial class ToursPage : Page
    {
        bool _isadmin;
        public ToursPage(bool isadmin)
        {
            InitializeComponent();
            _isadmin = isadmin;
            if (isadmin == true)
            {
                ButtonChangingView.Visibility = Visibility.Visible;
            }
            var allTypes = ToursBaseEntities.GetContext().Types.ToList();
            allTypes.Insert(0, new Type
            {
               name = "Все типы"
            });
            ComboType.ItemsSource = allTypes;
            CheckActual.IsChecked = true;
            ComboType.SelectedIndex = 0;

            UpdateTours();


        }

        private void UpdateTours()
        {
            var currentTours = ToursBaseEntities.GetContext().Tour_.ToList();

            if (ComboType.SelectedIndex > 0)
                currentTours = currentTours.Where(p => p.Types.Contains(ComboType.SelectedItem as Type)).ToList();

            currentTours = currentTours.Where(p => p.name.ToLower().Contains(TBoxSearch.Text.ToLower())).ToList();

            if (CheckActual.IsChecked.Value)
                currentTours = currentTours.Where(p => (bool)p.isactual).ToList();

            LViewTours.ItemsSource = currentTours.OrderBy(p => p.tickestcount).ToList();
        }

        private void ComboType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateTours();
        }

        private void CheckActual_Checked(object sender, RoutedEventArgs e)
        {
            UpdateTours();
        }

        private void TBoxSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateTours();
        }

        private void ButtonChangingView_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new ForManagerTourPage(_isadmin));
        }
    }
}