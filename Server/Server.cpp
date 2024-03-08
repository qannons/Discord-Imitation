#include "pch.h"
#include "Listener.h"
#include "SocketUtils.h"
#include "Protocol.pb.h"

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
        pkt.set_id(1);
        pkt.set_hp(2);
        pkt.set_attack(3);

        {
            Protocol::BuffData* data = pkt.add_buffs();
            data->set_buffid(100);
            data->set_remaintime(1.2f);
            data->add_victims(4000);
        }
        {
            Protocol::BuffData* data = pkt.add_buffs();
            data->set_buffid(200);
            data->set_remaintime(2.5f);
            data->add_victims(5000);
        }
        vector<uint8_t> serialized_data(pkt.ByteSizeLong());
        if (pkt.SerializeToArray(serialized_data.data(), serialized_data.size()))
        {
            GIocpCore.Broadcast(serialized_data.data());
        }
        this_thread::sleep_for(2s);
    }

    t2.join();
    t1.join();
    return 0;
}

