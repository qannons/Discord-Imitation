﻿using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using WpfApp.Database.Repo;
using WpfApp.Model;
using WpfApp.MVVM.Model;
using WpfApp.MVVM.View.MainControls.SubControls;
using WpfApp.Service;
using WpfApp.Service.Interface;
using WpfApp.Stores;

namespace WpfApp.MVVM.ViewModel
{
    [ObservableObject]
    public partial class HomeViewModel
    {
        private UserRepo _userRepo;
        private HomeStore _store;
        private ServerCommunicationService _serverService;

        [ObservableProperty]
        private UserControl? _currentSubViewModel;


        [ObservableProperty]
        private string _test;

        [ObservableProperty]
        private ChatRoom _selectedRoom;

        public List<ChatRoom> rooms { get; set; }
        public ObservableCollection<MinimalUser> friends { get; set; }

        [ObservableProperty]
        private string _roomNameLabel = default!;

        [ObservableProperty]
        private string _userName = "UserName";

        private void Init()
        {
            if(_store.CurrentUser != null)
                UserName = _store.CurrentUser.Nickname;
            //기본 화면은 FriendView
            CurrentSubViewModel = new FriendSubView();

            //통신 부분
            _serverService.Connect("127.0.0.1", 7777);

            Thread thread = new Thread(Recv);
            thread.Start();

            //친구 목록 불러오기
            friends = _userRepo.SelectAllFriends(_store.CurrentUser.ID);
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

        [RelayCommand]
        private void NavigateFriend()
        {
            CurrentSubViewModel = new FriendSubView();
        }

        [RelayCommand]
        private void NavDirectMessage()
        {
            CurrentSubViewModel = new DirectMessageSubView();
        }

        [RelayCommand]
        private void NavAddFriend()
        {
            CurrentSubViewModel = new AddFriendSubView();
        }

        [RelayCommand]
        private void AddFriend(string text)
        {
            bool tmp = _userRepo.AddFriend(_store.CurrentUser.ID, text);

            int tmp2;
        }

        private void Recv()
        {
            while(true)
            {
                string recvData = _serverService.Recv();
                Console.WriteLine("recvied: " + recvData);
                recvData = recvData.Split('\0')[0];
                Application.Current.Dispatcher.Invoke(() =>
                {

                    _selectedRoom.Messages.Add(new Message("Server", recvData));
                });
            }
        }

        //생성자
        public HomeViewModel(IUserRepo userRepository, HomeStore pHomeStore, IServerCommunicationService pServerCommunicationService)
        {
            _userRepo = (UserRepo?)userRepository;
            _store = pHomeStore;
            _serverService = (ServerCommunicationService)pServerCommunicationService;
            Init();

            
            rooms = new List<ChatRoom>
            {
                //new ChatRoom{RoomID="123", RoomName="Room1", Members=new List<User>{}, Messages=new List<Message>{ }},
                //new ChatRoom{RoomID="124", RoomName="Room2", Members=new List<User>{}, Messages=new List<Message>{ }}

                new ChatRoom{RoomID="123", RoomName="Room1", Members=new List<User>{}, Messages=new ObservableCollection<Message>{ }},
                new ChatRoom{RoomID="124", RoomName="Room2", Members=new List<User>{}, Messages=new ObservableCollection<Message>{ }}
            };


        }
        public void NaviageToPage(Type pageType)
        {
            Activator.CreateInstance(pageType);
        }
    }
}