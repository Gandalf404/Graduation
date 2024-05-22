using Graduation.Models;
using Graduation.Models.Master;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Navigation;

namespace Graduation.Pages.InvoicesPages
{
    public partial class InvoiceCreatePage : Page
    {
        private Invoice _invoice;
        private InvoicePau _invoicePau;
        private bool _isCreating;
        public InvoiceCreatePage()
        {
            try
            {
                InitializeComponent();
                _invoice = new Invoice();
                _invoicePau = new InvoicePau();
                _isCreating = true;
                foreach (var item in WorkOrdersDB.graduationContextMaster.WorkOrders.OrderBy(c => c.WorkOrderId))
                {
                    WorkOrderIdComboBox.Items.Add(item);
                }
                foreach (var item in WorkOrdersDB.graduationContextMaster.Departments)
                {
                    DepartmentIdComboBox.Items.Add(item);
                    DepartmentReceiverIdComboBox.Items.Add(item);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public InvoiceCreatePage(InvoicePau invoicePau)
        {
            try
            {
                InitializeComponent();
                _invoicePau = invoicePau;
                _isCreating = false;
                foreach (var item in WorkOrdersDB.graduationContextMaster.WorkOrders.OrderBy(c => c.WorkOrderId))
                {
                    WorkOrderIdComboBox.Items.Add(item);
                }
                foreach (var item in WorkOrdersDB.graduationContextMaster.Departments)
                {
                    DepartmentIdComboBox.Items.Add(item);
                    DepartmentReceiverIdComboBox.Items.Add(item);
                }
                InvoiceIdTextBox.IsEnabled = false;
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
                DataContext = _invoicePau;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        //TODO: Починить этот и WorkOrderIdComboBox.
        private void WorkOrderIdComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                foreach (var item in WorkOrdersDB.graduationContextMaster.WorkOrders.Where(c => c.WorkOrderCompilationDate
                        == ((WorkOrder)WorkOrderIdComboBox.SelectedItem).WorkOrderCompilationDate))
                {
                    WorkOrderCompilationDateComboBox.Items.Add(item);
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
                if (_isCreating)
                {
                    _invoice.InvoiceId = Convert.ToInt32(InvoiceIdTextBox.Text);
                    _invoice.InvoiceCompilationDate = DateOnly.FromDateTime(DateTime.Now);
                    _invoice.WorkOrderId = ((WorkOrder)WorkOrderIdComboBox.SelectedItem).WorkOrderId;
                    _invoice.WorkOrderCompilationDate = ((WorkOrder)WorkOrderCompilationDateComboBox.SelectedItem).WorkOrderCompilationDate;
                    //TODO: Починить ComboBox'ы с цехами.
                    _invoice.DepartmentId = ((Department)DepartmentIdComboBox.SelectedItem).DepartmentId;
                    _invoice.DepartmentReceiverId = ((Department)DepartmentReceiverIdComboBox.SelectedItem).DepartmentId;
                    _invoice.WorkOrderCompilationDate = ((WorkOrder)WorkOrderIdComboBox.SelectedItem).WorkOrderCompilationDate;

                    _invoicePau.InvoiceId = Convert.ToInt32(InvoiceIdTextBox.Text);
                    _invoicePau.InvoiceCompilationDate = DateOnly.FromDateTime(DateTime.Now);
                    _invoicePau.FactCount = Convert.ToInt32(FactCountTextBox.Text);

                    WorkOrdersDB.graduationContextMaster.Add(_invoice);
                    WorkOrdersDB.graduationContextMaster.Add(_invoicePau);
                    WorkOrdersDB.graduationContextMaster.SaveChanges();
                    MessageBox.Show("Накладная успешно добавлена", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    _invoicePau.Invoice.WorkOrderId = ((WorkOrder)WorkOrderIdComboBox.SelectedItem).WorkOrderId;
                    _invoicePau.Invoice.WorkOrderCompilationDate = ((WorkOrder)WorkOrderCompilationDateComboBox.SelectedItem).WorkOrderCompilationDate;
                    _invoicePau.Invoice.DepartmentId = ((Department)DepartmentIdComboBox.SelectedItem).DepartmentId;
                    _invoicePau.Invoice.DepartmentReceiverId = ((Department)DepartmentReceiverIdComboBox.SelectedItem).DepartmentId;
                    _invoicePau.FactCount = Convert.ToInt32(FactCountTextBox.Text);

                    WorkOrdersDB.graduationContextMaster.SaveChanges();
                    MessageBox.Show("Накладная успешно изменена", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch when (String.IsNullOrWhiteSpace(InvoiceIdTextBox.Text) || WorkOrderIdComboBox.SelectedItem == null || DepartmentIdComboBox.SelectedItem == null
                        || DepartmentReceiverIdComboBox.SelectedItem == null || String.IsNullOrWhiteSpace(FactCountTextBox.Text))
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
                NavigationService.Navigate(new InvoicesPage());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void InvoiceIdTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (Char.IsDigit(e.Text, 0)) return;
            e.Handled = true;
        }

        private void FactCountTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (Char.IsDigit(e.Text, 0)) return;
            e.Handled = true;
        }
    }
}
