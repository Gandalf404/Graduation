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
        private Employee _employee;
        private WorkOrder _workOrder;
        private WorkOrderArea _workOrderArea;
        private bool _isCreating;
        public WorkOrderCreatePage(Employee employee)
        {
            try
            {
                InitializeComponent();
                _employee = employee;
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
                WorkOrderCloseCheckBox.Visibility = Visibility.Collapsed;
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

        public WorkOrderCreatePage(Employee employee, WorkOrderArea workOrderArea)
        {
            try
            {
                InitializeComponent();
                _employee = employee;
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
                //TODO: Возможно убрать.
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
                    if (!String.IsNullOrWhiteSpace(WorkOrderIdTextBox.Text)) { _workOrder.WorkOrderId = Convert.ToInt32(WorkOrderIdTextBox.Text); } else { throw new Exception(); }
                    if (ReservationIdComboBox.SelectedItem != null) { _workOrder.ReservationId = ((Reservation)ReservationIdComboBox.SelectedItem).ReservationId; } else { throw new Exception(); }
                    _workOrder.WorkOrderCompilationDate = DateOnly.FromDateTime(DateTime.Now);
                    if (PauNameComboBox.SelectedItem != null) { _workOrder.PauId = ((Pau)PauNameComboBox.SelectedItem).PauId; } else { throw new Exception(); }
                    _workOrder.WorkOrderCompleteDate = DateOnly.Parse(WorkOrderCompleteDateTextBox.Text);
                    if (DateOnly.Parse(WorkOrderCompleteDateTextBox.Text) > DateOnly.FromDateTime(DateTime.Now))
                    {
                        throw new Exception("Дата выполнения заказ-наряда не может быть позднее текущей даты");
                    }
                    if (DateOnly.Parse(OperationEndDateTextBox.Text) > DateOnly.FromDateTime(DateTime.Now))
                    {
                        throw new Exception("Дата начала операции не может быть позднее текущей даты");
                    }
                    if (DateOnly.Parse(OperationEndDateTextBox.Text) < DateOnly.Parse(OperationStartDateTextBox.Text))
                    {
                        throw new Exception("Дата завершения операции не может быть раньше даты начала операции");
                    }
                    if (DateOnly.Parse(OperationStartDateTextBox.Text) > DateOnly.Parse(OperationEndDateTextBox.Text))
                    {
                        throw new Exception("Дата начала операции не может быть позднее даты окончания операции");
                    }
                    if (TimeOnly.Parse(OperationStartTimeTextBox.Text) < new TimeOnly(8, 00))
                    {
                        throw new Exception("Время начала операции не может быть раньше времени начала работы предприятия");
                    }
                    if (TimeOnly.Parse(OperationStartTimeTextBox.Text) > new TimeOnly(17, 00))
                    {
                        throw new Exception("Время начала операции не может быть позднее времени закрытия предприятия");
                    }
                    if (TimeOnly.Parse(OperationEndTimeTextBox.Text) < new TimeOnly(8, 00))
                    {
                        throw new Exception("Время завершения операции не может быть раньше времени начала работы предприятия");
                    }
                    if (TimeOnly.Parse(OperationEndTimeTextBox.Text) > new TimeOnly(17, 00))
                    {
                        throw new Exception("Время завершения операции не может быть позднее времени закрытия предприятия");
                    }
                    _workOrder.EmployeeId = _employee.EmployeeId;
                    if (ReservationIdComboBox.SelectedItem != null) { _workOrder.ReservationCompilationDate = ((Reservation)ReservationIdComboBox.SelectedItem).ReservationCompilationDate; } else { throw new Exception(); }
                    if (!String.IsNullOrWhiteSpace(WorkOrderIdTextBox.Text)) { _workOrderArea.WorkOrderId = Convert.ToInt32(WorkOrderIdTextBox.Text); } else { throw new Exception(); }
                    _workOrderArea.WorkOrderCompilationDate = DateOnly.FromDateTime(DateTime.Now);
                    if (AreaIdComboBox.SelectedItem != null) { _workOrderArea.AreaId = ((Area)AreaIdComboBox.SelectedItem).AreaId; } else { throw new Exception(); }
                    if (OperationNameComboBox.SelectedItem != null) { _workOrderArea.OperationId = ((Operation)OperationNameComboBox.SelectedItem).OperationId; } else { throw new Exception(); }

                    WorkOrdersDB.graduationContextMaster.Add(_workOrder);
                    WorkOrdersDB.graduationContextMaster.Add(_workOrderArea);
                    WorkOrdersDB.graduationContextMaster.SaveChanges();
                    MessageBox.Show("Заказ-наряд успешно добавлен", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    if (ReservationIdComboBox.SelectedItem != null) { _workOrderArea.WorkOrder.ReservationId = ((Reservation)ReservationIdComboBox.SelectedItem).ReservationId; } else { throw new Exception(); }
                    if (PauNameComboBox.SelectedItem != null) { _workOrderArea.WorkOrder.PauId = ((Pau)PauNameComboBox.SelectedItem).PauId; } else { throw new Exception(); }
                    _workOrderArea.WorkOrder.WorkOrderCompleteDate = DateOnly.Parse(WorkOrderCompleteDateTextBox.Text);
                    if (DateOnly.Parse(WorkOrderCompleteDateTextBox.Text) > DateOnly.FromDateTime(DateTime.Now))
                    {
                        throw new Exception("Дата выполнения заказ-наряда не может быть позднее текущей даты");
                    }
                    if (DateOnly.Parse(OperationEndDateTextBox.Text) > DateOnly.FromDateTime(DateTime.Now))
                    {
                        throw new Exception("Дата начала операции не может быть позднее текущей даты");
                    }
                    if (DateOnly.Parse(OperationEndDateTextBox.Text) < DateOnly.Parse(OperationStartDateTextBox.Text))
                    {
                        throw new Exception("Дата завершения операции не может быть раньше даты начала операции");
                    }
                    if (DateOnly.Parse(OperationStartDateTextBox.Text) > DateOnly.Parse(OperationEndDateTextBox.Text))
                    {
                        throw new Exception("Дата начала операции не может быть позднее даты окончания операции");
                    }
                    if (TimeOnly.Parse(OperationStartTimeTextBox.Text) < new TimeOnly(8, 00))
                    {
                        throw new Exception("Время начала операции не может быть раньше времени начала работы предприятия");
                    }
                    if (TimeOnly.Parse(OperationStartTimeTextBox.Text) > new TimeOnly(17, 00))
                    {
                        throw new Exception("Время начала операции не может быть позднее времени закрытия предприятия");
                    }
                    if (TimeOnly.Parse(OperationEndTimeTextBox.Text) < new TimeOnly(8, 00))
                    {
                        throw new Exception("Время завершения операции не может быть раньше времени начала работы предприятия");
                    }
                    if (TimeOnly.Parse(OperationEndTimeTextBox.Text) > new TimeOnly(17, 00))
                    {
                        throw new Exception("Время завершения операции не может быть позднее времени закрытия предприятия");
                    }
                    _workOrderArea.WorkOrder.EmployeeId = _employee.EmployeeId;
                    if (WorkOrderCloseCheckBox.IsChecked == true)
                    {
                        _workOrderArea.WorkOrder.WorkOrderCloseDate = DateOnly.FromDateTime(DateTime.Now);
                    }

                    WorkOrdersDB.graduationContextMaster.SaveChanges();
                    MessageBox.Show("Заказ-наряд успешно изменен", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch when (String.IsNullOrWhiteSpace(WorkOrderIdTextBox.Text) || ReservationIdComboBox.SelectedItem == null || PauNameComboBox.SelectedItem == null 
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
                NavigationService.Navigate(new WorkOrdersPage(_employee));
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
    }
}
