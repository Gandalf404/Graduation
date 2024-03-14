using Graduation.Models;
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

namespace Graduation.Pages.WorkOrderPages
{
    /// <summary>
    /// Логика взаимодействия для WorkOrdersPage.xaml
    /// </summary>
    public partial class WorkOrdersPage : Page
    {
        List<WorkOrder> workOrders;

        public WorkOrdersPage()
        {
            try
            {
                InitializeComponent();
                workOrders = GraduationDB.graduationContext.WorkOrders.ToList();
                WorkOrdersDataGrid.ItemsSource = workOrders;
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
                NavigationService.GoBack();
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
                workOrders = GraduationDB.graduationContext.WorkOrders.ToList();
                WorkOrdersDataGrid.ItemsSource = workOrders;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void WorkOrdersViewItem_Click(object sender, RoutedEventArgs e)
        {

        }

        private void AcceptNotesViewItem_Click(object sender, RoutedEventArgs e)
        {

        }

        private void PAUViewItem_Click(object sender, RoutedEventArgs e)
        {

        }

        private void WorkOrdersEmployeeViewItem_Click(object sender, RoutedEventArgs e)
        {

        }

        private void AcceptNotesEmployeeViewItem_Click(object sender, RoutedEventArgs e)
        {

        }

        private void AcceptNoteAddViewItem_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
