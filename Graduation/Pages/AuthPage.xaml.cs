using Graduation.Models;
using Graduation.Pages.AcceptNotePages;
using Graduation.Pages.WorkOrderPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data.Common;
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
        Employee employee;

        public AuthPage()
        {
            InitializeComponent();
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                employee = GraduationDB.graduationContext.Employees
                    .FirstOrDefault(c => c.EmployeeLogin == LoginTextBox.Text && c.EmployeePassword == PasswordBox.Password);
                if (employee != null)
                {
                    switch (employee.PositionId)
                    {
                        case 4:
                            NavigationService.Navigate(new WorkOrdersPage());
                            break;
                        default:
                            NavigationService.Navigate(new AcceptNotesPage());
                            break;
                    }
                }
                else
                {
                    throw new Exception("Неверный логин или пароль.");
                }
            }
            catch when (String.IsNullOrWhiteSpace(LoginTextBox.Text) && String.IsNullOrWhiteSpace(PasswordBox.Password))
            {
                MessageBox.Show("Введите логин и пароль.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch when (String.IsNullOrWhiteSpace(LoginTextBox.Text)) 
            {
                MessageBox.Show("Введите логин.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning); 
            }
            catch when (String.IsNullOrWhiteSpace(PasswordBox.Password))
            {
                MessageBox.Show("Введите пароль.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            catch (Exception ex) 
            { 
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error); 
            }
        }
    }
}
