#pragma once

//Server �ڵ�
enum ePacketID  : UINT16
{
	CHAT_MESSAGE = 0,
	IMAGE_MESSAGE = 1
};


struct PacketHeader
{
	UINT16 size;
	UINT16 id; // ��������ID (ex. 1=�α���, 2=�̵���û)
};