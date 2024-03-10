#include "pch.h"
#include "Listener.h"
#include "SocketUtils.h"
#include "Protocol.pb.h"
#include "PacketHeader.h"

int main()
{
    SocketUtils::Init();

    Listener listener(L"127.0.0.1", 7777);
    
    thread t1([=]() 
        {
            while (true)
                GIocpCore.Dispatch(); 
        });

    std::thread t2(&Listener::StartAccept, &listener);


    //while (true)
    //{

    //    Protocol::S_TEST pkt;
    //    pkt.set_id(1);
    //    pkt.set_hp(2);
    //    pkt.set_attack(3);

    //    const int array_size = pkt.ByteSizeLong();
    //    
    //    PacketHeader header;
    //    header.id = S_TEST;
    //    header.size = sizeof(PacketHeader) + array_size;

    //    // 바이트 배열 할당
    //    BYTE* byte_array = new BYTE[header.size];

    //    // PacketHeader를 바이트 배열에 복사
    //    memcpy_s(byte_array, header.size, &header, sizeof(header));

    //    // S_TEST 패킷을 바이트 배열에 직렬화하여 복사
    //    // 이 때, PacketHeader 크기만큼 오프셋을 적용
    //    //pkt.SerializeToArray(byte_array + sizeof(PacketHeader), array_size);

    //    if (pkt.SerializeToArray(byte_array + sizeof(PacketHeader), array_size))
    //    {
    //        GIocpCore.Broadcast(byte_array, header.size);
    //    }
    //    else
    //    {
    //        break;
    //    }
    //    this_thread::sleep_for(2s);
    //}

    while (true)
    {

        //PacketHeader header;
        //header.id = 1;
        //header.size = sizeof(PacketHeader); //기본 값 설정
       

        Protocol::ChatMessage message;
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
        }

        message.set_type(Protocol::EP_MessageType::TEXT);

        message.set_roomid("1212");

        const INT32  messageSize = message.ByteSizeLong();
       
        BYTE* byte_array = new BYTE[messageSize+ sizeof(PacketHeader)];
        PacketHeader* header = reinterpret_cast<PacketHeader*>(byte_array);
        header->id = 1;
        header->size = messageSize + sizeof(PacketHeader); //기본 값 설정

		
		if (message.SerializeToArray(byte_array + sizeof(PacketHeader), messageSize))
		{
			GIocpCore.Broadcast(byte_array, messageSize + sizeof(PacketHeader));
		}

        this_thread::sleep_for(4s);
    }

    t2.join();
    t1.join();
    return 0;
}

