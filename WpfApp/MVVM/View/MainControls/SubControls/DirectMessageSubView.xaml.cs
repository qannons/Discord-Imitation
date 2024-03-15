using Microsoft.Win32;
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

namespace WpfApp.MVVM.View.MainControls.SubControls
{
    /// <summary>
    /// DirectMessageSubView.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class DirectMessageSubView : UserControl
    {
        public DirectMessageSubView()
        {
            InitializeComponent();
        }

        private void AttachFileButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "이미지 파일|*.jpg;*.jpeg;*.png;*.gif|모든 파일|*.*";

            if (openFileDialog.ShowDialog() == true)
            {
                // 선택한 파일의 경로 가져오기
                string selectedImagePath = openFileDialog.FileName;

                // 이미지 파일 경로를 TextBox에 표시
                UserInputBox.Text = selectedImagePath;

                // 여기에서 선택한 이미지 파일을 다른 곳에 저장하거나 사용하는 추가적인 로직을 추가할 수 있습니다.
            }
        }

    }
}
