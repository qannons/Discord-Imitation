#pragma once
#include "Protocol.pb.h"

class ServerPacketHandler
{
public:
	//static BYTE* Handle_P_ChatMessage(Protocol::P_ChatMessage& pkt, UINT16 pktId);
	static BYTE* Handle_P_ChatMessage(BYTE* buffer, INT32 len);

	static BYTE* Handle_P_ImageMessage(BYTE* buffer, INT32 len);
};

