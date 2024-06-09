using Graduation.Models;
using Graduation.Models.Admin;
using Graduation.Pages.EmployeesPages;
using LiveCharts;
using LiveCharts.Wpf;
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

namespace Graduation.Pages.InfoPages
{
    public partial class ClosedWorkOrdersInfoPage : Page
    {
        private ClosedWorkOrder _closedWorkOrder;
        private List<ClosedWorkOrder> _closedWorkOrders;
        private List<Employee> _employees;
        private SeriesCollection _seriesCollection;

        public class ClosedWorkOrder
        {
            public string employeeSurname;
            public int closedWorkOrders;
        }

        public ClosedWorkOrdersInfoPage()
        {
            try
            {
                InitializeComponent();
                _closedWorkOrders = new List<ClosedWorkOrder>();
                _employees = WorkOrdersDB.graduationContextAdmin.Employees.Where(c => c.PositionId == 4).ToList();
                for (int i = 0; i < _employees.Count; i++)
                {
                    _closedWorkOrder = new ClosedWorkOrder()
                    {
                        employeeSurname = WorkOrdersDB.graduationContextAdmin.Employees.Where(c => c.PositionId == 4).ToList()[i].EmployeeSurname,
                        closedWorkOrders = WorkOrdersDB.graduationContextAdmin.WorkOrders.Where(c => c.Employee.EmployeeSurname == _employees[i].EmployeeSurname && c.WorkOrderCloseDate != null).ToList().Count
                    };
                    _closedWorkOrders.Add(_closedWorkOrder);
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
                _seriesCollection = new SeriesCollection();
                foreach (var item in _closedWorkOrders)
                {
                    _seriesCollection.Add(new PieSeries
                    {
                        Title = item.employeeSurname,
                        Values = new ChartValues<int> { item.closedWorkOrders },
                        DataLabels = true
                    });
                    ClosedWorkOrdersPieChart.Series = _seriesCollection;
                    ClosedWorkOrdersPieChart.LegendLocation = LegendLocation.Bottom;
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
                NavigationService.Navigate(new EmployeesPage());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
