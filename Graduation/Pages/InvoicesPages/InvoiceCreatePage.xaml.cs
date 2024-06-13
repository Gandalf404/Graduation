﻿using Graduation.Models;
using Graduation.Models.Master;
using Npgsql;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Navigation;
using WorkOrder = Graduation.Models.Master.WorkOrder;

namespace Graduation.Pages.InvoicesPages
{
    public partial class InvoiceCreatePage : Page
    {
        private Employee _employee;
        private Invoice _invoice;
        private InvoicePau _invoicePau;
        private bool _isCreating;
        public InvoiceCreatePage(Employee employee)
        {
            try
            {
                InitializeComponent();
                _employee = employee;
                _invoice = new Invoice();
                _invoicePau = new InvoicePau();
                _isCreating = true;
                foreach (var item in WorkOrdersDB.graduationContextMaster.WorkOrders.Where(c => c.EmployeeId == _employee.EmployeeId && c.WorkOrderCloseDate == null)
                    .OrderBy(c => c.WorkOrderId))
                {
                    WorkOrderIdComboBox.Items.Add(item);
                }
                foreach (var item in WorkOrdersDB.graduationContextMaster.Departments.OrderBy(c => c.DepartmentId))
                {
                    DepartmentIdComboBox.Items.Add(item);
                    DepartmentReceiverIdComboBox.Items.Add(item);
                }
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

        public InvoiceCreatePage(Employee employee, InvoicePau invoicePau)
        {
            try
            {
                InitializeComponent();
                _employee = employee;
                _invoicePau = invoicePau;
                _isCreating = false;
                foreach (var item in WorkOrdersDB.graduationContextMaster.WorkOrders.Where(c => c.EmployeeId == _employee.EmployeeId && c.WorkOrderCloseDate == null)
                    .OrderBy(c => c.WorkOrderId))
                {
                    WorkOrderIdComboBox.Items.Add(item);
                }
                foreach (var item in WorkOrdersDB.graduationContextMaster.Departments.OrderBy(c => c.DepartmentId))
                {
                    DepartmentIdComboBox.Items.Add(item);
                    DepartmentReceiverIdComboBox.Items.Add(item);
                }
                foreach (var item in WorkOrdersDB.graduationContextMaster.Paus.OrderBy(c => c.PauId))
                {
                    PauNameComboBox.Items.Add(item);
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

        private void WorkOrderIdComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                WorkOrderCompilationDateComboBox.Items.Clear();
                if ((WorkOrder)WorkOrderIdComboBox.SelectedItem == null) { return; }
                foreach (var item in WorkOrdersDB.graduationContextMaster.WorkOrders.Where(c => c.EmployeeId == _employee.EmployeeId 
                    && c.WorkOrderCompilationDate
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

        private void DepartmentIdComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                DepartmentReceiverIdComboBox.Items.Clear();
                foreach (var item in WorkOrdersDB.graduationContextMaster.Departments.Where(c => ((Department)DepartmentIdComboBox.SelectedItem).DepartmentId
                    != c.DepartmentId))
                {
                    DepartmentReceiverIdComboBox.Items.Add(item);
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
                    if (!String.IsNullOrWhiteSpace(InvoiceIdTextBox.Text)) { _invoice.InvoiceId = Convert.ToInt32(InvoiceIdTextBox.Text); } else { throw new Exception(); }
                    _invoice.InvoiceCompilationDate = DateOnly.FromDateTime(DateTime.Now);
                    if (WorkOrderIdComboBox.SelectedItem != null) { _invoice.WorkOrderId = ((WorkOrder)WorkOrderIdComboBox.SelectedItem).WorkOrderId; } else { throw new Exception(); }
                    if (WorkOrderCompilationDateComboBox.SelectedItem != null) { _invoice.WorkOrderCompilationDate = ((WorkOrder)WorkOrderCompilationDateComboBox.SelectedItem).WorkOrderCompilationDate; } else { throw new Exception(); }
                    if (DepartmentIdComboBox.SelectedItem != null) { _invoice.DepartmentId = ((Department)DepartmentIdComboBox.SelectedItem).DepartmentId; } else { throw new Exception(); }
                    if (DepartmentReceiverIdComboBox.SelectedItem != null) { _invoice.DepartmentReceiverId = ((Department)DepartmentReceiverIdComboBox.SelectedItem).DepartmentId; } else { throw new Exception(); }
                    if (WorkOrderCompilationDateComboBox.SelectedItem != null) { _invoice.WorkOrderCompilationDate = ((WorkOrder)WorkOrderCompilationDateComboBox.SelectedItem).WorkOrderCompilationDate; } else { throw new Exception(); }

                    if (!String.IsNullOrWhiteSpace(InvoiceIdTextBox.Text)) { _invoicePau.InvoiceId = Convert.ToInt32(InvoiceIdTextBox.Text); } else { throw new Exception(); }
                    _invoicePau.InvoiceCompilationDate = DateOnly.FromDateTime(DateTime.Now);
                    if (PauNameComboBox.SelectedItem != null) { _invoicePau.PauId = ((Pau)PauNameComboBox.SelectedItem).PauId; } else { throw new Exception(); }
                    if (!String.IsNullOrWhiteSpace(FactCountTextBox.Text)) { _invoicePau.FactCount = Convert.ToInt32(FactCountTextBox.Text); } else { throw new Exception(); }

                    WorkOrdersDB.graduationContextMaster.Add(_invoice);
                    WorkOrdersDB.graduationContextMaster.Add(_invoicePau);
                    WorkOrdersDB.graduationContextMaster.SaveChanges();
                    MessageBox.Show("Накладная успешно добавлена", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    if (WorkOrderIdComboBox.SelectedItem != null) { _invoicePau.Invoice.WorkOrderId = ((WorkOrder)WorkOrderIdComboBox.SelectedItem).WorkOrderId; } else { throw new Exception(); }
                    if (WorkOrderCompilationDateComboBox.SelectedItem != null) { _invoicePau.Invoice.WorkOrderCompilationDate = ((WorkOrder)WorkOrderCompilationDateComboBox.SelectedItem).WorkOrderCompilationDate; } else { throw new Exception(); }
                    if (PauNameComboBox.SelectedItem != null) { _invoicePau.PauId = ((Pau)PauNameComboBox.SelectedItem).PauId; } else { throw new Exception(); }
                    if (DepartmentIdComboBox.SelectedItem != null) { _invoicePau.Invoice.DepartmentId = ((Department)DepartmentIdComboBox.SelectedItem).DepartmentId; } else { throw new Exception(); }
                    if (DepartmentReceiverIdComboBox.SelectedItem != null) { _invoicePau.Invoice.DepartmentReceiverId = ((Department)DepartmentReceiverIdComboBox.SelectedItem).DepartmentId; } else { throw new Exception(); }
                    if (!String.IsNullOrWhiteSpace(FactCountTextBox.Text)) { _invoicePau.FactCount = Convert.ToInt32(FactCountTextBox.Text); } else { throw new Exception(); }
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
                NavigationService.Navigate(new InvoicesPage(_employee));
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
