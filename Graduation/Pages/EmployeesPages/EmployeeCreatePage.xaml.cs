using Graduation.Classes;
using Graduation.Models;
using Graduation.Models.Admin;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Navigation;

namespace Graduation.Pages.EmployeesPages
{
    public partial class EmployeeCreatePage : Page
    {
        private Employee _employee;
        private bool _isCreating;

        public EmployeeCreatePage()
        {
            try
            {
                InitializeComponent();
                _employee = new Employee();
                _isCreating = true;
                foreach (var item in WorkOrdersDB.graduationContextAdmin.Areas)
                {
                    AreaIdComboBox.Items.Add(item);
                }
                foreach (var item in WorkOrdersDB.graduationContextAdmin.Positions)
                {
                    PositionNameComboBox.Items.Add(item);
                }
                foreach (var item in WorkOrdersDB.graduationContextAdmin.Classes)
                {
                    ClassComboBox.Items.Add(item);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        //UNDONE: Сделать пустые поля логина и пароля.
        public EmployeeCreatePage(Employee employee)
        {
            try
            {
                InitializeComponent();
                _employee = employee;
                _isCreating = false;
                foreach (var item in WorkOrdersDB.graduationContextAdmin.Areas)
                {
                    AreaIdComboBox.Items.Add(item);
                }
                foreach (var item in WorkOrdersDB.graduationContextAdmin.Positions)
                {
                    PositionNameComboBox.Items.Add(item);
                }
                foreach (var item in WorkOrdersDB.graduationContextAdmin.Classes)
                {
                    ClassComboBox.Items.Add(item);
                }
                EmployeeLoginTextBox.Text = "";
                EmployeePasswordBox.Password = "";
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
                DataContext = _employee;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        private void EmployeeIdTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
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

        private void EmployeeSurnameTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
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

        private void EmployeeNameTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
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

        private void EmployeePatronymicTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
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

        //UNDONE: При роли не мастер или админ БД чтобы исчезали поля ввода логина и пароля.
        private void EmployeeCreateButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (_isCreating)
                {
                    _employee.AreaId = ((Area)AreaIdComboBox.SelectedItem).AreaId;
                    _employee.PositionId = ((Position)PositionNameComboBox.SelectedItem).PositionId;
                    _employee.ClassId = ((Class)ClassComboBox.SelectedItem).ClassId;

                    WorkOrdersDB.graduationContextAdmin.Add(_employee);
                    WorkOrdersDB.graduationContextAdmin.SaveChanges();
                    MessageBox.Show("Сотрудник успешно зарегистрирован", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    _employee.AreaId = ((Area)AreaIdComboBox.SelectedItem).AreaId;
                    _employee.PositionId = ((Position)PositionNameComboBox.SelectedItem).PositionId;
                    _employee.ClassId = ((Class)ClassComboBox.SelectedItem).ClassId;

                    WorkOrdersDB.graduationContextAdmin.SaveChanges();
                    MessageBox.Show("Данные о сотруднике успешно изменены", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch when (String.IsNullOrWhiteSpace(EmployeeIdTextBox.Text) || String.IsNullOrWhiteSpace(EmployeeSurnameTextBox.Text) || String.IsNullOrWhiteSpace(EmployeeNameTextBox.Text)
            || String.IsNullOrWhiteSpace(EmployeePatronymicTextBox.Text) || AreaIdComboBox.SelectedItem == null || PositionNameComboBox.SelectedItem == null
            || ClassComboBox.SelectedItem == null || String.IsNullOrWhiteSpace(EmployeeLoginTextBox.Text) || String.IsNullOrWhiteSpace(EmployeePasswordBox.Password))
            {
                MessageBox.Show("Необходимо заполнить все поля", "Внимание", MessageBoxButton.OK, MessageBoxImage.Warning);
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
                NavigationService.Navigate(new EmployeesPage());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void AreaIdComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void PositionNameComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
