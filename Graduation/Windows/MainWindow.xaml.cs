using Graduation.Pages;
using System.Windows;

namespace Graduation
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            MainFrame.Navigate(new AuthPage());
        }
    }
}