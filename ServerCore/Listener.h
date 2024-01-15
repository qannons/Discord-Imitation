#pragma once
#include "IocpCore.h"

class Listener : public IocpObject
{
private:
	SOCKET _socket;

public:
	Listener();

public:
	// IocpObject을(를) 통해 상속됨
	 HANDLE GetHandle(void) override;
	 void Dispatch() override;

public:
	void StartAccept();
};

