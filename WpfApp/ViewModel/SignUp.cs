using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp.Core;

namespace WpfApp.ViewModel
{
    public class SignUp : ObservableObject
    {
        //생성자
        public SignUp() 
        {
            // 연도, 월, 일에 대한 컬렉션 초기화
            Years = new ObservableCollection<string>(Enumerable.Range(1900, DateTime.Now.Year - 1899).Select(year => year.ToString()));
            Months = new ObservableCollection<string>(Enumerable.Range(1, 12).Select(month => month.ToString()));
            Days = new ObservableCollection<string>(Enumerable.Range(1, 31).Select(day => day.ToString()));
        }

        //Public변수
        public ObservableCollection<string> Years { get; set; }
        public ObservableCollection<string> Months { get; set; }
        public ObservableCollection<string> Days { get; set; }
    }
}
