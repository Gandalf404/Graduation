using Graduation.Models;
using Graduation.Models.Master;
using Graduation.Pages.InvoicesPages;
using Microsoft.EntityFrameworkCore;
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

namespace Graduation.Pages.WorkOrdersPages
{
    public partial class WorkOrdersPage : Page
    {
        private List<WorkOrderArea> _workOrderAreas;
        private Pau _selectedPau;
        private WorkOrderArea _selectedWorkOrderArea;
        public WorkOrdersPage()
        {
            try
            {
                InitializeComponent();
                _workOrderAreas = GraduationDB.graduationContext.WorkOrderAreas.Include(c => c.WorkOrder).Include(c => c.WorkOrder.Employee).Include(c => c.Operation).OrderBy(c => c.WorkOrderId).ToList();
                WorkOrdersDataGrid.ItemsSource = _workOrderAreas;
                PauNameComboBox.Items.Add(new Pau { PauName = "Все ДСЕ" });
                foreach (var item in GraduationDB.graduationContext.Paus)
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
                _workOrderAreas = GraduationDB.graduationContext.WorkOrderAreas.Include(c => c.WorkOrder).Include(c => c.WorkOrder.Employee).Include(c => c.Operation).OrderBy(c => c.WorkOrderId).ToList();
                WorkOrdersDataGrid.ItemsSource = _workOrderAreas;
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

        private void WorkOrdersViewItem_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                NavigationService.Navigate(new WorkOrdersPage());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void InvoicesViewItem_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                NavigationService.Navigate(new InvoicesPage());
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
                    _workOrderAreas = GraduationDB.graduationContext.WorkOrderAreas.Include(c => c.WorkOrder).Include(c => c.WorkOrder.Employee).Include(c => c.Operation).OrderBy(c => c.WorkOrderId).ToList();
                    WorkOrdersDataGrid.ItemsSource = _workOrderAreas;
                }
                else
                {
                    _workOrderAreas = _workOrderAreas.Where(c => c.WorkOrder.ReservationId.ToString().Contains(SearchTextBox.Text)
                                                            || c.WorkOrderId.ToString().Contains(SearchTextBox.Text)
                                                            || c.WorkOrder.WorkOrderCompilationDate.ToString().Contains(SearchTextBox.Text)
                                                            || c.WorkOrder.WorkOrderCloseDate.ToString().Contains(SearchTextBox.Text)
                                                            || c.WorkOrder.Employee.EmployeeSurname.ToLower().Contains(SearchTextBox.Text)
                                                            || c.WorkOrder.Pau.PauName.ToLower().Contains(SearchTextBox.Text)                                                             
                                                            || c.Operation.OperationName.ToLower().Contains(SearchTextBox.Text)
                                                            || c.OperationStartDate.ToString().Contains(SearchTextBox.Text)
                                                            || c.OperationStartTime.ToString().Contains(SearchTextBox.Text)
                                                            || c.OperationEndDate.ToString().Contains(SearchTextBox.Text)
                                                            || c.OperationEndTime.ToString().Contains(SearchTextBox.Text)).ToList();
                    WorkOrdersDataGrid.ItemsSource = _workOrderAreas;
                    if (WorkOrdersDataGrid.Items.Count == 0)
                    {
                        MessageBox.Show("Поиск не дал результатов", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void WorkOrderCompilationDateAscRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                _workOrderAreas = _workOrderAreas.OrderBy(c => c.WorkOrder.WorkOrderCompilationDate).ToList();
                WorkOrdersDataGrid.ItemsSource = _workOrderAreas;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void WorkOrderCompilationDateDescRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                _workOrderAreas = _workOrderAreas.OrderByDescending(c => c.WorkOrder.WorkOrderCompilationDate).ToList();
                WorkOrdersDataGrid.ItemsSource = _workOrderAreas;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void WorkOrderCloseDateAscRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                _workOrderAreas = _workOrderAreas.OrderBy(c => c.WorkOrder.WorkOrderCloseDate).ToList();
                WorkOrdersDataGrid.ItemsSource = _workOrderAreas;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void WorkOrderCloseDateDescRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                _workOrderAreas = _workOrderAreas.OrderByDescending(c => c.WorkOrder.WorkOrderCloseDate).ToList();
                WorkOrdersDataGrid.ItemsSource = _workOrderAreas;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void OperationStartTimeAscRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                _workOrderAreas = _workOrderAreas.OrderBy(c => c.OperationStartTime).ToList();
                WorkOrdersDataGrid.ItemsSource = _workOrderAreas;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void OperationStartTimeDescRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                _workOrderAreas = _workOrderAreas.OrderByDescending(c => c.OperationStartTime).ToList();
                WorkOrdersDataGrid.ItemsSource = _workOrderAreas;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void OperationEndTimeAscRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                _workOrderAreas = _workOrderAreas.OrderBy(c => c.OperationEndTime).ToList();
                WorkOrdersDataGrid.ItemsSource = _workOrderAreas;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void OperationEndTimeDescRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                _workOrderAreas = _workOrderAreas.OrderByDescending(c => c.OperationEndTime).ToList();
                WorkOrdersDataGrid.ItemsSource = _workOrderAreas;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void PAUComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                _selectedPau = (Pau)PauNameComboBox.SelectedItem;
                if (_selectedPau.PauName != "Все ДСЕ")
                {
                    _workOrderAreas = GraduationDB.graduationContext.WorkOrderAreas.Where(c => c.WorkOrder.Pau.PauName == _selectedPau.PauName).ToList();
                    WorkOrdersDataGrid.ItemsSource = _workOrderAreas;
                }
                else
                {
                    WorkOrderCompilationDateAscRadioButton.IsChecked = false;
                    WorkOrderCompilationDateDescRadioButton.IsChecked = false;
                    WorkOrderCloseDateAscRadioButton.IsChecked = false;
                    WorkOrderCloseDateDescRadioButton.IsChecked = false;
                    OperationStartTimeAscRadioButton.IsChecked = false;
                    OperationStartTimeDescRadioButton.IsChecked = false;
                    OperationEndTimeAscRadioButton.IsChecked = false;
                    OperationEndTimeDescRadioButton.IsChecked = false;
                    _workOrderAreas = GraduationDB.graduationContext.WorkOrderAreas.Include(c => c.WorkOrder).Include(c => c.WorkOrder.Employee).Include(c => c.Operation).OrderBy(c => c.WorkOrderId).ToList();
                    WorkOrdersDataGrid.ItemsSource = _workOrderAreas;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void WorkOrderCreateButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                NavigationService.Navigate(new WorkOrderCreatePage());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void UpdateWorkOrderMenuItem_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                _selectedWorkOrderArea = (WorkOrderArea)WorkOrdersDataGrid.SelectedItem;
                if (_selectedWorkOrderArea != null)
                {
                    NavigationService.Navigate(new WorkOrderCreatePage(_selectedWorkOrderArea));
                }
                else
                {
                    MessageBox.Show("Выберите заказ-наряд для изменения", "Внимание", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void PrintWorkOrderMenuItem_Click(object sender, RoutedEventArgs e)
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
