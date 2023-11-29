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

namespace WpfApp.MVVM.ViewModel
{
    internal class SubMainViewModel : ObservableObject
    {
        private readonly NavigationService navigationService;

        public NavigateCommand navigateCommand { get; }

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
            _currentPage = "/MVVM/View/subSignIn.xaml"; // 초기 페이지 설정
            navigateCommand = new NavigateCommand(this);

            

        }

        public void NavigateToPage(string pPageName)
        {
            CurrentPage = pPageName;
        }

    }
}
