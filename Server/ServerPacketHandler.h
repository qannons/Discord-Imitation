#pragma once
#include "Protocol.pb.h"

class ServerPacketHandler
{
public:
	static BYTE* Handle_P_ChatMessage(Protocol::ChatMessage& pkt, UINT16 pktId);
	static BYTE* Handle_P_ChatMessage2(Protocol::ChatMessage& pkt);
};

