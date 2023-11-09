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
    public enum eStatus
    {
        Online,
        AFK,
        Offline
    }

    public class User
    {
        //생성자
        public User(string pUserID, string pUserName, string pProfilePicture = null) 
        {
            UserID = pUserID;
            UserName = pUserName;
            ProfilePicture = pProfilePicture;

            Status = eStatus.Online;
            Friends = null;
            ChatRooms = null;
        }  
        //변수
        public string UserID { get; set; }
        public string UserName { get; set; }
        public string ProfilePicture { get; set; }
        public eStatus Status{ get; set; }
        public List<User> Friends { get; set; }
        public List<ChatRoom> ChatRooms { get; set; }
    }

    public class ChatRoom
    {
        public string RoomID { get; set; }
        public string RoomName { get; set; }
        public List<User> Members { get; set; }
        public ObservableCollection<Message> Messages { get; set; }
        //나중에 삭제
        public string ProfilePicture { get; set; }
    }

    public class Message 
    {
        //생성자
        public Message(string pSenderID, string pContent) 
        {
            MessageID = Guid.NewGuid().ToString();
            SenderID = pSenderID;
            Content = pContent;
            Timestamp = DateTime.Now;
            IsRead = false;
        }    

        //변수
        public string MessageID { get; set; }
        public string SenderID { get; set; }
        public string Content { get; set; }
        public DateTime Timestamp { get; set; }
        public bool IsRead { get; set; }
    }

}
