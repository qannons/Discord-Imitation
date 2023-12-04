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
using WpfApp.View;

namespace WpfApp.MVVM.View
{
    public partial class subSignIn : Page
    {
        static private subSignUp _subSignUp = new subSignUp();

        public subSignIn()
        {
            InitializeComponent();
            //subSignUp _subSignUp = new subSignUp();
        }



        private void Hyperlink_signup(object sender, RoutedEventArgs e)
        {
            if(_subSignUp != null) 
            {
                NavigationService.Navigate(_subSignUp);

            }
            //subSignUp _subSignUp = new subSignUp();
        }
    }
}
