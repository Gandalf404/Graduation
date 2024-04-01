//using Graduation.Models;
//using Graduation.Models.Master;
using Graduation.Pages.WorkOrdersPages;
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

namespace Graduation.Pages
{
    public partial class AuthPage : Page
    {
        //private Authorisation _authorisation;
        public AuthPage()
        {
            InitializeComponent();
        }

        private void LogInButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //_authorisation = GraduationDB.graduationContext.Authorisations.FirstOrDefault(c => c.Login == LoginTextBox.Text && c.Password == PasswordBox.Password && c.Employee.PositionId == 4);
                //if (_authorisation != null)
                //{
                //    MessageBox.Show("Успешный вход", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
                      NavigationService.Navigate(new WorkOrdersPage());
                //}
                //else
                //{
                //    MessageBox.Show("Неверный логин или пароль", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                //}
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
