using Graduation.Models;
using Graduation.Models.Master;
using Graduation.Pages.EmployeesPages;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace Graduation.Pages.PausPages
{
    public partial class PausPage : Page
    {
        private List<Pau> _paus;
        private StoragePlace _selectedStoragePlace;
        private Pau _selectedPau;

        public PausPage()
        {            
            try
            {
                InitializeComponent();
                _paus = GraduationDB.graduationContext.Paus.OrderBy(c => c.PauId).ToList();
                PausListView.ItemsSource = _paus;
                StoragePlaceIdComboBox.Items.Add(new StoragePlace { StoragePlaceId = 0 });
                foreach (var item in GraduationDB.graduationContext.StoragePlaces)
                {
                    StoragePlaceIdComboBox.Items.Add(item);
                }
                PauNameComboBox.Items.Add(new Pau { PauName = "Все ДСЕ" });
                foreach (var item in GraduationDB.graduationContext.Paus.OrderBy(c => c.PauId))
                {
                    PauNameComboBox.Items.Add(item);
                }
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
                _paus = GraduationDB.graduationContext.Paus.OrderBy(c => c.PauId).ToList();
                PausListView.ItemsSource = _paus;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void LogOutButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                NavigationService.Navigate(new AuthPage());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void EmployeesViewItem_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                NavigationService.Navigate(new EmployeesPage());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void PausViewItem_Click(object sender, RoutedEventArgs e)
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

        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (String.IsNullOrWhiteSpace(SearchTextBox.Text))
                {
                    _paus = GraduationDB.graduationContext.Paus.OrderBy(c => c.PauId).ToList();
                    PausListView.ItemsSource = _paus;
                }
                else
                {
                    _paus = GraduationDB.graduationContext.Paus.Where(c => c.PauId.ToString().Contains(SearchTextBox.Text) || c.StoragePlaceId.ToString().Contains(SearchTextBox.Text)
                                                                        || c.PauName.Contains(SearchTextBox.Text) || c.PauCount.ToString().Contains(SearchTextBox.Text)).ToList();
                    PausListView.ItemsSource = _paus;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void PauCountAscRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                _paus = _paus.OrderBy(c => c.PauCount).ToList();
                PausListView.ItemsSource = _paus;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void PauCountDescRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                _paus = _paus.OrderByDescending(c => c.PauCount).ToList();
                PausListView.ItemsSource = _paus;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void StoragePlaceIdComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                _selectedStoragePlace = (StoragePlace)StoragePlaceIdComboBox.SelectedItem;
                if (StoragePlaceIdComboBox.SelectedIndex != 0)
                {
                    _paus =  GraduationDB.graduationContext.Paus.Where(c => c.StoragePlaceId == _selectedStoragePlace.StoragePlaceId).ToList();
                    PausListView.ItemsSource = _paus;
                }
                else
                {
                    PauCountAscRadioButton.IsChecked = false;
                    PauCountDescRadioButton.IsChecked = false;
                    _paus = GraduationDB.graduationContext.Paus.ToList();
                    PausListView.ItemsSource = _paus;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void PauNameComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                _selectedPau = (Pau)PauNameComboBox.SelectedItem;
                if (_selectedPau.PauName != "Все ДСЕ")
                {
                    _paus = GraduationDB.graduationContext.Paus.Where(c => c.PauName == _selectedPau.PauName).ToList();
                    PausListView.ItemsSource = _paus;
                }
                else
                {
                    PauCountAscRadioButton.IsChecked = false;
                    PauCountDescRadioButton.IsChecked = false;
                    _paus = GraduationDB.graduationContext.Paus.OrderBy(c => c.PauId).ToList();
                    PausListView.ItemsSource = _paus;
                }
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
                NavigationService.Navigate(new PauCreatePage());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void UpdatePauMenuItem_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                _selectedPau = (Pau)PausListView.SelectedItem;
                if (_selectedPau != null)
                {
                    NavigationService.Navigate(new PauCreatePage(_selectedPau));
                }
                else
                {
                    MessageBox.Show("Выберите ДСЕ для изменения", "Внимание", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
