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
using System.Windows.Navigation;
using WpfApp.Core;
using WpfApp.MVVM.Model;
using WpfApp.Service;
using WpfApp.Service.Interface;
using WpfApp.Stores;

namespace WpfApp.MVVM.ViewModel
{

    [ObservableObject]
    public partial class SignUpViewModel
    {
        private readonly INavigationService _navigationService;
        private HomeStore _homeStore;

        //TextBox모음
        [ObservableProperty]
        private string _emailTextBlock = "이메일 또는 전화번호";
        [ObservableProperty]
        private SolidColorBrush _emailBrush = new SolidColorBrush(Colors.DarkGray);

        [ObservableProperty]
        private string _nameTextBlock = "사용자명";
        [ObservableProperty]
        private SolidColorBrush _nameBrush = new SolidColorBrush(Colors.DarkGray);

        [ObservableProperty]
        private string _pwdTextBlock = "비밀번호";
        [ObservableProperty]
        private SolidColorBrush _pwdBrush = new SolidColorBrush(Colors.DarkGray);

        [ObservableProperty]
        private User _signUpUser = default;

        //Public변수
        public ObservableCollection<string> Years { get; set; }
        public ObservableCollection<string> Months { get; set; }
        public ObservableCollection<string> Days { get; set; }
        public IAsyncRelayCommand SignUpBtnAsyncCommand { get; }
        //생성자
        public SignUpViewModel(INavigationService navigationService, HomeStore pSignUpStore) 
        {
            SignUpBtnAsyncCommand = new AsyncRelayCommand<PasswordBox>(SignUpBtnAsync);
            _signUpUser = new User();
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
        
        private async Task SignUpBtnAsync(PasswordBox pPwd)
        {
            if (CheckSignUpField(pPwd.Password) == false)
                return;

            _signUpUser.Password = pPwd.Password;
            bool flag = await LoginServerCommunicationService.SignUpAsync(_signUpUser);
            if (flag)
            {
                _homeStore.CurrentUser = new User() { Email = _signUpUser.Email, Nickname = _signUpUser.Nickname };
                _navigationService.Navigate(NaviType.HOME);           
            }
            else
            {
                EmailTextBlock = "이메일-이미 존재하는 이메일입니다.";
                EmailBrush.Color = Colors.Red;
            }
        }

        private bool CheckSignUpField(string pPwd)
        {
            bool ret = true;
            if (SignUpUser.Email == null || SignUpUser.Email == "")
            {
                EmailTextBlock = "이메일-필수 요건";
                EmailBrush.Color = Colors.Red;
                ret = false;
            }
            if(SignUpUser.Name == null || SignUpUser.Name == "")
            {
                NameTextBlock = "사용자명-필수 요건";
                NameBrush.Color = Colors.Red;
                ret = false;
            }
            if(pPwd == null || pPwd == "")
            {
                PwdTextBlock = "비밀번호-필수요건";
                PwdBrush.Color = Colors.Red;
                ret = false;
            }
            return ret;
        }
    }
}
