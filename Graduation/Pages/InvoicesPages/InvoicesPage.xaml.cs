using Graduation.Models;
using Graduation.Models.Master;
using Graduation.Pages.WorkOrdersPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Win32;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Xceed.Words.NET;

namespace Graduation.Pages.InvoicesPages
{
    public partial class InvoicesPage : Page
    {
        private List<InvoicePau> _invoicesPaus;
        private WorkOrder _selectedWorkOrder;
        private Department _selectedDepartment;
        private InvoicePau _selectedInvoicePau;
        private DocX _docX;
        SaveFileDialog saveFileDialog;

        public InvoicesPage()
        {
            try
            {
                InitializeComponent();
                _invoicesPaus = GraduationDB.graduationContext.InvoicePaus.Include(c => c.Invoice).Include(c => c.Invoice.WorkOrder).ToList();
                InvoicesDataGrid.ItemsSource = _invoicesPaus;
                WorkOrderIdComboBox.Items.Add(new WorkOrder { WorkOrderId = 0 });
                foreach (var item in GraduationDB.graduationContext.WorkOrders)
                {
                    WorkOrderIdComboBox.Items.Add(item);
                }
                DepartmentIdComboBox.Items.Add(new Department { DepartmentId = 0 });
                foreach (var item in GraduationDB.graduationContext.Departments)
                {
                    DepartmentIdComboBox.Items.Add(item);
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
                _invoicesPaus = GraduationDB.graduationContext.InvoicePaus.Include(c => c.Invoice).Include(c => c.Invoice.WorkOrder).ToList();
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

        //UNDONE: Сделать вывод окна если ничего не найдено.
        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (String.IsNullOrWhiteSpace(SearchTextBox.Text))
                {
                    _invoicesPaus = GraduationDB.graduationContext.InvoicePaus.Include(c => c.Invoice).Include(c => c.Invoice.WorkOrder.Pau).ToList();
                    InvoicesDataGrid.ItemsSource = _invoicesPaus;
                }
                else
                {
                    _invoicesPaus = _invoicesPaus.Where(c => c.InvoiceId.ToString().Contains(SearchTextBox.Text)
                                                        || c.Invoice.WorkOrder.WorkOrderId.ToString().Contains(SearchTextBox.Text)
                                                        || c.Invoice.InvoiceCompilationDate.ToString().Contains(SearchTextBox.Text)
                                                        || c.Invoice.DepartmentId.ToString().Contains(SearchTextBox.Text)
                                                        || c.Invoice.DepartmentReceiverId.ToString().Contains(SearchTextBox.Text)
                                                        || c.Invoice.WorkOrder.PauId.ToString().Contains(SearchTextBox.Text)
                                                        || c.Invoice.WorkOrder.Pau.PauCount.ToString().Contains(SearchTextBox.Text)
                                                        || c.FactCount.ToString().Contains(SearchTextBox.Text)).ToList();
                    InvoicesDataGrid.ItemsSource = _invoicesPaus;
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

        private void PauCountAscRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                _invoicesPaus = _invoicesPaus.OrderBy(c => c.Invoice.WorkOrder.Pau.PauCount).ToList();
                InvoicesDataGrid.ItemsSource = _invoicesPaus;
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
                _invoicesPaus = _invoicesPaus.OrderByDescending(c => c.Invoice.WorkOrder.Pau.PauCount).ToList();
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

        //TODO: Подумать над фильтрацией либо по номеру заказ-наряда, либо по цеху отправителю.
        private void WorkOrderIdComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                _selectedWorkOrder = (WorkOrder)WorkOrderIdComboBox.SelectedItem;
                if (_selectedWorkOrder.WorkOrderId != 0)
                {
                    _invoicesPaus = GraduationDB.graduationContext.InvoicePaus.Where(c => c.Invoice.WorkOrder.WorkOrderId == _selectedWorkOrder.WorkOrderId).ToList();
                    InvoicesDataGrid.ItemsSource = _invoicesPaus;
                }
                else
                {
                    _invoicesPaus = GraduationDB.graduationContext.InvoicePaus.Include(c => c.Invoice).Include(c => c.Invoice.WorkOrder).OrderBy(c => c.InvoiceId).ToList();
                    InvoicesDataGrid.ItemsSource = _invoicesPaus;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void DepartmentIdComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                _selectedDepartment = (Department)DepartmentIdComboBox.SelectedItem;
                if (_selectedDepartment.DepartmentId != 0)
                {
                    if (DepartmentRadioButton.IsChecked == true)
                    {
                        _invoicesPaus = GraduationDB.graduationContext.InvoicePaus.Where(c => c.Invoice.DepartmentId == _selectedDepartment.DepartmentId).ToList();
                        InvoicesDataGrid.ItemsSource = _invoicesPaus;
                    }
                    else if (DepartmentReceiverRadioButton.IsChecked == true)
                    {
                        _invoicesPaus = GraduationDB.graduationContext.InvoicePaus.Where(c => c.Invoice.DepartmentReceiverId == _selectedDepartment.DepartmentId).ToList();
                        InvoicesDataGrid.ItemsSource = _invoicesPaus;
                    }
                }
                else
                {
                    InvoicesCompilationDateAscRadioButton.IsChecked = false;
                    InvoicesCompilationDateDescRadioButton.IsChecked = false;
                    PauCountAscRadioButton.IsChecked = false;
                    PauCountDescRadioButton.IsChecked = false;
                    FactCountAscRadioButton.IsChecked = false;
                    FactCountDescRadioButton.IsChecked = false;
                    DepartmentRadioButton.IsChecked = false;
                    DepartmentReceiverRadioButton.IsChecked = false;
                    _invoicesPaus = GraduationDB.graduationContext.InvoicePaus.Include(c => c.Invoice).Include(c => c.Invoice.WorkOrder).OrderBy(c => c.InvoiceId).ToList();
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
                NavigationService.Navigate(new InvoiceCreatePage());
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
                    NavigationService.Navigate(new InvoiceCreatePage(_selectedInvoicePau));
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
                    _docX.ReplaceText("{7}", $"{_selectedInvoicePau.Invoice.WorkOrder.PauCount}");
                    _docX.ReplaceText("{8}", $"{_selectedInvoicePau.FactCount}");
                    _docX.ReplaceText("{9}", $"{_selectedInvoicePau.Invoice.WorkOrder.Employee.EmployeeSurname}");
                    saveFileDialog = new SaveFileDialog() { DefaultDirectory = @$"C:\Users\{Environment.UserName}\source\repos\Graduation\Graduation\Resources\Invoices" };
                    if (saveFileDialog.ShowDialog() != true)
                    {
                        return;
                    }
                    _docX.SaveAs(saveFileDialog.FileName);
                    MessageBox.Show("Форма для печати приемо-сдаточной накладной успешно создана", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
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
