﻿using System;
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

namespace ChattingWindow
{
    /// <summary>
    /// DMWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class DMWindow : UserControl
    {
        public DMWindow()
        {
            InitializeComponent();
        }
        private void TextBoxKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                string inputText = inputTextBox.Text;
                outputListBox.Items.Add(inputText);
                inputTextBox.Clear();
                e.Handled = true; //이벤트 처리 완료를 설정하여 Enter 키 이벤트를 중복 처리하지 않도록 합니다.
            }
        }
    }
}