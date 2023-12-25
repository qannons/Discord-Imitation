using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp.Service.Interface;

namespace WpfApp.Service
{
    class TestService : ITestService
    {
        public string GetString()
        {
            return "test!";
        }
    }
}
