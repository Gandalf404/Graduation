using Graduation.Models;
using Graduation.Models.Master;
using Npgsql;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Navigation;

namespace Graduation.Pages.WorkOrdersPages
{
    public partial class WorkOrderCreatePage : Page
    {
        private WorkOrder _workOrder;
        private WorkOrderArea _workOrderArea;
        private bool _isCreating;
        public WorkOrderCreatePage()
        {
            try
            {
                InitializeComponent();
                _workOrder = new WorkOrder();
                _workOrderArea = new WorkOrderArea();
                _isCreating = true;
                foreach (var item in WorkOrdersDB.graduationContextMaster.Reservations)
                {
                    ReservationIdComboBox.Items.Add(item);
                }
                foreach (var item in WorkOrdersDB.graduationContextMaster.Paus.OrderBy(c => c.PauId))
                {
                    PauNameComboBox.Items.Add(item);
                }
                foreach (var item in WorkOrdersDB.graduationContextMaster.Employees.OrderBy(c => c.EmployeeId))
                {
                    EmployeeSurnameComboBox.Items.Add(item);
                }
                foreach (var item in WorkOrdersDB.graduationContextMaster.Areas)
                {
                    AreaIdComboBox.Items.Add(item);
                }
                foreach (var item in WorkOrdersDB.graduationContextMaster.Operations)
                {
                    OperationNameComboBox.Items.Add(item);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public WorkOrderCreatePage(WorkOrderArea workOrderArea)
        {
            try
            {
                InitializeComponent();
                _workOrderArea = workOrderArea;
                _isCreating = false;
                foreach (var item in WorkOrdersDB.graduationContextMaster.Reservations)
                {
                    ReservationIdComboBox.Items.Add(item);
                }
                foreach (var item in WorkOrdersDB.graduationContextMaster.Paus.OrderBy(c => c.PauId))
                {
                    PauNameComboBox.Items.Add(item);
                }
                foreach (var item in WorkOrdersDB.graduationContextMaster.Employees.OrderBy(c => c.EmployeeId))
                {
                    EmployeeSurnameComboBox.Items.Add(item);
                }
                foreach (var item in WorkOrdersDB.graduationContextMaster.Areas)
                {
                    AreaIdComboBox.Items.Add(item);
                }
                foreach (var item in WorkOrdersDB.graduationContextMaster.Operations)
                {
                    OperationNameComboBox.Items.Add(item);
                }
                if (_workOrderArea.WorkOrder.WorkOrderCloseDate != null)
                {
                    WorkOrderCloseCheckBox.IsChecked = true;
                    WorkOrderCloseCheckBox.IsEnabled = false;
                    WorkOrderCloseCheckBox.Content = "Наряд закрыт";
                }
                WorkOrderIdTextBox.IsEnabled = false;
                AreaIdComboBox.IsEnabled = false;
                OperationNameComboBox.IsEnabled = false;
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
                DataContext = _workOrderArea;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        private void ReservationIdComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                ReservationCountTextBlock.Text = ((Reservation)ReservationIdComboBox.SelectedItem).ReservationCount.ToString();
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
                if (_isCreating == true)
                {
                    _workOrder.WorkOrderId = Convert.ToInt32(WorkOrderIdTextBox.Text);
                    _workOrder.ReservationId = ((Reservation)ReservationIdComboBox.SelectedItem).ReservationId;
                    _workOrder.WorkOrderCompilationDate = DateOnly.FromDateTime(DateTime.Now);
                    _workOrder.PauId = ((Pau)PauNameComboBox.SelectedItem).PauId;
                    _workOrder.EmployeeId = ((Employee)EmployeeSurnameComboBox.SelectedItem).EmployeeId;
                    _workOrder.ReservationCompilationDate = ((Reservation)ReservationIdComboBox.SelectedItem).ReservationCompilationDate;
                    if (WorkOrderCloseCheckBox.IsChecked == true)
                    {
                        _workOrder.WorkOrderCloseDate = DateOnly.FromDateTime(DateTime.Now);
                    }

                    _workOrderArea.WorkOrderId = Convert.ToInt32(WorkOrderIdTextBox.Text);
                    _workOrderArea.WorkOrderCompilationDate = DateOnly.FromDateTime(DateTime.Now);
                    _workOrderArea.AreaId = ((Area)AreaIdComboBox.SelectedItem).AreaId;
                    _workOrderArea.OperationId = ((Operation)OperationNameComboBox.SelectedItem).OperationId;

                    using (NpgsqlConnection npgsqlConnection = new NpgsqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["Postgresql"].ToString()))
                    {
                        using (NpgsqlCommand npgsqlCommand = new NpgsqlCommand($"CALL add_work_order({_workOrder.WorkOrderId}, {_workOrder.PauId}, {_workOrder.ReservationId}, {_workOrder.ReservationCompilationDate}, {_workOrder.EmployeeId}, {_workOrder.WorkOrderCompleteDate}, " +
                            $"{_workOrder.WorkOrderCloseDate}, {_workOrderArea.AreaId}, {_workOrderArea.OperationId}, {_workOrderArea.OperationStartDate}, {_workOrderArea.OperationStartTime}, {_workOrderArea.OperationEndDate}, {_workOrderArea.OperationEndTime});", npgsqlConnection))
                        {
                            npgsqlConnection.Open();
                            npgsqlCommand.ExecuteNonQuery();
                        }
                    }
                    //WorkOrdersDB.graduationContextMaster.Add(_workOrder);
                    //WorkOrdersDB.graduationContextMaster.Add(_workOrderArea);
                    WorkOrdersDB.graduationContextMaster.SaveChanges();
                    MessageBox.Show("Заказ-наряд успешно добавлен", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    _workOrderArea.WorkOrder.ReservationId = ((Reservation)ReservationIdComboBox.SelectedItem).ReservationId;
                    _workOrderArea.WorkOrder.PauId = ((Pau)PauNameComboBox.SelectedItem).PauId;
                    _workOrderArea.WorkOrder.EmployeeId = ((Employee)EmployeeSurnameComboBox.SelectedItem).EmployeeId;
                    if (WorkOrderCloseCheckBox.IsChecked == true)
                    {
                        _workOrderArea.WorkOrder.WorkOrderCloseDate = DateOnly.FromDateTime(DateTime.Now);
                    }

                    WorkOrdersDB.graduationContextMaster.SaveChanges();
                    MessageBox.Show("Заказ-наряд успешно изменен", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch when (String.IsNullOrWhiteSpace(WorkOrderIdTextBox.Text) || ReservationIdComboBox.SelectedItem == null
                        || PauNameComboBox.SelectedItem == null || String.IsNullOrWhiteSpace(WorkOrderCompleteDateTextBox.Text) || EmployeeSurnameComboBox.SelectedItem == null
                        || AreaIdComboBox.SelectedItem == null || OperationNameComboBox.SelectedItem == null || String.IsNullOrWhiteSpace(OperationStartDateTextBox.Text)
                        || String.IsNullOrWhiteSpace(OperationStartTimeTextBox.Text) || String.IsNullOrWhiteSpace(OperationEndDateTextBox.Text) || String.IsNullOrWhiteSpace(OperationEndTimeTextBox.Text))
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
                NavigationService.Navigate(new WorkOrdersPage());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void WorkOrderIdTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (Char.IsDigit(e.Text, 0)) return;
            e.Handled = true;
        }

        private void PauCountTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (Char.IsDigit(e.Text, 0)) return;
            e.Handled = true;
        }
    }
}
