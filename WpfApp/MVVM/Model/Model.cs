using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp.Core;

namespace WpfApp.Model
{
    //public enum eStatus
    //{
    //    Online,
    //    AFK,
    //    Offline
    //}

    //public class User
    //{
    //    //생성자
    //    public User(string pUserID, string pUserEmail, string pUserName, string pProfilePicture = null) 
    //    {
    //        UserID = pUserID;
    //        UserEmail = pUserEmail;
    //        UserName = pUserName;
    //        ProfilePicture = pProfilePicture;

    //        Status = eStatus.Online;
    //        Friends = null;
    //        ChatRooms = null;
    //    }  
    //    //변수
    //    public string UserID { get; set; }
    //    public string UserEmail {  get; set; }
    //    public string UserName { get; set; }
    //    public string ProfilePicture { get; set; }
    //    public eStatus Status{ get; set; }
    //    public List<User> Friends { get; set; }
    //    public List<ChatRoom> ChatRooms { get; set; }
    //}

    //public class ChatRoom
    //{
    //    public string RoomID { get; set; }
    //    public string RoomName { get; set; }
    //    public List<User> Members { get; set; }
    //    public ObservableCollection<Message> Messages { get; set; }
    //    //나중에 삭제
    //    public string ProfilePicture { get; set; }
    //}

    //public class Message 
    //{
    //    //생성자
    //    public Message(string pSenderID, string pContent) 
    //    {
    //        MessageID = Guid.NewGuid().ToString();
    //        SenderID = pSenderID;
    //        Content = pContent;
    //        Timestamp = DateTime.Now.ToString("yyyy.MM.dd tt hh:mm");
    //        IsRead = false;
    //        //ImageSource = UserManager.GetImageSource(pSenderID);
    //    }    

    //    //변수
    //    public string MessageID { get; set; }
    //    public string SenderID { get; set; }
    //    public string Content { get; set; }
    //    public string Timestamp { get; set; }
    //    public bool IsRead { get; set; }
    //    //public string ImageSource {  get; set; }
    //}
    ////static public class UserManager
    ////{
    ////    public static string GetImageSource(string pSenderID)
    ////    {
    ////        return Users[pSenderID].ProfilePicture;
    ////    }


    ////    public static Dictionary<string, User> Users;
    ////}

    //public class FriendShip
    //{
    //    //생성자

    //    //변수
    //    public string user_id;
    //    public List<string> friend_name;
    //}
}
