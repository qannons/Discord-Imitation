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

    //listener.StartAccept();
    std::thread t2(&Listener::StartAccept, &listener);


    while (true)
    {
        Protocol::S_TEST pkt;
        pkt.set_id(S_TEST);
        pkt.set_hp(2);
        pkt.set_attack(3);

        const int array_size = pkt.ByteSizeLong();
        //const int array_size = 6;

        // 배열 할당
        BYTE* byte_array = new BYTE[array_size];

        if (pkt.SerializeToArray(byte_array, array_size))
        {
            GIocpCore.Broadcast(byte_array);
        }
        else
        {
            break;
        }
        this_thread::sleep_for(2s);
    }

    t2.join();
    t1.join();
    return 0;
}

