#pragma once

//Server �ڵ�
enum PacketID  : UINT16
{
	S_TEST = 1
};


struct PacketHeader
{
	UINT16 size;
	UINT16 id; // ��������ID (ex. 1=�α���, 2=�̵���û)
};