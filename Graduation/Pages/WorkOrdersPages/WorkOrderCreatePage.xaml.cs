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

namespace Graduation.Pages.WorkOrdersPages
{
    public partial class WorkOrderCreatePage : Page
    {
        //private WorkOrder _workOrder;
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
                foreach (var item in GraduationDB.graduationContext.Reservations)
                {
                    ReservationIdComboBox.Items.Add(item);
                }
                foreach (var item in GraduationDB.graduationContext.Paus)
                {
                    PauNameComboBox.Items.Add(item);
                }
                foreach (var item in GraduationDB.graduationContext.Employees)
                {
                    EmployeeSurnameComboBox.Items.Add(item);
                }
                foreach (var item in GraduationDB.graduationContext.Areas)
                {
                    AreaIdComboBox.Items.Add(item);
                }
                foreach (var item in GraduationDB.graduationContext.Operations)
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
                foreach (var item in GraduationDB.graduationContext.Reservations)
                {
                    ReservationIdComboBox.Items.Add(item);
                }
                foreach (var item in GraduationDB.graduationContext.Paus)
                {
                    PauNameComboBox.Items.Add(item);
                }
                foreach (var item in GraduationDB.graduationContext.Employees)
                {
                    EmployeeSurnameComboBox.Items.Add(item);
                }
                foreach (var item in GraduationDB.graduationContext.Areas)
                {
                    AreaIdComboBox.Items.Add(item);
                }
                foreach (var item in GraduationDB.graduationContext.Operations)
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
                    //TODO: Починить ReservationId.
                    _workOrder.WorkOrderId = Convert.ToInt32(WorkOrderIdTextBox.Text);
                    _workOrder.ReservationId = ((Reservation)ReservationIdComboBox.SelectedItem).ReservationId;
                    _workOrder.WorkOrderCompilationDate = DateOnly.FromDateTime(DateTime.Now);
                    _workOrder.PauId = ((Pau)PauNameComboBox.SelectedItem).PauId;
                    _workOrder.PauCount = ((Pau)PauNameComboBox.SelectedItem).PauCount;
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

                    if (Convert.ToInt32(PauCountTextBox.Text) > Convert.ToInt32(ReservationCountTextBlock.Text))
                    {
                        throw new Exception("Количество ДСЕ не может быть больше количества ДСЕ в заказе");
                    }

                    GraduationDB.graduationContext.Add(_workOrder);
                    GraduationDB.graduationContext.Add(_workOrderArea);
                    GraduationDB.graduationContext.SaveChanges();
                    MessageBox.Show("Заказ-наряд успешно добавлен", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    _workOrderArea.WorkOrder.ReservationId = ((Reservation)ReservationIdComboBox.SelectedItem).ReservationId;
                    _workOrderArea.WorkOrder.PauId = ((Pau)PauNameComboBox.SelectedItem).PauId;
                    _workOrderArea.WorkOrder.PauCount = Convert.ToInt32(PauCountTextBox.Text);
                    _workOrderArea.WorkOrder.EmployeeId = ((Employee)EmployeeSurnameComboBox.SelectedItem).EmployeeId;
                    if (WorkOrderCloseCheckBox.IsChecked == true)
                    {
                        _workOrderArea.WorkOrder.WorkOrderCloseDate = DateOnly.FromDateTime(DateTime.Now);
                    }

                    if (Convert.ToInt32(PauCountTextBox.Text) > Convert.ToInt32(ReservationCountTextBlock.Text))
                    {
                        throw new Exception("Количество ДСЕ не может быть больше количества ДСЕ в заказе");
                    }

                    GraduationDB.graduationContext.SaveChanges();
                    MessageBox.Show("Заказ-наряд успешно изменен", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
                }
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
