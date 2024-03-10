#pragma once

//Server 코드
enum PacketID  : UINT16
{
	S_TEST = 1
};


struct PacketHeader
{
	UINT16 size;
	UINT16 id; // 프로토콜ID (ex. 1=로그인, 2=이동요청)
};