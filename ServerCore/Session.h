#pragma once
#include "IocpCore.h"
#include "IocpEvent.h"
class Session : public IocpObject
{
public:
	BYTE _recvBuf[512];

private:
	SOCKET _socket;
	SOCKADDR_IN _addr;

private:
	 RecvEvent _recvEvent;
	 SendEvent _sendEvent;

public:
	// IocpObject을(를) 통해 상속됨
	virtual HANDLE GetHandle(void) override;
	virtual void Dispatch(class IocpEvent* pIocpEvent) override;

public:
	void Send();

private:
	void RegisterSend();
	void RegisterRecv();

	void ProcessSend();
	void ProcessRecv();


public:
	void Init(SOCKET pSocket, SOCKADDR_IN pAddr);
};

