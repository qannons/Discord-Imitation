using ChattingWindow;
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

namespace WpfApp
{
    public partial class MainWindow : Window
    {
        //public MainWindow()
        //{
        //    InitializeComponent();
        //}

        public MainWindow()
        {
            InitializeComponent();
            NavigateToHomePage();
        }

        private void NavigateToHomePage()
        {
            DMWindow dmWindow = new DMWindow();
            mainFrame.Navigate(dmWindow);
        }

        private void NavigateToAboutPage()
        {
            ServerWindow serverWindow = new ServerWindow();
            mainFrame.Navigate(serverWindow);
        }

        private void HomeButton_Click(object sender, RoutedEventArgs e)
        {
            NavigateToHomePage();
        }

        private void AboutButton_Click(object sender, RoutedEventArgs e)
        {
            NavigateToAboutPage();
        }
    }
}
