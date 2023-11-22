using System;
using System.Collections.Generic;
using System.Diagnostics;
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

namespace WpfApp.View
{
    /// <summary>
    /// Login2.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class SignUp : Window
    {
        public SignUp()
        {
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

        private void Hyperlink_terms(object sender, RequestNavigateEventArgs e)
        {
            try
            {
                // 링크 클릭 시 기본 웹 브라우저로 열기
                Process.Start(new ProcessStartInfo(e.Uri.AbsoluteUri));
                e.Handled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error opening link: " + ex.Message);
            }
        }

        private void Hyperlink_personal_info(object sender, RequestNavigateEventArgs e)
        {
            try
            {
                // 링크 클릭 시 기본 웹 브라우저로 열기
                Process.Start(new ProcessStartInfo(e.Uri.AbsoluteUri));
                e.Handled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error opening link: " + ex.Message);
            }
        }

        private void Hyperlink_login(object sender, RequestNavigateEventArgs e)
        {

        }
    }
}
