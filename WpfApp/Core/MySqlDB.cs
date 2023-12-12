using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp.Core
{
    public class MySqlDB : IDisposable
    {
        //변수
        private MySqlConnection? _connection;
        private readonly string _connectionString;

        //생성자
        public MySqlDB(string pConnectionString) 
        {
            _connectionString = pConnectionString;
            Connection();
        }

        private void Connection()
        {
            _connection = new MySqlConnection(_connectionString);

            try
            {
                _connection.Open();
            } catch (Exception ex) 
            {
                Console.WriteLine(ex.ToString());
            }
        }

        private void AddParameters(MySqlCommand cmd, SqlParameter[]? parameters)
        {
            if (parameters != null)
            {
                foreach (SqlParameter param in parameters)
                {
                    cmd.Parameters.AddWithValue(param.Name, param.Value);
                }
            }
        }

        public IDataReader GetReader(string query) 
        {
            return GetReader(query, null);
        }

        public IDataReader GetReader(string query, SqlParameter[]? parameters) 
        {
            using MySqlCommand cmd = new MySqlCommand(query, _connection);

            AddParameters(cmd, parameters);
            return cmd.ExecuteReader();
        }

        public DataTable GetTable(string query)
        {
            return GetTable(query, null);
        }

        public DataTable GetTable(string query, SqlParameter[]? parameters)
        {
            using MySqlCommand cmd = new MySqlCommand(query, _connection);
            AddParameters(cmd, parameters);
            using MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
        }

        public long Execute(string query)
        {
            return Execute(query, null);
        }

        public long Execute(string query, SqlParameter[]? parameters)
        {
            using MySqlCommand cmd = new MySqlCommand(query, _connection);

            AddParameters(cmd, parameters);
            cmd.ExecuteNonQuery();
            return cmd.LastInsertedId;
        }

        public void Dispose()
        {
            _connection?.Close();
            _connection?.Dispose();
            _connection = null;
        }
    }
}
