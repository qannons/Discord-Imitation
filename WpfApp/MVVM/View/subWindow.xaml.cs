using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using System.Windows.Shapes;

namespace WpfApp.MVVM.View
{
    /// <summary>
    /// subWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class subWindow : Window
    {
        public Frame myFrame;

        public subWindow()
        {
            myFrame = mainFrame;
            InitializeComponent();
            
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

        private void MaximizeWindow(object sender, RoutedEventArgs e)
        {
            Application.Current.MainWindow.WindowState =
                   (Application.Current.MainWindow.WindowState != WindowState.Maximized)
                   ? WindowState.Maximized : WindowState.Normal;
        }

        private void CloseWindow(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void MinimizeWindow(object sender, RoutedEventArgs e)
        {
            Application.Current.MainWindow.WindowState = WindowState.Minimized;
        }

    }
}
