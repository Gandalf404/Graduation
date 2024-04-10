using Graduation.Models;
using Graduation.Models.Master;
using Microsoft.EntityFrameworkCore;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace Graduation.Pages.EmployeesPages
{
    public partial class EmployeesPage : Page
    {
        private List<Employee> _employees;
        private Position _selectedPosition;
        private Employee _selectedEmployee;

        public EmployeesPage()
        {
            try
            {
                InitializeComponent();
                _employees = GraduationDB.graduationContext.Employees.Include(c => c.Position).Include(c => c.Authorisation).OrderBy(c => c.EmployeeId).ToList();
                EmployeesDataGrid.ItemsSource = _employees;
                PositionNameComboBox.Items.Add(new Position { PositionName = "Все должности" });
                foreach (var item in GraduationDB.graduationContext.Positions)
                {
                    PositionNameComboBox.Items.Add(item);
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
                _employees = GraduationDB.graduationContext.Employees.Include(c => c.Position).Include(c => c.Authorisation).OrderBy(c => c.EmployeeId).ToList();
                EmployeesDataGrid.ItemsSource = _employees;
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

        //TODO: Изменить навигацию мб.
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

        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (String.IsNullOrWhiteSpace(SearchTextBox.Text))
                {
                    _employees = GraduationDB.graduationContext.Employees.Include(c => c.Position).ToList();
                    EmployeesDataGrid.ItemsSource = _employees;
                }
                else
                {
                    _employees = GraduationDB.graduationContext.Employees.Where(c => c.EmployeeId.ToString().Contains(SearchTextBox.Text)
                                                                                || c.EmployeeSurname.Contains(SearchTextBox.Text)
                                                                                || c.EmployeeName.Contains(SearchTextBox.Text)
                                                                                || c.EmployeePatronymic.Contains(SearchTextBox.Text)
                                                                                || c.AreaId.ToString().Contains(SearchTextBox.Text)
                                                                                || c.Position.PositionName.Contains(SearchTextBox.Text)
                                                                                || c.ClassId.ToString().Contains(SearchTextBox.Text)).ToList();
                    EmployeesDataGrid.ItemsSource = _employees;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void AreaIdAscRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                _employees = _employees.OrderBy(c => c.AreaId).ToList();
                EmployeesDataGrid.ItemsSource = _employees;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void AreaIdDescRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                _employees = _employees.OrderByDescending(c => c.AreaId).ToList();
                EmployeesDataGrid.ItemsSource = _employees;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ClassIdAscRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                _employees = _employees.OrderBy(c => c.ClassId).ToList();
                EmployeesDataGrid.ItemsSource = _employees;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ClassIdDescRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                _employees = _employees.OrderByDescending(c => c.ClassId).ToList();
                EmployeesDataGrid.ItemsSource = _employees;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void PositionNameComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                _selectedPosition = (Position)PositionNameComboBox.SelectedItem;
                if (_selectedPosition.PositionName != "Все должности")
                {
                    _employees = GraduationDB.graduationContext.Employees.Where(c => c.Position.PositionName == _selectedPosition.PositionName).ToList();
                    EmployeesDataGrid.ItemsSource= _employees;
                }
                else
                {
                    _employees = GraduationDB.graduationContext.Employees.Include(c => c.Position).ToList();
                    EmployeesDataGrid.ItemsSource = _employees;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void EmployeeCreateButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                NavigationService.Navigate(new EmployeeCreatePage());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void UpdateEmployeeMenuItem_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                _selectedEmployee = (Employee)EmployeesDataGrid.SelectedItem;
                if (_selectedEmployee != null)
                {
                    NavigationService.Navigate(new EmployeeCreatePage(_selectedEmployee));
                }
                else
                {
                    MessageBox.Show("Выберите сотрдуника для изменения", "Внимание", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
