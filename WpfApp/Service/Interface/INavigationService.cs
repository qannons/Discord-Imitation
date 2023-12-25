using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp.Service.Interface
{
    public enum NaviType
    {
        SIGNIN,
        SIGNUP,
        HOME
    }

    public interface INavigationService
    {
        void Navigate(NaviType naviType);
    }
}
