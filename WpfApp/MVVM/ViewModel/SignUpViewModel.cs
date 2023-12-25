using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp.Core;
using WpfApp.Service.Interface;
using WpfApp.Stores;

namespace WpfApp.MVVM.ViewModel
{
    [ObservableObject]
    public partial class SignUpViewModel
    {
        private readonly INavigationService _navigationService;


        //생성자
        public SignUpViewModel(INavigationService navigationService) 
        {
            _navigationService = navigationService;
            // 연도, 월, 일에 대한 컬렉션 초기화
            Years = new ObservableCollection<string>(Enumerable.Range(1920, DateTime.Now.Year - 1919-3)
                .OrderByDescending(year=>year)
                .Select(year => year.ToString()));
            Months = new ObservableCollection<string>(Enumerable.Range(1, 12).Select(month => month.ToString()));
            Days = new ObservableCollection<string>(Enumerable.Range(1, 31).Select(day => day.ToString()));
        }

        [RelayCommand]
        private void ToSignIn(object _)
        {
            _navigationService.Navigate(NaviType.SIGNIN);
        }

        //Public변수
        public ObservableCollection<string> Years { get; set; }
        public ObservableCollection<string> Months { get; set; }
        public ObservableCollection<string> Days { get; set; }
    }
}
