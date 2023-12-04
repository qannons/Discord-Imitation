using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Navigation;
using WpfApp.Core;
using WpfApp.Model;
using WpfApp.MVVM.View;

namespace WpfApp.MVVM.ViewModel
{
    internal class SubMainViewModel : ObservableObject
    {
        private string _currentPage;
        public string CurrentPage
        {
            get { return _currentPage; }
            set
            {
                if (_currentPage != value)
                {
                    _currentPage = value;
                    OnPropertyChanged(nameof(CurrentPage));
                }
            }
        }

        public SubMainViewModel()
        {
            // 초기 페이지 설정
            CurrentPage = "/MVVM/View/subSignIn.xaml";
            
            
        }


    }
}
