//using System;
//using System.Collections.Generic;
//using System.ComponentModel;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using WpfApp.MVVM.ViewModel;
//using WpfApp.Service.Interface;
//using WpfApp.Stores;

//namespace WpfApp.Service
//{
//    public class SubNavigation : INavigationService
//    {
//        private readonly MainNavigationStore _mainNavigationStore;
//        private INotifyPropertyChanged CurrentSubViewModel
//        {
//            set => _mainNavigationStore.CurrentViewModel = value;
//        }

//        public SubNavigation(MainNavigationStore mainNavigationStore)
//        {
//            this._mainNavigationStore = mainNavigationStore;
//        }

//        public void Navigate(NaviType naviType)
//        {
//            switch (naviType)
//            {
//                case NaviType.SubFriend:
//                    CurrentSubViewModel = (INotifyPropertyChanged)App.Current.Services.GetService(typeof(HomeViewModel))!;
//                    break;

//                default:
//                    return;
//            }
//        }
//    }
//}
