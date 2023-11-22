using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using WpfApp.Core;
using WpfApp.Model;
using WpfApp.Service;

namespace WpfApp.MVVM.ViewModel
{
    class MainViewModel : ObservableObject
    {
        public ServerCommunicationService client;
        //생성자
        public MainViewModel()
        {
            client = new ServerCommunicationService();
            client.Connect("127.0.0.1", 7777);
            //
            users = new ObservableCollection<User>
            {
                new User("1", "User1", "https://www.simplilearn.com/ice9/free_resources_article_thumb/what_is_image_Processing.jpg"),
                new User("2", "User2", "https://www.simplilearn.com/ice9/free_resources_article_thumb/what_is_image_Processing.jpg")
            };

            rooms = new List<ChatRoom>
            { 
                new ChatRoom{RoomID="123", RoomName="Room1", Members=new List<User>{}, Messages=new ObservableCollection<Message>{ }, ProfilePicture="https://www.simplilearn.com/ice9/free_resources_article_thumb/what_is_image_Processing.jpg"},
                new ChatRoom{RoomID="124", RoomName="Room2", Members=new List<User>{}, Messages=new ObservableCollection<Message>{ },  ProfilePicture="https://www.simplilearn.com/ice9/free_resources_article_thumb/what_is_image_Processing.jpg"}
            };
            
            rooms[0].Members.Add(users[0]);
            rooms[0].Members.Add(users[1]);
            rooms[0].Messages.Add(new Message("1", "Cannons"));

            rooms[1].Members.Add(users[0]);
            rooms[1].Members.Add(users[1]);
            rooms[1].Messages.Add(new Message("2", "Cannons2"));

            RoomNameLabel = "@ChattingRoom";

        }

        //변수
        

        public List<ChatRoom> rooms {  get; set; }
        private string _roomNameLabel { get; set; }
        public string RoomNameLabel
        {
            get { return _roomNameLabel; }
            set { _roomNameLabel = value; OnPropertyChanged(nameof(RoomNameLabel)); }
        }

        private ChatRoom _selectedRoom;
        public ChatRoom SelectedRoom
        {
            get { return _selectedRoom; }
            set { 
                _selectedRoom = value;
                _roomNameLabel = value.RoomName;
                OnPropertyChanged(nameof(RoomNameLabel));
                OnPropertyChanged(nameof(SelectedRoom)); 
            }
        }

        public ObservableCollection<User> users { get; set; }

        private string _userInput;

        public string UserInput
        {
            get { return _userInput; }
            set
            {
                _userInput = value;
                OnPropertyChanged(nameof(UserInput));
            }
        }

        private RelayCommand _sendCommand;

        public RelayCommand SendCommand
        {
            get
            {
                if (_sendCommand == null)
                {
                    _sendCommand = new RelayCommand(
                        param =>
                        {
                            // ListView에 데이터 추가
                            SelectedRoom.Messages.Add(new Message("1", UserInput));
                            // 다른 사용자에게 데이터 전송 (예: 서버 또는 네트워크 호출)
                            client.Send(UserInput);
                            //SendMessageToOthers(messageText);

                            // TextBox 비우기
                            UserInput = string.Empty;
                        }
                    );
                }
                return _sendCommand;
            }
        }
    }
}
