using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using WpfApp.Model;
using WpfApp.MVVM.Model;
using WpfApp.Service;
using WpfApp.Service.Interface;
using WpfApp.Stores;

namespace WpfApp.MVVM.ViewModel
{
    [ObservableObject]
    public partial class HomeViewModel
    {
        private HomeStore _store;
        private ServerCommunicationService _serverService;


        [ObservableProperty]
        private string _test;

        [ObservableProperty]
        private ChatRoom _selectedRoom;

        public List<ChatRoom> rooms { get; set; }

        [ObservableProperty]
        private string _roomNameLabel = default!;

        [ObservableProperty]
        private string _userName = "UserName";

        private void Init()
        {
            if(_store.CurrentUser != null)
                UserName = _store.CurrentUser.Nickname;

            _serverService.Connect("127.0.0.1", 7777);
        }

        [RelayCommand]
        private void Enter(object pUserInput)
        {
            if (Test.Length == 0)
                    return;
            SelectedRoom.Messages.Add(new Message(UserName, Test));
            _serverService.Send(Test);
            Test = "";
        }

        //생성자
        public HomeViewModel(HomeStore pHomeStore, IServerCommunicationService pServerCommunicationService)
        {
            _store = pHomeStore;
            _serverService = (ServerCommunicationService)pServerCommunicationService;
            Init();

            
            rooms = new List<ChatRoom>
            {
                new ChatRoom{RoomID="123", RoomName="Room1", Members=new List<User>{}, Messages=new ObservableCollection<Message>{ }},
                new ChatRoom{RoomID="124", RoomName="Room2", Members=new List<User>{}, Messages=new ObservableCollection<Message>{ }}
            };


        }
    }
}
