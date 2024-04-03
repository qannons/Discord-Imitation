using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Org.BouncyCastle.Security;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Navigation;
using System.Windows.Xps.Serialization;
using WpfApp.Core;
using WpfApp.MVVM.Model;
using WpfApp.MVVM.View;
using WpfApp.Service;
using WpfApp.Service.Interface;
using WpfApp.Stores;

namespace WpfApp.MVVM.ViewModel
{
    [ObservableObject]
    internal partial class SignInViewModel
    {
        private readonly INavigationService _navigationService;

        private HomeStore _homeStore;

        [ObservableProperty]
        private string _email = default;

        [ObservableProperty]
        private string _emailTextBlock = "이메일 또는 전화번호";

        [ObservableProperty]
        private SolidColorBrush _colorBrush = new SolidColorBrush(Colors.DarkGray);
        
        [RelayCommand]
        private async Task LoginBtnAsync(PasswordBox pPwd)
        {
            User? user = await LoginServerCommunicationService.LoginAsyncOrNull(Email, pPwd.Password);
            if (user == null)
            {
                EmailTextBlock = "이메일 또는 전화번호-유효하지 않은 아이디 또는 비밀번호입니다.";
                ColorBrush.Color = Colors.Red;
                return;
            }
            else
            {
                _homeStore.CurrentUser = new User { ID = user.ID, Name = user.Name, Nickname = user.Nickname};

                _navigationService.Navigate(NaviType.HOME);
            }
        }

        [RelayCommand]
        private void ToSignup(object _)
        {
            _navigationService.Navigate(NaviType.SIGNUP);
        }

        public SignInViewModel( INavigationService navigationService, HomeStore pHomeStore)
        {
            _navigationService = navigationService;
            _homeStore = pHomeStore;
        }
    }
}
