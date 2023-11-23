using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp.Core;

namespace WpfApp.MVVM.ViewModel
{
    public class SignUpViewModel : ObservableObject
    {
        //생성자
        public SignUpViewModel() 
        {
            // 연도, 월, 일에 대한 컬렉션 초기화
            Years = new ObservableCollection<string>(Enumerable.Range(1920, DateTime.Now.Year - 1919-3)
                .OrderByDescending(year=>year)
                .Select(year => year.ToString()));
            Months = new ObservableCollection<string>(Enumerable.Range(1, 12).Select(month => month.ToString()));
            Days = new ObservableCollection<string>(Enumerable.Range(1, 31).Select(day => day.ToString()));
        }

        //Public변수
        public ObservableCollection<string> Years { get; set; }
        public ObservableCollection<string> Months { get; set; }
        public ObservableCollection<string> Days { get; set; }
    }
}
