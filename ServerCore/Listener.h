#pragma once
#include "IocpCore.h"

class Listener : public IocpObject
{
private:
	SOCKET _socket;

public:
	Listener();

public:
	// IocpObject��(��) ���� ��ӵ�
	 HANDLE GetHandle(void) override;
	 void Dispatch() override;

public:
	void StartAccept();
};

