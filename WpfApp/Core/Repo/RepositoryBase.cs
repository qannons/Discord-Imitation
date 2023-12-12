using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp.Core.Repo
{
    public abstract class RepositoryBase
    {
        public MySqlDB GetMySqlDB()
        {
            return new MySqlDB(Properties.Settings.Default.DB_CONN_STR);
        }
    }
}
