using Graduation.Models;
using Graduation.Models.Admin;
using Microsoft.Win32;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Navigation;

namespace Graduation.Pages.PausPages
{
    public partial class PauCreatePage : Page
    {
        private Pau _pau;
        private bool _isCreating;
        private OpenFileDialog _openFileDialog;

        public PauCreatePage()
        {
            try
            {
                InitializeComponent();
                _pau = new Pau();
                _isCreating = true;
                foreach (var item in WorkOrdersDB.graduationContextAdmin.StoragePlaces)
                {
                    StoragePlaceIdComboBox.Items.Add(item);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public PauCreatePage(Pau pau)
        {
            try
            {
                InitializeComponent();
                _pau = pau;
                _isCreating = false;
                foreach (var item in WorkOrdersDB.graduationContextAdmin.StoragePlaces)
                {
                    StoragePlaceIdComboBox.Items.Add(item);
                }
                PauIdTextBox.IsEnabled = false;
                PauBlueprintImage.DataContext = _pau.PauBlueprint;
                AddPauBlueprintImageButton.Content = "Изменить чертёж";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                DataContext = _pau;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void AddPauBlueprintImageButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                _openFileDialog = new OpenFileDialog();
                if (_openFileDialog.ShowDialog() == true)
                {
                    _pau.PauBlueprint = File.ReadAllBytes(_openFileDialog.FileName);
                    PauBlueprintImage.DataContext = File.ReadAllBytes(_openFileDialog.FileName);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void PauIdTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            try
            {
                if (Char.IsDigit(e.Text, 0)) return;
                e.Handled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void PauNameTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            try
            {
                if (Char.IsLetter(e.Text, 0)) return;
                e.Handled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void PauCountTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            try
            {
                if (Char.IsDigit(e.Text, 0)) return;
                e.Handled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void PauCreateButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (_isCreating)
                {
                    _pau.StoragePlaceId = ((StoragePlace)StoragePlaceIdComboBox.SelectedItem).StoragePlaceId;
                    WorkOrdersDB.graduationContextAdmin.Add(_pau);
                    WorkOrdersDB.graduationContextAdmin.SaveChanges();
                    MessageBox.Show("ДСЕ успешно добавлена", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    _pau.StoragePlaceId = ((StoragePlace)StoragePlaceIdComboBox.SelectedItem).StoragePlaceId;
                    WorkOrdersDB.graduationContextAdmin.SaveChanges();
                    MessageBox.Show("ДСЕ успешно изменена", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                NavigationService.Navigate(new PausPage());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
