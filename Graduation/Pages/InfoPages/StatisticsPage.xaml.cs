using Graduation.Models;
using Graduation.Models.Admin;
using Graduation.Pages.EmployeesPages;
using LiveCharts;
using LiveCharts.Wpf;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace Graduation.Pages.InfoPages
{
    public partial class StatisticsPage : Page
    {
        private ClosedWorkOrder _closedWorkOrder;
        private CompilatedInvoice _compilatedInvoice;
        private List<ClosedWorkOrder> _closedWorkOrders;
        private List<CompilatedInvoice> _compilatedInvoices;
        private List<Employee> _employees;
        private SeriesCollection _seriesCollection;

        public class ClosedWorkOrder
        {
            public string employeeSurname;
            public int closedWorkOrder;
        }

        public class CompilatedInvoice : ClosedWorkOrder
        {
            public int compilatedInvoice;
        }

        public StatisticsPage()
        {
            try
            {
                InitializeComponent();
                StatisticComboBox.Items.Add("Без статистики");
                StatisticComboBox.Items.Add("Кол-во закрытых заказ-нарядов");
                StatisticComboBox.Items.Add("Кол-во составленных приемо-сдаточных накладных");
                _closedWorkOrders = new List<ClosedWorkOrder>();
                _compilatedInvoices = new List<CompilatedInvoice>();
                _seriesCollection = new SeriesCollection();
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
                NavigationService.Navigate(new EmployeesPage());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void StatisticComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                switch (StatisticComboBox.SelectedIndex)
                {
                    case 0:
                        StatisticPieChart.Visibility = Visibility.Collapsed;
                        break;
                    case 1:
                        if (_employees?.Count > 0) { _employees.Clear(); }
                        if (_seriesCollection?.Count > 0) { _seriesCollection.Clear(); }
                        if (_closedWorkOrders?.Count > 0) { _closedWorkOrders.Clear(); }
                        ClosedWorkOrdersByEmployees();
                        break;
                    case 2:
                        if (_employees?.Count > 0) { _employees.Clear(); }
                        if (_seriesCollection?.Count > 0) { _seriesCollection.Clear(); }
                        if (_compilatedInvoices?.Count > 0) { _compilatedInvoices.Clear(); }
                        CompilatedInvoicesByEmployees();
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ClosedWorkOrdersByEmployees()
        {
            try
            {
                _employees = WorkOrdersDB.graduationContextAdmin.Employees.Where(c => c.PositionId == 4).ToList();
                for (int i = 0; i < _employees.Count; i++)
                {
                    _closedWorkOrder = new ClosedWorkOrder()
                    {
                        employeeSurname = WorkOrdersDB.graduationContextAdmin.Employees.Where(c => c.PositionId == 4).ToList()[i].EmployeeSurname,
                        closedWorkOrder = WorkOrdersDB.graduationContextAdmin.WorkOrders.Where(c => c.Employee.EmployeeSurname == _employees[i].EmployeeSurname && c.WorkOrderCloseDate != null).ToList().Count
                    };
                    _closedWorkOrders.Add(_closedWorkOrder);
                }
                for (int i = 0; i < _closedWorkOrders.Count; i++)
                {
                    _seriesCollection.Add(new PieSeries
                    {
                        Title = _closedWorkOrders[i].employeeSurname,
                        Values = new ChartValues<int> { _closedWorkOrders[i].closedWorkOrder },
                        DataLabels = true
                    });
                    StatisticPieChart.Series = _seriesCollection;
                    StatisticPieChart.LegendLocation = LegendLocation.Bottom;
                    StatisticPieChart.Visibility = Visibility.Visible;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void CompilatedInvoicesByEmployees()
        {
            try
            {
                _employees = WorkOrdersDB.graduationContextAdmin.Employees.Where(c => c.PositionId == 4).ToList();
                for (int i = 0; i < _employees.Count; i++)
                {
                    _compilatedInvoice = new CompilatedInvoice()
                    {
                        employeeSurname = WorkOrdersDB.graduationContextAdmin.Employees.Where(c => c.PositionId == 4).ToList()[i].EmployeeSurname,
                        compilatedInvoice = WorkOrdersDB.graduationContextAdmin.Invoices.Where(c => c.WorkOrder.Employee.EmployeeSurname == _employees[i].EmployeeSurname && c.InvoiceCompilationDate != null).ToList().Count
                    };
                    _compilatedInvoices.Add(_compilatedInvoice);
                }
                for (int i = 0; i < _compilatedInvoices.Count; i++)
                {
                    _seriesCollection.Add(new PieSeries
                    {
                        Title = _compilatedInvoices[i].employeeSurname,
                        Values = new ChartValues<int> { _compilatedInvoices[i].compilatedInvoice },
                        DataLabels = true
                    });
                }
                StatisticPieChart.Series = _seriesCollection;
                StatisticPieChart.LegendLocation = LegendLocation.Bottom;
                StatisticPieChart.Visibility = Visibility.Visible;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
