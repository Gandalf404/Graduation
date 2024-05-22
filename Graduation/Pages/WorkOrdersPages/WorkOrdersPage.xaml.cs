using Graduation.Models;
using Graduation.Models.Master;
using Graduation.Pages.InvoicesPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Win32;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Xceed.Words.NET;

namespace Graduation.Pages.WorkOrdersPages
{
    public partial class WorkOrdersPage : Page
    {
        private Employee _employee;
        private List<WorkOrderArea> _workOrderAreas;
        private Pau _selectedPau;
        private WorkOrderArea _selectedWorkOrderArea;
        private DocX _docX;
        private SaveFileDialog _saveFileDialog;
        public WorkOrdersPage()
        {
            try
            {
                InitializeComponent();
                _workOrderAreas = WorkOrdersDB.graduationContextMaster.WorkOrderAreas.Include(c => c.WorkOrder).Include(c => c.WorkOrder.Employee).Include(c => c.WorkOrder.Reservation).Include(c => c.Operation).OrderBy(c => c.WorkOrderId).ToList();
                WorkOrdersDataGrid.ItemsSource = _workOrderAreas;
                PauNameComboBox.Items.Add(new Pau { PauName = "Все ДСЕ" });
                foreach (var item in WorkOrdersDB.graduationContextMaster.Paus.OrderBy(c => c.PauId))
                {
                    PauNameComboBox.Items.Add(item);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        //TODO: Сделать отображение заказ-нарядов которые составил авторизованный мастер.
        public WorkOrdersPage(Employee employee)
        {
            try
            {
                InitializeComponent();
                _employee = employee;
                _workOrderAreas = WorkOrdersDB.graduationContextMaster.WorkOrderAreas.Include(c => c.WorkOrder).Include(c => c.WorkOrder.Employee).Include(c => c.WorkOrder.Reservation).Include(c => c.Operation)
                    .Where(c => c.WorkOrder.EmployeeId == employee.EmployeeId).ToList();
                WorkOrdersDataGrid.ItemsSource = _workOrderAreas;
                PauNameComboBox.Items.Add(new Pau { PauName = "Все ДСЕ" });
                foreach (var item in WorkOrdersDB.graduationContextMaster.Paus)
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
                _workOrderAreas = WorkOrdersDB.graduationContextMaster.WorkOrderAreas.Include(c => c.WorkOrder).Include(c => c.WorkOrder.Employee).Include(c => c.Operation).OrderBy(c => c.WorkOrderId).ToList();
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
                    _workOrderAreas = WorkOrdersDB.graduationContextMaster.WorkOrderAreas.Include(c => c.WorkOrder).Include(c => c.WorkOrder.Employee).Include(c => c.Operation).OrderBy(c => c.WorkOrderId).ToList();
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
                    _workOrderAreas = WorkOrdersDB.graduationContextMaster.WorkOrderAreas.Where(c => c.WorkOrder.Pau.PauName == _selectedPau.PauName).ToList();
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
                    _workOrderAreas = WorkOrdersDB.graduationContextMaster.WorkOrderAreas.Include(c => c.WorkOrder).Include(c => c.WorkOrder.Employee).Include(c => c.Operation).OrderBy(c => c.WorkOrderId).ToList();
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
                _selectedWorkOrderArea = (WorkOrderArea)WorkOrdersDataGrid.SelectedItem;
                if (_selectedWorkOrderArea != null)
                {
                    _docX = DocX.Load(new FileInfo("..//..//..//Resources//WorkOrderTemplate.docx").FullName);
                    _docX.ReplaceText("{0}", $"{_selectedWorkOrderArea.WorkOrderId}");
                    _docX.ReplaceText("{1}", $"{_selectedWorkOrderArea.WorkOrderCompilationDate}");
                    _docX.ReplaceText("{2}", $"{_selectedWorkOrderArea.WorkOrderId}");
                    _docX.ReplaceText("{3}", $"{_selectedWorkOrderArea.WorkOrder.ReservationId}");
                    if (_selectedWorkOrderArea.WorkOrder.WorkOrderCloseDate != null)
                    {
                        _docX.ReplaceText("{4}", $"{_selectedWorkOrderArea.WorkOrder.WorkOrderCloseDate}");
                    }
                    else
                    {
                        _docX.ReplaceText("{4}", "-");
                    }
                    _docX.ReplaceText("{5}", $"{_selectedWorkOrderArea.WorkOrder.Employee.EmployeeSurname}");
                    _docX.ReplaceText("{6}", $"{_selectedWorkOrderArea.WorkOrder.Pau.PauName}");
                    _docX.ReplaceText("{7}", $"{_selectedWorkOrderArea.WorkOrder.Reservation.ReservationCount}");
                    _docX.ReplaceText("{8}", $"{_selectedWorkOrderArea.AreaId}");
                    _docX.ReplaceText("{9}", $"{_selectedWorkOrderArea.Operation.OperationName}");
                    _docX.ReplaceText("{10}", $"{_selectedWorkOrderArea.OperationStartDate}");
                    _docX.ReplaceText("{11}", $"{_selectedWorkOrderArea.OperationStartTime}");
                    _docX.ReplaceText("{12}", $"{_selectedWorkOrderArea.OperationEndDate}");
                    _docX.ReplaceText("{13}", $"{_selectedWorkOrderArea.OperationEndTime}");
                    _docX.ReplaceText("{14}", $"{_employee.EmployeeSurname}");
                    _saveFileDialog = new SaveFileDialog() { DefaultDirectory = @$"C:\Users\{Environment.UserName}\source\repos\Graduation\Graduation\Resources\WorkOrders" };
                    if (_saveFileDialog.ShowDialog() != true)
                    {
                        return;
                    }
                    _docX.SaveAs(_saveFileDialog.FileName);
                    MessageBox.Show("Форма для печати заказ-наряда успешно создана", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show("Выберите накладную для печати", "Внимание", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
