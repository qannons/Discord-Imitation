using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using WpfApp.MVVM.Model;

namespace WpfApp.Database.Repo
{
    public interface IUserRepo
    {
        long Insert(User account);
        List<User> SelectAll();
        void Update(User account);
        void Delete(int id);
        public bool IsExistEmail(string pEmail, string pPwd);

        public bool AddFriend(UInt32 pUserID, string pFriendName);
    }
}
