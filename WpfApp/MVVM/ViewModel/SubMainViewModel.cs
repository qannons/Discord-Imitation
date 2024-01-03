using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Navigation;
using WpfApp.Core;
using WpfApp.Model;
using WpfApp.MVVM.View;
using WpfApp.Service.Interface;
using WpfApp.Stores;

namespace WpfApp.MVVM.ViewModel
{
    internal class SubMainViewModel : MyObservableObject
    {
        private readonly MainNavigationStore _mainNavigationStore;
        private INotifyPropertyChanged? _currentViewModel;

        private void CurrentViewModelChanged()
        {
            CurrentViewModel = _mainNavigationStore.CurrentViewModel;
        }

        public SubMainViewModel(MainNavigationStore mainNavigationStore, INavigationService navigationService)
        {
            _mainNavigationStore = mainNavigationStore;
            _mainNavigationStore.CurrentViewModelChanged += CurrentViewModelChanged;
            navigationService.Navigate(NaviType.SIGNIN);

            //            client = new ServerCommunicationService();
            //            client.Connect("127.0.0.1", 7777);
        }

        public INotifyPropertyChanged? CurrentViewModel
        {
            get { return _currentViewModel; }
            set
            {
                if (_currentViewModel != value)
                {
                    _currentViewModel = value;
                    OnPropertyChanged(nameof(CurrentViewModel));
                }
            }
        }

    }
}
