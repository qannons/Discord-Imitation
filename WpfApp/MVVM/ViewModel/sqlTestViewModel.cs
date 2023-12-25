using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using WpfApp.Database.Repo;
using WpfApp.MVVM.Model;

namespace WpfApp.MVVM.ViewModel
{
    class sqlTestViewModel : ObservableObject
    {
        private IUserRepo _userRepo;
        private List<User> _users = default!;
        public List<User> Users
        {
            get => _users; set
            {
                if (_users != value)
                {
                    _users = value;
                    OnPropertyChanged();
                }
            }
        }

        private void Connection(object _)
        {

        }

        private void Insert(object _)
        {
            User user = new()
            {
                Email = "test1@naver.com",
                Password = "pwd1",
                Name = "test1",
            };
            _userRepo.Insert(user);
        }

        private void Update(object _)
        {
            User user = new()
            {
                ID = 1,
                Email = "updatedTest@naver.com",
                Password = "updatedPwd1",
                Name = "updatedTest1",
            };
            _userRepo.Update(user);
        }

        private void Delete(object _)
        {
            _userRepo.Delete(2);
        }

        private void Select(object _)
        {
            Users = _userRepo.SelectAll();
        }

        public sqlTestViewModel(IUserRepo userRepository)
        {
            _userRepo = userRepository;
        }

   
        public ICommand ConnectionCommand => new RelayCommand<object>(Connection);
        public ICommand InsertCommand => new RelayCommand<object>(Insert);
        public ICommand UpdateCommand => new RelayCommand<object>(Update);
        public ICommand DeleteCommand => new RelayCommand<object>(Delete);
        public ICommand SelectCommand => new RelayCommand<object>(Select);
    }
}
