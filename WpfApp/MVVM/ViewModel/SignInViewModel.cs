using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Navigation;
using WpfApp.Core;

namespace WpfApp.MVVM.ViewModel
{
    internal class SignInViewModel : ObservableObject
    {
        private SubMainViewModel _mainViewModel;
        public RelayCommand ButtonClickCommand { get; }

        public SignInViewModel(SubMainViewModel pMainViewModel)
        {
            _mainViewModel = pMainViewModel;
            ButtonClickCommand = new RelayCommand(Button_Click);
        }

        public void Button_Click(object parameter)
        {
            _mainViewModel.testFunc();
           
        }
    }
}
