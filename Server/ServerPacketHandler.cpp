#include "pch.h"
#include "ServerPacketHandler.h"
#include "PacketHeader.h"
#include "IocpCore.h"
//BYTE* ServerPacketHandler::Handle_P_ChatMessage(Protocol::P_ChatMessage& pkt, UINT16 pktId)
//{
//    BYTE* buffer = new BYTE[pkt.ByteSizeLong()+sizeof(PacketHeader)];
//    PacketHeader* header = reinterpret_cast<PacketHeader*>(buffer);
//    header->id = pktId;
//    header->size = pkt.ByteSizeLong() + sizeof(PacketHeader); //기본 값 설정
//    return nullptr;
//}

BYTE* ServerPacketHandler::Handle_P_ChatMessage(BYTE* buffer, INT32 len)
{
    Protocol::P_ChatMessage pkt;

    if (pkt.ParseFromArray(buffer + sizeof(PacketHeader), len - sizeof(PacketHeader)))
    {
        cout << "Message ID: " << pkt.base().messageid() << endl;				//1
        cout << "Room ID: " << pkt.base().roomid() << endl;							//3
        cout << "Sender ID: " << pkt.base().sender().userid() << endl;			//4
        cout << "Username: " << pkt.base().sender().username() << endl;	//4
        cout << "Timestamp: " << pkt.base().timestamp() << endl;				//5	

        cout << "Content: " << pkt.content() << endl;										//2-2

        //tmp;
        GIocpCore.Broadcast(buffer, len);
    }
    else
    {
        return;
    }
    //GIocpCore.Broadcast(pkt, 1);
}

BYTE* ServerPacketHandler::Handle_P_ImageMessage(BYTE* buffer, INT32 len)
{
    Protocol::P_ImageMessage pkt;

    if (pkt.ParseFromArray(buffer + sizeof(PacketHeader), len - sizeof(PacketHeader)))
    {
        cout << "Message ID: " << pkt.base().messageid() << endl;				//1
        cout << "Room ID: " << pkt.base().roomid() << endl;							//3
        cout << "Sender ID: " << pkt.base().sender().userid() << endl;			//4
        cout << "Username: " << pkt.base().sender().username() << endl;	//4
        cout << "Timestamp: " << pkt.base().timestamp() << endl;				//5	

        cout << "Content: " << pkt.content() << endl;										//2-2

        //tmp;
        GIocpCore.Broadcast(buffer, len);
    }
    else
    {
        return;
    }
    //GIocpCore.Broadcast(pkt, 1);

    
}

