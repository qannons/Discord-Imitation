using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using WpfApp.Core;
using WpfApp.Database.Repo;
using WpfApp.MVVM.Model;
using WpfApp.Service.Interface;
using WpfApp.Stores;

namespace WpfApp.MVVM.ViewModel
{

    [ObservableObject]
    public partial class SignUpViewModel
    {
        private IUserRepo _userRepo;
        private readonly INavigationService _navigationService;
        private HomeStore _homeStore;

        [ObservableProperty]
        private string _emailTextBlock = "이메일 또는 전화번호";
        [ObservableProperty]
        private SolidColorBrush _emailBrush = new SolidColorBrush(Colors.DarkGray);

        [ObservableProperty]
        private string _email = default;

        [ObservableProperty]
        private string _nickname = default;

        [ObservableProperty]
        private string _pwd = default;

        [ObservableProperty]
        private string _name = default;

        //[ObservableProperty]
        //private DateTime _birth = default;

        //Public변수
        public ObservableCollection<string> Years { get; set; }
        public ObservableCollection<string> Months { get; set; }
        public ObservableCollection<string> Days { get; set; }

        //생성자
        public SignUpViewModel(IUserRepo userRepository, INavigationService navigationService, HomeStore pSignUpStore) 
        {
            _userRepo = userRepository;
            _navigationService = navigationService;
            _homeStore = pSignUpStore;

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

        [RelayCommand]
        private void SignUpBtn(PasswordBox pPwd)
        {
            if (_userRepo.IsExistEmail(Email, pPwd.Password))
            {
                EmailTextBlock = "이메일-이미 존재하는 이메일입니다.";
                EmailBrush.Color = Colors.Red;
                return;
                
            }
            else
            {
                _userRepo.Insert(new User { Email = Email, Password = pPwd.Password, Name = Name, Nickname = Nickname });
                _homeStore.CurrentUser = new Model.User() { Email = Email, Nickname = Nickname };
                _navigationService.Navigate(NaviType.HOME);

            }

        }
    }
}
