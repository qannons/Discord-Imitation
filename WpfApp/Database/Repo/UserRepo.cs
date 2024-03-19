using MySql.Data.MySqlClient;
using MySqlX.XDevAPI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls.Primitives;
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
                        ID = (UInt16)dr["id"],
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
        
        public User? SelectUser(string pEmail)
        {
            string query = "SELECT ID, EMAIL, USER_NAME, NICKNAME FROM users WHERE email = @Email;";
            using MySqlDB? db = GetTestDB();

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@Email", pEmail)
            };

            using (IDataReader dr = db.GetReader(query, parameters))
            {
                if (dr.Read())
                {
                    User user = new User()
                    {
                        ID = Convert.ToUInt32(dr["id"]),
                        Email = (string)dr["email"],
                        Name = (string)dr["user_name"],
                        Nickname = (string)dr["nickname"],
                    };
                    return user;
                }
            }
            return null;
        }

        public ObservableCollection<MinimalUser> SelectAllFriends(int pUserID)
        {
            ObservableCollection<MinimalUser> list = new ObservableCollection<MinimalUser>();

            //string query = "SELECT friend_id FROM friendship WHERE user_id = @id;";
            string query = "SELECT f.friend_id, u2.user_name, u2.nickname FROM Users u1 INNER JOIN Friendship f ON u1.id = f.user_id INNER JOIN Users u2 ON f.friend_id = u2.id WHERE u1.id=@id;";
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@id", pUserID),
            };

            using MySqlDB? db = GetTestDB();
            using (IDataReader dr = db.GetReader(query, parameters))
            {
                while (dr.Read())
                {
                    MinimalUser user = new MinimalUser()
                    {  
                        ID = (UInt32)dr["friend_id"],
                        
                        Name = (string)dr["user_name"],
                        Nickname = (string)dr["nickname"],
                    };
                    list.Add(user);
                }
            }
            return list;
        }

        public bool IsExistEmail(string pEmail, string pPwd)
        {
            string query = "SELECT EXISTS(SELECT 1 FROM users WHERE email = @Email AND pwd = @Password)";
            using MySqlDB? db = GetTestDB();

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@Email", pEmail),
                new SqlParameter("@Password", pPwd)
            };

            using (IDataReader dr = db.GetReader(query, parameters))
            {
                if (dr.Read())
                {
                    int count = dr.GetInt32(0);
                    return count > 0;
                }
            }
            return false;
        }

        public void Update(User user)
        {
            string query = "UPDATE USERS SET email = @email, pwd = @pwd, user_name = @user_name nickname = @nickname WHERE id = @id;";

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
            string query = "DELETE FROM USERS WHERE id = @id;";
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
            string query = "SELECT 1 FROM USERS WHERE ID = @ID;";
            using MySqlDB db = GetTestDB();
            using var dr = db.GetReader(query, new SqlParameter[] { new SqlParameter("@email", pEmail)});
            return dr.Read();
        }

        public bool AddFriend(UInt32 pUserID, string pFriendName)
        {
            string query = "SELECT ID FROM USERS WHERE USER_NAME = @friendName;";
            using (MySqlDB? db = GetTestDB())
            {
                using (var dr = db.GetReader(query, new SqlParameter[]  { new SqlParameter("@friendName", pFriendName) }))
                {
                    //pFriendName가 유효하지 않다면 false
                    if (dr.Read() == false)
                    {
                        return false;
                    }

                    int friendID = (int)dr["ID"];
                    dr.Close();

                    query = "INSERT INTO FRIENDSHIP(USER_ID, FRIEND_ID, STATUS) VALUES(@user_id, @friend_id, @status);";
                    string query2 = "INSERT INTO FRIENDSHIP(USER_ID, FRIEND_ID) VALUES(@friend_id, @user_id);";
                    try
                    {
                        db.Execute(query, new SqlParameter[]
                        {
                            new SqlParameter("@user_id", pUserID),
                            new SqlParameter("@friend_id", friendID),
                            new SqlParameter("@status", "REQUEST"),
                        });

                        db.Execute(query2, new SqlParameter[]
                        {
                            new SqlParameter("@user_id", pUserID),
                            new SqlParameter("@friend_id", friendID),
                        });
                    }
                    catch { return false; }
                }
                return true;
            }
        }
    }
}
