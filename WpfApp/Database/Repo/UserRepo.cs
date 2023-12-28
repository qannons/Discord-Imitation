using MySqlX.XDevAPI;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using WpfApp.Model;
using WpfApp.MVVM.Model;

namespace WpfApp.Database.Repo
{
    class UserRepo : RepositoryBase, IUserRepo
    {
        public long Insert(User user)
        {
            string query = "INSERT INTO users(email, pwd, user_name, nickname) VALUES(@email, @pwd, @user_name, @nickname);";

            using (MySqlDB? db = GetTestDB())
            {
                return db.Execute(query, new SqlParameter[]
                {
                  new SqlParameter("@email", user.Email),
                  new SqlParameter("@pwd", user.Password),
                  new SqlParameter("@user_name", user.Name),
                  new SqlParameter("@nickname", user.Nickname),
                });
            }
        }

        public List<User> SelectAll()
        {
            List<User> list = new List<User>();
            string query = "SELECT * FROM users;";
            using MySqlDB? db = GetTestDB();
            using (IDataReader dr = db.GetReader(query))
            {
                while (dr.Read())
                {
                    User account = new User()
                    {
                        ID = (int)dr["id"],
                        Email = (string)dr["email"],
                        Password = (string)dr["pwd"],
                        Name = (string)dr["user_name"],
                        Nickname = (string)dr["nickname"],
                    };
                    list.Add(account);
                }
            }
            return list;
        }
        
        public string? SelectUser(string pEmail)
        {
            string query = "SELECT nickname FROM users WHERE email = @Email;";
            using MySqlDB? db = GetTestDB();

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@Email", pEmail)
            };

            using (IDataReader dr = db.GetReader(query, parameters))
            {
                if (dr.Read())
                {
                    return (string)dr["nickname"];
                }
            }
            return null;
        }

        public bool IsExistEmail(string pEmail, string pPwd)
        {
            string query = "SELECT EXISTS(SELECT 1 FROM users WHERE email = @Email)";
            using MySqlDB? db = GetTestDB();

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@Email", pEmail),
                new SqlParameter("@Password", pPwd)
            };

            //using IDataReader dr = db.GetReader(query, parameters);
            using (IDataReader dr = db.GetReader(query, parameters))
            {
                if (dr.Read())
                {
                    int count = dr.GetInt32(0);
                    return count > 0;
                }
            }
            return false;
            //return dr.Read();
        }

        public void Update(User user)
        {
            string query = "UPDATE users SET email = @email, pwd = @pwd, user_name = @user_name nickname = @nickname WHERE id = @id;";

            using (MySqlDB? db = GetTestDB())
            {
                db.Execute(query, new SqlParameter[]
                {
                  new SqlParameter("@id", user.ID),
                  new SqlParameter("@email", user.Email),
                  new SqlParameter("@pwd", user.Password),
                  new SqlParameter("@user_name", user.Name),
                  new SqlParameter("@nickname", user.Nickname)
                });
            }
        }

        public void Delete(int id)
        {
            string query = "DELETE FROM users WHERE id = @id;";
            using (MySqlDB? db = GetTestDB())
            {
                db.Execute(query, new SqlParameter[]
                {
                    new SqlParameter("@id", id),
                });
            }
        }

        public bool IsExistEmail(string pEmail)
        {
            string query = "SELECT 1 FROM USER WHERE ID = @ID;";
            using MySqlDB db = GetTestDB();
            using var dr = db.GetReader(query, new SqlParameter[] { new SqlParameter("@email", pEmail)});
            return dr.Read();
        }
    }
}
