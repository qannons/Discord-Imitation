#pragma once

//Server 코드
enum ePacketID  : UINT16
{
	CHAT_MESSAGE = 0,
	IMAGE_MESSAGE = 1
};


struct PacketHeader
{
	UINT16 size;
	UINT16 id; // 프로토콜ID (ex. 1=로그인, 2=이동요청)
};