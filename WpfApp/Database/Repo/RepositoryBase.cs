using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp.Database.Repo
{
    public abstract class RepositoryBase
    {
        protected MySqlDB GetTestDB()
        {
            return new MySqlDB(Properties.Settings.Default.TEST_DB_CONN_STR);
        }
    }
}
