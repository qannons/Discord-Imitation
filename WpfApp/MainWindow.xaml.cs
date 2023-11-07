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
using WpfApp.Model;

namespace WpfApp
{
    public partial class MainWindow : Window
    {
        public MainWindow()
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

        private void EnterDown(object sender, KeyEventArgs e)
        {
            //if (e.Key == Key.Enter)
            //{
            //    string inputText = inputTextBox.Text;
            //    outputListBox.Items.Add(inputText);
            //    inputTextBox.Clear();
            //    e.Handled = true; //이벤트 처리 완료를 설정하여 Enter 키 이벤트를 중복 처리하지 않도록 합니다.
            //}
        }

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ChatRoom selectedRoom = e.AddedItems[0] as ChatRoom;
            UserNameLabel.Content = selectedRoom.RoomName;
        }

        private void InputTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void ListView_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
