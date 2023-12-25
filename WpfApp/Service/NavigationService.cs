using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp.MVVM.ViewModel;
using WpfApp.Service.Interface;
using WpfApp.Stores;

namespace WpfApp.Service
{
    public class NavigationService : INavigationService
    {
        private readonly MainNavigationStore _mainNavigationStore;
        private INotifyPropertyChanged CurrentViewModel
        {
            set => _mainNavigationStore.CurrentViewModel = value;
        }

        public NavigationService(MainNavigationStore mainNavigationStore)
        {
            this._mainNavigationStore = mainNavigationStore;
        }

        public void Navigate(NaviType naviType)
        {
            switch (naviType)
            {
                case NaviType.SIGNIN:
                    CurrentViewModel = (INotifyPropertyChanged)App.Current.Services.GetService(typeof(SignInViewModel))!;
                    break;
                case NaviType.SIGNUP:
                    CurrentViewModel = (INotifyPropertyChanged)App.Current.Services.GetService(typeof(SignUpViewModel))!;
                    break;
                case NaviType.HOME:
                    CurrentViewModel = (INotifyPropertyChanged)App.Current.Services.GetService(typeof(HomeViewModel))!;
                    break;
                default:
                    return;
            }
        }
    }
}
