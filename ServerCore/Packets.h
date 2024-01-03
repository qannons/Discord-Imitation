#pragma once

enum class OP_TYPE : short
{
	ADD = 0,
	SUB = 1,
	Mul = 2,
	Div = 3,
};

enum class PACKET_ID : short
{
	CALCU_2_REQ = 21,
	CALCU_3_REQ = 22,

	CALCU_RES = 31,
};

struct PktHeader
{
	short totalSize;
	short id;
};

struct PktCalcuRes : PktHeader
{
	int num;
};
#pragma pack(pop)