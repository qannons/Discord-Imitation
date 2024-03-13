using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Org.BouncyCastle.Bcpg;
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
using WpfApp.Protocol;
using WpfApp.Service;
using WpfApp.Service.Interface;
using WpfApp.Stores;
using Protocol;
using System.Diagnostics;
using Microsoft.Win32;
using System.IO;
using System.Reflection.Metadata;
using System.Net.NetworkInformation;


namespace WpfApp.MVVM.ViewModel
{
    [ObservableObject]
    public partial class HomeViewModel
    {

        private UserRepo _userRepo;
        private HomeStore _store;
        private ServerCommunicationService _serverService;
        private RecvBuffer _recvBuffer;

        [ObservableProperty]
        private UserControl? _currentSubViewModel;

        [ObservableProperty]
        private string _test;

        [ObservableProperty]
        private ChatRoom _selectedRoom;

        [ObservableProperty]
        private string _timestamp;

        public List<ChatRoom> rooms { get; set; }
        public ObservableCollection<MinimalUser> friends { get; set; }

        [ObservableProperty]
        private string _roomNameLabel = default!;

        [ObservableProperty]
        private string _userName = "UserName";

        [ObservableProperty]
        private eSTATE _userState = eSTATE.Offline;

        private void Init()
        {
            Test = "";

            _recvBuffer = new RecvBuffer(0x10000);
            if(_store.CurrentUser != null)
                UserName = _store.CurrentUser.Nickname;
            //기본 화면은 FriendView
            CurrentSubViewModel = new FriendSubView();

            //통신 부분
            if(_serverService.Connect("127.0.0.1", 7777))
            {
                _userState = eSTATE.Online;
                Thread thread = new Thread(Recv);
                thread.Start();
            }
            else
            {
                Debug.Print("연결실패");
                _userState = eSTATE.Offline;
            }

            //친구 목록 불러오기
            //friends = _userRepo.SelectAllFriends(_store.CurrentUser.ID);
        }

        [RelayCommand]
        private void ImageSend()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "이미지 파일|*.jpg;*.jpeg;*.png;*.gif|모든 파일|*.*";

            if (openFileDialog.ShowDialog() == true)
            {
                // 선택한 이미지 파일의 경로 가져오기
                string imagePath = openFileDialog.FileName;

                // 이미지 파일을 바이트 배열로 변환
                byte[] imageData = File.ReadAllBytes(imagePath);


                //이미지를 서버로 전송
                if (imageData != null)
                {
                    //bool success = await SendImageToServerAsync(imageData);
                    _serverService.Send(SelectedRoom.RoomID, imageData, _store.CurrentUser, ePacketID.IMAGE_MESSAGE);

                }
            }
            else
            {
                Debug.Print("Fail to open file");
            }
        }

        [RelayCommand]
        private void Enter(object pUserInput)
        {

            if (Test.Length == 0)
                    return;

            
            SelectedRoom.Messages.Add(new Message(UserName, Test));
            byte[] bytes = Encoding.UTF8.GetBytes(Test);
            _serverService.Send(SelectedRoom.RoomID, bytes, _store.CurrentUser);
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
                if (_selectedRoom == null)
                    continue;
                

                 int numOfBytes = _serverService.Recv(_recvBuffer.WritePos());
                _recvBuffer.OnWrite(numOfBytes);
                

               Int32 ProcessLen= OnRecv(_recvBuffer.ReadPos(), _recvBuffer.DataSize());

                _recvBuffer.OnRead(ProcessLen);
                _recvBuffer.Clean();
            }
        }

        private unsafe int OnRecv(Span<byte> buffer, int len)
        {
            Int32 processLen = 0;

            while (true)
            {
                Int32 dataSize = len - processLen;
                //최소한 헤더는 파싱할 수 있어야 한다
                if (dataSize < sizeof(PacketHeader))
                    break;
                fixed (byte* ptr = buffer)
                {
                    PacketHeader* header = (PacketHeader*)(ptr + processLen);
                    // 헤더에 기록된 패킷 크기를 파싱할 수 있어야 한다
                    if (dataSize < header->size)
                        break;

                    //패킷 조립 성공
                    HandlePacket(buffer.Slice(processLen), header->size, (ePacketID)header->id);

                    processLen += header->size;
                   
                }
            }
            return processLen;
        }
        private void HandlePacket(Span<byte> buffer, int len, ePacketID ID)
        {
            byte[] byteBuffer = buffer.ToArray();
            switch (ID)
            {
                case ePacketID.CHAT_MESSAGE:
                    Handle_P_ChatMessage(byteBuffer, len);
                    break;

                case ePacketID.IMAGE_MESSAGE:
                    Handle_P_ImageMessage(byteBuffer, len);
                    break;
            }
            
        }

        unsafe void Handle_P_ChatMessage(byte[] buffer , int len)
        {
            int headerSize = sizeof(PacketHeader);
            P_ChatMessage message = P_ChatMessage.Parser.ParseFrom(buffer, headerSize, len - headerSize);
            //if (message.Base.Sender.UserID == _store.CurrentUser?.ID)
            //{
            //    return;
            //}

            DateTimeOffset dateTimeOffset = DateTimeOffset.FromUnixTimeSeconds(message.Base.Timestamp);
            // 출력 형식 지정
            string formattedDate = dateTimeOffset.ToString("yyyy.MM.dd. tt hh:mm");

            Application.Current.Dispatcher.Invoke(() =>
            {
                _timestamp = formattedDate;
                _selectedRoom.Messages.Add(new Message(message.Base.Sender.UserName, message.Content));
            });
        }

        unsafe void Handle_P_ImageMessage(byte[] buffer, int len)
        {
            int headerSize = sizeof(PacketHeader);
            P_ImageMessage message = P_ImageMessage.Parser.ParseFrom(buffer, headerSize, len - headerSize);
            //if (message.Base.Sender.UserID == _store.CurrentUser?.ID)
            //{
            //    return;
            //}

            DateTimeOffset dateTimeOffset = DateTimeOffset.FromUnixTimeSeconds(message.Base.Timestamp);
            // 출력 형식 지정
            string formattedDate = dateTimeOffset.ToString("yyyy.MM.dd. tt hh:mm");
            string s = message.Content.ToString();
            Application.Current.Dispatcher.Invoke(() =>
            {
                _timestamp = formattedDate;
                
                _selectedRoom.Messages.Add(new Message {
                    SenderID= message.Base.Sender.UserName, 
                    Timestamp= formattedDate
                    //ImagePath = message.Content
                });
            });
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

                new ChatRoom{RoomID=Guid.NewGuid(), RoomName="Room1", Members=new List<User>{}, Messages=new ObservableCollection<Message>{ }},
                new ChatRoom{RoomID=Guid.NewGuid(), RoomName="Room2", Members=new List<User>{}, Messages=new ObservableCollection<Message>{ }}
            };


        }
        public void NaviageToPage(Type pageType)
        {
            Activator.CreateInstance(pageType);
        }
    }

}
