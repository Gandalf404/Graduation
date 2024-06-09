using Graduation.Classes;
using Graduation.Models;
using Graduation.Models.Master;
using Graduation.Pages.EmployeesPages;
using Graduation.Pages.WorkOrdersPages;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace Graduation.Pages
{
    public partial class AuthPage : Page
    {
        private Employee _dbAdmin;
        private Employee _master;
        public AuthPage()
        {
            InitializeComponent();
        }

        private void LogInButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (String.IsNullOrWhiteSpace(LoginTextBox.Text) && String.IsNullOrWhiteSpace(PasswordBox.Password)) { throw new Exception(); }
                if (String.IsNullOrWhiteSpace(PasswordBox.Password)) { throw new Exception(); }
                if (String.IsNullOrWhiteSpace(LoginTextBox.Text)) { throw new Exception(); }
                _master = WorkOrdersDB.graduationContextMaster.Employees.FirstOrDefault(c => c.Login == Encryption.Encrypt(LoginTextBox.Text) && c.Password == Encryption.Encrypt(PasswordBox.Password) && c.PositionId == 4);
                _dbAdmin = WorkOrdersDB.graduationContextMaster.Employees.FirstOrDefault(c => c.Login == Encryption.Encrypt(LoginTextBox.Text) && c.Password == Encryption.Encrypt(PasswordBox.Password) && c.PositionId == 2);
                if (_dbAdmin != null)
                {
                    MessageBox.Show("Успешный вход", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
                    NavigationService.Navigate(new EmployeesPage());
                }
                else if (_master != null)
                {
                    MessageBox.Show("Успешный вход", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
                    NavigationService.Navigate(new WorkOrdersPage(_master));
                }
                else
                {
                    MessageBox.Show("Неверный логин или пароль", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch when (String.IsNullOrWhiteSpace(LoginTextBox.Text) && String.IsNullOrWhiteSpace(PasswordBox.Password))
            {
                MessageBox.Show("Введите логин и пароль", "Внимание", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            catch when (String.IsNullOrWhiteSpace(LoginTextBox.Text))
            {
                MessageBox.Show("Введите логин", "Внимание", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            catch when (String.IsNullOrWhiteSpace(PasswordBox.Password))
            {
                MessageBox.Show("Введите пароль", "Внимание", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
