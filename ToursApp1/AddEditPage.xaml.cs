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

namespace ToursApp1
{
    /// <summary>
    /// Логика взаимодействия для AddEditPage.xaml
    /// </summary>
    public partial class AddEditPage : Page
    {
        private Hotel1 _currnetHotel = new Hotel1();
        public AddEditPage(Hotel1 selectedHotel)
        {
            InitializeComponent();

            if (selectedHotel != null)
                _currnetHotel = selectedHotel;

            DataContext = _currnetHotel;
            ComboCountries.ItemsSource = upEntities.Context().Country.ToList();
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();
            if (string.IsNullOrWhiteSpace(_currnetHotel.Name))
                errors.AppendLine("Укажите название отеля");
            if (_currnetHotel.CountOfStars < 1 || _currnetHotel.CountOfStars > 5)
                errors.AppendLine("Количество звезд - число от 1 до 5");
            if (_currnetHotel.Country == null)
                errors.AppendLine("Выберите страну");

            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }

            if (_currnetHotel.id == 0)
                upEntities.Context().Hotel1.Add(_currnetHotel);


            try
            {
                upEntities.Context().SaveChanges();
                MessageBox.Show("Информация сохранена!");
                Manager.MainFrame.GoBack();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }
    }
}
