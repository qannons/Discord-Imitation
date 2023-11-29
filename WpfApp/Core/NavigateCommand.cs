using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using WpfApp.MVVM.ViewModel;

namespace WpfApp.Core
{
    internal class NavigateCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;
        private readonly SubMainViewModel _viewModel;

        public NavigateCommand(SubMainViewModel pViewModel)
        {
            _viewModel = pViewModel;
        }

        public bool CanExecute(object parameter)
        {
            // 필요한 경우, 커맨드 실행 가능 여부를 확인하는 로직을 작성합니다.
            return true;
        }

        public virtual void Execute(object parameter)
        {
            string pageName = parameter as string;
            if (!string.IsNullOrEmpty(pageName))
            {
                _viewModel.NavigateToPage(pageName);
            }
        }
    }
}
