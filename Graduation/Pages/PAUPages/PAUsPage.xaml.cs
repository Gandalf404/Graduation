using Graduation.Models;
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

namespace Graduation.Pages.PAUPages
{
    public partial class PAUsPage : Page
    {
        Pau pau;
        public PAUsPage()
        {
            InitializeComponent();
            DataContext = pau;
        }

        private void AddPAUButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {                
                pau.StoragePlaceId = ((StoragePlace)StoragePlaceNumberComboBox.SelectedItem).StoragePlaceId;
                pau.PauName = PAUNameTextBox.Text;
                pau.PauCount = Int32.Parse(PAUCountTextBox.Text);
                GraduationDB.graduationContext.Paus.Add(pau);
                MessageBox.Show("ДСЕ успешно добавлена.", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void AddBlueprintSymbolIcon_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
