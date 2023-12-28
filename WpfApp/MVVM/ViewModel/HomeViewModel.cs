using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using WpfApp.Stores;

namespace WpfApp.MVVM.ViewModel
{
    [ObservableObject]
    public partial class HomeViewModel
    {
        private HomeStore _store;



        [ObservableProperty]
        private string _userName = "UserName";

        private void Init()
        {
            UserName = _store.CurrentUser.Nickname;
            
        }

        //생성자
        public HomeViewModel(HomeStore pHomeStore)
        {
            _store = pHomeStore;
            Init();
        }
    }
}
