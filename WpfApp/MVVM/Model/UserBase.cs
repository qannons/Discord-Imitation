using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp.MVVM.Model
{
    public class UserBase
    {
        public UInt32 ID { get; set; }
        public string Name { get; set; }
        public string Nickname { get; set; }
        //string status;
        //string picture;
    }

    public enum eSTATE
    {
        Online,
        Offline,
        Idle
    }

    public class MinimalUser : UserBase
    {
        public eSTATE Status { get; set; }
    }
}
