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
using System.Windows.Shapes;

namespace WpfApp.MVVM.View
{
    /// <summary>
    /// Login.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class Login : Window
    {
        public Login()
        {
            InitializeComponent();
        }

        private void textEmailBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if(string.IsNullOrEmpty(textEmailBox.Text) && textEmailBox.Text.Length>0)
            {
                textEmail.Visibility = Visibility.Collapsed;
            }
            else
            {
                textEmail.Visibility = Visibility.Visible;
            }
            
        }

        private void textEmail_MouseDown(object sender, MouseButtonEventArgs e)
        {
            textEmail.Focus();
        }

        private void textPassword_MouseDown(object sender, MouseButtonEventArgs e)
        {
            textPassword.Focus();
        }

        private void textPasswordBox_TextChanged(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(textPasswordBox.Password) && textPasswordBox.Password.Length > 0)
            {
                textPasswordBox.Visibility = Visibility.Collapsed;
            }
            else
            {
                textPasswordBox.Visibility = Visibility.Visible;
            }
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if(e.ChangedButton == MouseButton.Left) 
            {
                this.DragMove();
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if(string.IsNullOrEmpty(textEmailBox.Text) && string.IsNullOrEmpty(textPasswordBox.Password)) 
            {
                MessageBox.Show("Successfully Signed In");
            }
        }

        private void Image_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
