#include "pch.h"
#include "Listener.h"
#include "SocketUtils.h"
#include "Protocol.pb.h"
#include "PacketHeader.h"

int main()
{
    SocketUtils::Init();

    Listener listener(L"203.237.81.67", 7777);
    
    thread t1([=]() 
        {
            while (true)
                GIocpCore.Dispatch(); 
        });

    std::thread t2(&Listener::StartAccept, &listener);

    while (true)
    {

        //PacketHeader header;
        //header.id = 1;
        //header.size = sizeof(PacketHeader); //기본 값 설정
        Protocol::

       /* Protocol::P_ChatMessage message;
        {
            Protocol::P_Sender* sender = new Protocol::P_Sender;
            sender->set_userid(1);
            sender->set_username("cannons");
            message.set_allocated_sender(sender);
        }

        message.set_content("Hello World!");

        {
            auto now = std::chrono::system_clock::now();
            auto timestamp = std::chrono::duration_cast<std::chrono::seconds>(now.time_since_epoch()).count();
            message.set_timestamp(timestamp);
        }*/

        message.set_type(Protocol::EP_MessageType::TEXT);

        message.set_roomid("1212");

        const INT32  messageSize = message.ByteSizeLong();

        BYTE* byte_array = new BYTE[messageSize + sizeof(PacketHeader)];
        PacketHeader* header = reinterpret_cast<PacketHeader*>(byte_array);
        header->id = 1;
        header->size = messageSize + sizeof(PacketHeader); //기본 값 설정


        //if (message.SerializeToArray(byte_array + sizeof(PacketHeader), messageSize))
        //{
        //    GIocpCore.Broadcast(byte_array, messageSize + sizeof(PacketHeader));
        //}

        this_thread::sleep_for(4s);
    }

    t2.join();
    t1.join();
    return 0;
}

