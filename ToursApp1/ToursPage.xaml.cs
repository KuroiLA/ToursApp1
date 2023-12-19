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
using ToursApp1;

namespace ToursApp1
{
    /// <summary>
    /// Логика взаимодействия для ToursPage.xaml
    /// </summary>
    public partial class ToursPage : Page
    {
        public ToursPage()
        {
            InitializeComponent();

            var allTypes = upEntities.Context().type.ToList();
            allTypes.Insert(0, new type
            {
                Name = "Все типы"
            });
            ComboType.ItemsSource = allTypes;
            CheckActual.IsChecked = true;
            ComboType.SelectedIndex = 0;
            var currentTours = upEntities.Context().Tour.ToList();
            LViewTours.ItemsSource = currentTours;
            UpdateTours();
            
           
            
        }
        private void UpdateTours()
        {
            var currentTours = upEntities.Context().Tour.ToList();
            if (ComboType.SelectedIndex > 0)
                currentTours = currentTours.Where(p => p.type.Contains(ComboType.SelectedItem as type)).ToList();
            currentTours = currentTours.Where(p => p.Name.ToLower().Contains(TboxSearch.Text.ToLower())).ToList();
            if
                (CheckActual.IsChecked.Value)
                currentTours = currentTours.Where(p => p.IsActual).ToList();
            LViewTours.ItemsSource = currentTours.OrderBy(p => p.TicketsCount).ToList();
            
        }

        private void TboxSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateTours();
        }

        private void ComboType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateTours();
        }

        private void CheckActual_Checked(object sender, RoutedEventArgs e)
        {
            UpdateTours();
        }

        private void CheckActual_Unchecked(object sender, RoutedEventArgs e)
        {
            UpdateTours();
        }
    }
}
