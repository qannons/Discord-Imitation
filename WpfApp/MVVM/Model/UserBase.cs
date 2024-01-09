using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp.MVVM.Model
{
    public class UserBase
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Nickname { get; set; }
        //string status;
        //string picture;
    }

    public enum eSTATUS
    {
        Online,
        Offline,
        Idle
    }

    public class MinimalUser : UserBase
    {
        public  eSTATUS Status { get; set; }
    }
}
