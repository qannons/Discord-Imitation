using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp.Model;

namespace WpfApp.Core.Repo
{
    public class TestRepo : RepositoryBase, ITestRepo
    {
        public void Delete(int modelPK)
        {
            string query = "DELETE FROM testtable WHERE pk = @pk;";

            using (var db = GetMySqlDB())
            {
                db.Execute(query, new SqlParameter[]
                {
                    new SqlParameter("@pk", modelPK),
                });
            }
        }

        public long Insert(testModel model)
        {
            string query = "INSERT INTO testtable(pk, npk) VALUES (@pk, @npk);";
            
            using (var db = GetMySqlDB())
            {
                return db.Execute(query, new SqlParameter[]
                {
                    new SqlParameter("@pk", model.pk),
                    new SqlParameter("@npk", model.npk)
                });
            }
        }

        public void Update(testModel model)
        {
            string query = "UPDATE testtable SET npk = @npk" +
                                     " WHERE pk=@pk";

            using (var db = GetMySqlDB())
            {
                db.Execute(query, new SqlParameter[]
                {
                    new SqlParameter("@pk", model.pk),
                    new SqlParameter("@npk", model.npk)
                });
            }
        }

        public List<testModel> GetAll()
        {
            List<testModel> list = new List<testModel>();
            string query = "SELECT * FROM testTable;";
            using (MySqlDB? db = GetMySqlDB())
            {
                using (IDataReader dr = db.GetReader(query))
                {
                    while(dr.Read())
                    {
                        testModel t = new testModel()
                        {
                            pk = (int)dr["pk"],
                            npk = (string)dr["npk"]
                        };
                        list.Add(t);
                    }
                }
            }
            return list;
        }
    }
}
