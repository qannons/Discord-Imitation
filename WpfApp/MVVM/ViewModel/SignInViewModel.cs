using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using WpfApp.Core;

namespace WpfApp.MVVM.ViewModel
{
    internal class SignInViewModel : ObservableObject
    {
        private MySqlConnection _conn = default!;
        
        private void Connection(object _)
        {
            string connectionString = "UID: root;PWD=0913;Server=127.0.0.1;Port=3306;Database=kabultalk";
            _conn = new MySqlConnection(connectionString);
            try
            {
                _conn.Open();

            }
            catch (Exception ex) 
            {
                MessageBox.Show($"Connection 실패\n {ex.Message}");
            }
        }
        

        

    }
}
