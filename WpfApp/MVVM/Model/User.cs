using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using WpfApp.Model;

namespace WpfApp.MVVM.Model
{
    public class User : UserBase
    {
        //public int ID { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        //public string Name { get; set; }
        //public string Nickname { get; set; }
    }
}
