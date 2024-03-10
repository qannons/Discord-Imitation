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

    t2.join();
    t1.join();
    return 0;
}

