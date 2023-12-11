using MySql.Data.MySqlClient;
using MySqlX.XDevAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using WpfApp.Core;
using WpfApp.Model;

namespace WpfApp.MVVM.ViewModel
{
    class testViewModel
    {
        private MySqlConnection _conn = default!;
        //private IAccountRepository _accountRepository;
        //private List<Account> _accounts = default!;

        private void Connection(object _)
        {
            string connectionString = "UID=root;PWD=0913;Server=127.0.0.1;Port=3306;Database=dis";
            try
            {
                _conn = new MySqlConnection(connectionString);
                _conn.Open();

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Connection 실패\n {ex.Message}");
            }
        }

        private void Insert(object _)
        {
            string query = "INSERT INTO testtable(pk, npk) VALUES (@pk, @npk);";

            using (MySqlCommand cmd = _conn.CreateCommand())
            {
                cmd.CommandText = query;
                cmd.Parameters.Add("@pk", MySqlDbType.Int16);
                cmd.Parameters.Add("@npk", MySqlDbType.VarChar);

                cmd.Parameters["@pk"].Value = 2;
                cmd.Parameters["@npk"].Value = "test3";

                cmd.ExecuteNonQuery();
            }
        }

        private void Update(object _)
        {
            string query = "UPDATE testtable SET npk = @npk" + 
                                     " WHERE pk=@pk";

            using (MySqlCommand cmd = _conn.CreateCommand())
            {
                cmd.CommandText = query;
                cmd.Parameters.Add("@pk", MySqlDbType.Int16);
                cmd.Parameters.Add("@npk", MySqlDbType.VarChar);

                cmd.Parameters["@pk"].Value = 0;
                cmd.Parameters["@npk"].Value = "testUpdate";

                cmd.ExecuteNonQuery();
            }
        }

        private void Delete(object _)
        {
            string query = "DELETE FROM testtable WHERE pk = @pk;";

            using (MySqlCommand cmd = _conn.CreateCommand())
            {
                cmd.CommandText = query;
                cmd.Parameters.Add("@pk", MySqlDbType.Int16);
                cmd.Parameters["@pk"].Value = 2;

                cmd.ExecuteNonQuery();
            }
        }

        private void Select(object _)
        {
            string query = "SELECT * FROM testtable WHERE pk = @pk";

            using (MySqlCommand cmd = _conn.CreateCommand())
            {
                cmd.CommandText = query;
                cmd.Parameters.Add("@pk", MySqlDbType.Int16);
                cmd.Parameters["@pk"].Value = 1;

                //cmd.ExecuteNonQuery();
                using (MySqlDataReader dr = cmd.ExecuteReader())
                {
                    //using 문은 IDisposable 인터페이스를 구현한 객체를 사용할 때 자원을 효과적으로 해제하기 위해 사용

                }
                //DataReader 모두 사용한 후 반드시 close해줘야 한다. 

            }
        }

        //public MainViewModel(IAccountRepository accountRepository)
        //{
        //    _accountRepository = accountRepository;
        //}

        //public List<Account> Accounts
        //{
        //    get => _accounts; set
        //    {
        //        if (_accounts != value)
        //        {
        //            _accounts = value;
        //            OnPropertyChanged();
        //        }
        //    }
        //}
        public ICommand ConnectionCommand => new RelayCommand(Connection);
        public ICommand InsertCommand => new RelayCommand(Insert);
        public ICommand UpdateCommand => new RelayCommand(Update);
        public ICommand DeleteCommand => new RelayCommand(Delete);
        public ICommand SelectCommand => new RelayCommand(Select);
    }   
}
