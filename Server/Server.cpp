#include "pch.h"
#include "Listener.h"
#include "SocketUtils.h"

int main()
{
    SocketUtils::Init();

    Listener listener(L"127.0.0.1", 7777);
    
    thread t([=]() 
        {
            while (true)
                GIocpCore.Dispatch(); 
        });

    listener.StartAccept();

    t.join();
    return 0;
}

