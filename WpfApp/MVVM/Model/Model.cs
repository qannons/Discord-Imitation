using CommunityToolkit.Mvvm.ComponentModel;
using Google.Protobuf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp.Core;
using WpfApp.MVVM.Model;

namespace WpfApp.Model
{
    public class ChatRoom
    {
        public Guid RoomID { get; set; }
        public string RoomName { get; set; }
        public List<User> Members { get; set; }
        public ObservableCollection<Message> Messages { get; set; }
    }

    public class Message
    {
        public Message() { }


        public Message(string pSenderID, string pContent)
        {
            SenderID = pSenderID;
            Content = pContent;
            Timestamp = DateTime.Now.ToString("yyyy.MM.dd tt hh:mm");
            IsRead = false;
            ImagePath = "C:\\Users\\PC\\Desktop\\씨@발.png";
        }


        //변수
        public string MessageID { get; set; }
        public string SenderID { get; set; }
        public string? Content { get; set; }
        public string Timestamp { get; set; }
        public bool IsRead { get; set; }
        public string ImagePath = "C:\\Users\\PC\\Desktop\\씨@발.png";
    }
}
