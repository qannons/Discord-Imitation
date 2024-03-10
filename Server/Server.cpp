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
        

        this_thread::sleep_for(2s);
    }

    t2.join();
    t1.join();
    return 0;
}

