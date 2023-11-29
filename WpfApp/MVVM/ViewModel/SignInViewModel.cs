using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Navigation;
using WpfApp.Core;

namespace WpfApp.MVVM.ViewModel
{
    internal class SignInViewModel : ObservableObject
    {
        private SubMainViewModel _mainViewModel;
        
        private readonly NavigationService navigationService;
        

        public SignInViewModel(SubMainViewModel pMainViewModel, Frame pFrame)
        {
            _mainViewModel = pMainViewModel;
            
        
        }

    }
}
