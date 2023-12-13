using MySql.Data.MySqlClient;
using MySqlX.XDevAPI;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using WpfApp.Core;
using WpfApp.Core.Repo;
using WpfApp.Model;

namespace WpfApp.MVVM.ViewModel
{
    class testViewModel : ObservableObject
    {
        private MySqlConnection _conn = default!;
        //private IAccountRepository _accountRepository;
        private List<testModel> _tests= default!;
        public List<testModel> Tests 
        {
            get { return _tests; } 
            set 
            {
                if(_tests != value)
                {
                    _tests = value; 
                    OnPropertyChanged(nameof(Tests)); 
                }
            }
        }
        private TestRepo _testRepo;

        //생성자
        public testViewModel(TestRepo itestRepo)
        {
            _testRepo = itestRepo;
        }

        private void Connection(object _)
        {
            
        }


        private void Insert(object _)
        {
            testModel t = new()
            {
                pk = 25,
                npk = "cannons"
            };
            _testRepo.Insert(t);
        }

        private void Update(object _)
        {
            testModel t = new()
            {
                pk = 1,
                npk = "hi"
            };

            _testRepo.Update(t);
        }

        private void Delete(object _)
        {
            _testRepo.Delete(2);
        }

        private void Select(object _)
        {
            Tests = _testRepo.GetAll();
        }

        public ICommand ConnectionCommand => new RelayCommand(Connection);
        public ICommand InsertCommand => new RelayCommand(Insert);
        public ICommand UpdateCommand => new RelayCommand(Update);
        public ICommand DeleteCommand => new RelayCommand(Delete);
        public ICommand SelectCommand => new RelayCommand(Select);
    }   
}
