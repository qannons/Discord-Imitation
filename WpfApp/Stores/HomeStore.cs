using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp.MVVM.Model;

namespace WpfApp.Stores
{
    public class HomeStore
    {
        public User? CurrentUser { get; set; }
    }
}
