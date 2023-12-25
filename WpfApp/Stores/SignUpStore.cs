using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using WpfApp.MVVM.Model;

namespace WpfApp.Stores
{
    public class SignUpStore
    {
        public User? CurrentUser{ get; set; }
    }
}
