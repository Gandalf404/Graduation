using Graduation.Models;
using Graduation.Models.Master;
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
                foreach (var item in GraduationDB.graduationContext.Areas)
                {
                    AreaIdComboBox.Items.Add(item);
                }
                foreach (var item in GraduationDB.graduationContext.Positions)
                {
                    PositionNameComboBox.Items.Add(item);
                }
                foreach (var item in GraduationDB.graduationContext.Classes)
                {
                    ClassComboBox.Items.Add(item);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public EmployeeCreatePage(Employee employee)
        {
            try
            {
                InitializeComponent();
                _employee = employee;
                _isCreating = false;
                foreach (var item in GraduationDB.graduationContext.Areas)
                {
                    AreaIdComboBox.Items.Add(item);
                }
                foreach (var item in GraduationDB.graduationContext.Positions)
                {
                    PositionNameComboBox.Items.Add(item);
                }
                foreach (var item in GraduationDB.graduationContext.Classes)
                {
                    ClassComboBox.Items.Add(item);
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
                DataContext = _employee;
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

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
