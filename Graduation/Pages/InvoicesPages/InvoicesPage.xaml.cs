using Graduation.Models;
using Graduation.Models.Master;
using Graduation.Pages.WorkOrdersPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Win32;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using System.Windows.Threading;
using Xceed.Words.NET;

namespace Graduation.Pages.InvoicesPages
{
    public partial class InvoicesPage : Page
    {
        private Employee _employee;
        private List<InvoicePau> _invoicesPaus;
        private WorkOrder _selectedWorkOrder;
        private Department _selectedDepartment;
        private InvoicePau _selectedInvoicePau;
        private DocX _docX;
        private SaveFileDialog _saveFileDialog;

        public InvoicesPage(Employee employee)
        {
            try
            {
                InitializeComponent();
                _employee = employee;
                _invoicesPaus = WorkOrdersDB.graduationContextMaster.InvoicePaus.Include(c => c.Invoice).Include(c => c.Invoice.WorkOrder).Include(c => c.Invoice.WorkOrder.Reservation).Where(c => c.Invoice.WorkOrder.EmployeeId == _employee.EmployeeId)
                    .OrderBy(c => c.InvoiceId).ToList();
                InvoicesDataGrid.ItemsSource = _invoicesPaus;
                WorkOrderIdComboBox.Items.Add(new WorkOrder { WorkOrderId = 0 });
                foreach (var item in WorkOrdersDB.graduationContextMaster.WorkOrders.Where(c => c.EmployeeId == _employee.EmployeeId).OrderBy(c => c.WorkOrderId))
                {
                    WorkOrderIdComboBox.Items.Add(item);
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
                _invoicesPaus = WorkOrdersDB.graduationContextMaster.InvoicePaus.Include(c => c.Invoice).Include(c => c.Invoice.WorkOrder).Include(c => c.Invoice.WorkOrder.Reservation).Where(c => c.Invoice.WorkOrder.EmployeeId == _employee.EmployeeId)
                    .OrderBy(c => c.InvoiceId).ToList();
                InvoicesDataGrid.ItemsSource = _invoicesPaus;
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
                NavigationService.Navigate(new WorkOrdersPage(_employee));
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
                NavigationService.Navigate(new InvoicesPage(_employee));
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
                    _invoicesPaus = WorkOrdersDB.graduationContextMaster.InvoicePaus.Include(c => c.Invoice).Include(c => c.Invoice.WorkOrder).OrderBy(c => c.InvoiceId).ToList();
                    InvoicesDataGrid.ItemsSource = _invoicesPaus;
                }
                else
                {
                    _invoicesPaus = _invoicesPaus.Where(c => c.InvoiceId.ToString().Contains(SearchTextBox.Text)
                                                        || c.Invoice.WorkOrder.WorkOrderId.ToString().Contains(SearchTextBox.Text)
                                                        || c.Invoice.WorkOrderCompilationDate.ToString().Contains(SearchTextBox.Text)
                                                        || c.Invoice.InvoiceCompilationDate.ToString().Contains(SearchTextBox.Text)
                                                        || c.Invoice.DepartmentId.ToString().Contains(SearchTextBox.Text)
                                                        || c.Invoice.DepartmentReceiverId.ToString().Contains(SearchTextBox.Text)
                                                        || c.Invoice.WorkOrder.Pau.PauName.ToString().ToLower().Contains(SearchTextBox.Text)
                                                        || c.FactCount.ToString().Contains(SearchTextBox.Text)
                                                        || c.Invoice.WorkOrder.Reservation.ReservationCount.ToString().Contains(SearchTextBox.Text)).ToList();
                    if (InvoicesDataGrid.Items.Count == 0)
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

        private void InvoicesCompilationDateAscRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                _invoicesPaus = _invoicesPaus.OrderBy(c => c.Invoice.InvoiceCompilationDate).ToList();
                InvoicesDataGrid.ItemsSource = _invoicesPaus;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void InvoicesCompilationDateDescRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                _invoicesPaus = _invoicesPaus.OrderByDescending(c => c.Invoice.InvoiceCompilationDate).ToList();
                InvoicesDataGrid.ItemsSource = _invoicesPaus;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ReservationCountAscRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                _invoicesPaus = _invoicesPaus.OrderBy(c => c.Invoice.WorkOrder.Reservation.ReservationCount).ToList();
                InvoicesDataGrid.ItemsSource = _invoicesPaus;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ReservationCountDescRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                _invoicesPaus = _invoicesPaus.OrderByDescending(c => c.Invoice.WorkOrder.Reservation.ReservationCount).ToList();
                InvoicesDataGrid.ItemsSource = _invoicesPaus;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void FactCountAscRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                _invoicesPaus = _invoicesPaus.OrderBy(c => c.FactCount).ToList();
                InvoicesDataGrid.ItemsSource = _invoicesPaus;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void FactCountDescRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                _invoicesPaus = _invoicesPaus.OrderByDescending(c => c.FactCount).ToList();
                InvoicesDataGrid.ItemsSource = _invoicesPaus;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void WorkOrderIdComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                _selectedWorkOrder = (WorkOrder)WorkOrderIdComboBox.SelectedItem;
                if (_selectedWorkOrder.WorkOrderId != 0)
                {
                    _invoicesPaus = WorkOrdersDB.graduationContextMaster.InvoicePaus.Where(c => c.Invoice.WorkOrder.WorkOrderId == _selectedWorkOrder.WorkOrderId).ToList();
                    InvoicesDataGrid.ItemsSource = _invoicesPaus;
                    if (_invoicesPaus.Count == 0)
                    {
                        MessageBox.Show("По данному заказ-наряду ещё не составлялись премо-сдаточные накладные", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
                else
                {
                    _invoicesPaus = WorkOrdersDB.graduationContextMaster.InvoicePaus.Include(c => c.Invoice).Include(c => c.Invoice.WorkOrder).Include(c => c.Invoice.WorkOrder.Reservation).Where(c => c.Invoice.WorkOrder.EmployeeId == _employee.EmployeeId)
                        .OrderBy(c => c.InvoiceId).ToList();
                    InvoicesDataGrid.ItemsSource = _invoicesPaus;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void InvoiceCreateButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                NavigationService.Navigate(new InvoiceCreatePage(_employee));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void UpdateInvoiceMenuItem_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                _selectedInvoicePau = (InvoicePau)InvoicesDataGrid.SelectedItem;
                if (_selectedInvoicePau != null)
                {
                    NavigationService.Navigate(new InvoiceCreatePage(_employee, _selectedInvoicePau));
                }
                else
                {
                    MessageBox.Show("Выберите накладную для изменения", "Внимание", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void PrintInvoiceMenuItem_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                _selectedInvoicePau = (InvoicePau)InvoicesDataGrid.SelectedItem;
                if (_selectedInvoicePau != null)
                {
                    _docX = DocX.Load(new FileInfo("..//..//..//Resources//InvoiceTemplate.docx").FullName);
                    _docX.ReplaceText("{0}", $"{_selectedInvoicePau.InvoiceId}");
                    _docX.ReplaceText("{1}", $"{_selectedInvoicePau.InvoiceCompilationDate}");
                    _docX.ReplaceText("{2}", $"{_selectedInvoicePau.Invoice.DepartmentId}");
                    _docX.ReplaceText("{3}", $"{_selectedInvoicePau.Invoice.DepartmentReceiverId}");
                    _docX.ReplaceText("{4}", $"{_selectedInvoicePau.Invoice.WorkOrderId}");
                    _docX.ReplaceText("{5}", $"{_selectedInvoicePau.Invoice.WorkOrder.PauId}");
                    _docX.ReplaceText("{6}", $"{_selectedInvoicePau.Invoice.WorkOrder.Pau.PauName}");
                    _docX.ReplaceText("{7}", $"{_selectedInvoicePau.FactCount}");
                    _docX.ReplaceText("{8}", $"{_selectedInvoicePau.Invoice.WorkOrder.Reservation.ReservationCount}");
                    _docX.ReplaceText("{9}", $"{_selectedInvoicePau.Invoice.WorkOrder.Employee.EmployeeSurname}");
                    _saveFileDialog = new SaveFileDialog() { InitialDirectory = @$"C:\Users\{Environment.UserName}\source\repos\Graduation\Graduation\Resources\Invoices" };
                    if (Directory.Exists(_saveFileDialog.InitialDirectory))
                    {
                        if (_saveFileDialog.ShowDialog() != true) { return; }
                        _docX.SaveAs(_saveFileDialog.FileName);
                    }
                    else
                    {
                        Directory.CreateDirectory(_saveFileDialog.InitialDirectory);
                        if (_saveFileDialog.ShowDialog() != true) { return; }
                        _docX.SaveAs(_saveFileDialog.FileName);
                    }
                    MessageBox.Show("Форма для печати приемо-сдаточной накладной успешно создана", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show("Выберите приемо-сдаточную накладную для печати", "Внимание", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void DispatcherTimer_Tick(object? sender, EventArgs e)
        {
            _invoicesPaus = WorkOrdersDB.graduationContextMaster.InvoicePaus.Include(c => c.Invoice).Include(c => c.Invoice.WorkOrder).OrderBy(c => c.InvoiceId).ToList();
            InvoicesDataGrid.ItemsSource = _invoicesPaus;
        }
    }
}
