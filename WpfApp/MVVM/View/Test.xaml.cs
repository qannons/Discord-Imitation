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
using WpfApp.Core.Repo;
using WpfApp.MVVM.ViewModel;

namespace WpfApp.MVVM.View
{
    /// <summary>
    /// Login3.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class Test : Window
    {
        public Test()
        {
            InitializeComponent();

            var testRepo = new TestRepo();
            DataContext = new testViewModel(testRepo);
        }
    }
}
