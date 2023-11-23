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

namespace WpfApp.MVVM.View
{
    /// <summary>
    /// subSignUp.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class subSignUp : Page
    {
        public subSignUp()
        {
            InitializeComponent();
        }

        private void Hyperlink_personal_info(object sender, RequestNavigateEventArgs e)
        {

        }

        private void Hyperlink_terms(object sender, RequestNavigateEventArgs e)
        {

        }

        private void Hyperlink_signin(object sender, RequestNavigateEventArgs e)
        {
            Frame parentFrame = this.Parent as Frame;
            parentFrame.Navigate(new subSignIn());
        }
    }
}
