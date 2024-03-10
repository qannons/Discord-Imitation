#include "pch.h"
#include "ServerPacketHandler.h"
#include "PacketHeader.h"

BYTE* ServerPacketHandler::Handle_P_ChatMessage(Protocol::ChatMessage& pkt, UINT16 pktId)
{
    BYTE* buffer = new BYTE[pkt.ByteSizeLong()+sizeof(PacketHeader)];
    PacketHeader* header = reinterpret_cast<PacketHeader*>(buffer);
    header->id = pktId;
    header->size = pkt.ByteSizeLong() + sizeof(PacketHeader); //기본 값 설정
    return nullptr;
}

BYTE* ServerPacketHandler::Handle_P_ChatMessage2(Protocol::ChatMessage& pkt)
{
    //Protocol::ChatMessage message;
    //{
    //    Protocol::P_Sender* sender = new Protocol::P_Sender;
    //    sender->set_userid(1);
    //    sender->set_username("cannons");
    //    message.set_allocated_sender(sender);
    //}

    //message.set_content("Hello World!");

    //{
    //    auto now = std::chrono::system_clock::now();
    //    auto timestamp = std::chrono::duration_cast<std::chrono::seconds>(now.time_since_epoch()).count();
    //    message.set_timestamp(timestamp);
    //}

    //message.set_type(Protocol::EP_MessageType::TEXT);

    //message.set_roomid("1212");

    //const INT32  messageSize = message.ByteSizeLong();

    //BYTE* byte_array = new BYTE[messageSize + sizeof(PacketHeader)];
    //PacketHeader* header = reinterpret_cast<PacketHeader*>(byte_array);
    //header->id = 1;
    //header->size = messageSize + sizeof(PacketHeader); //기본 값 설정

    //if (message.SerializeToArray(byte_array + sizeof(PacketHeader), messageSize))
    //{
    //    return byte_array;
    //}
    //else
    //    return nullptr;
}
