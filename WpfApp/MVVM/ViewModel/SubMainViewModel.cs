using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using WpfApp.Core;

namespace WpfApp.MVVM.ViewModel
{
    internal class SubMainViewModel : ObservableObject
    {
        public SignInViewModel signInViewModel { get; }

        public SubMainViewModel()
        {
            signInViewModel = new SignInViewModel(this);

        }
        public void testFunc()
        {
            
            return;
        }

    }
}
