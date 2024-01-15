#pragma once
#include "IocpCore.h"
class Session : public IocpObject
{
private:
	SOCKET _socket;
	BYTE _recvBuf[512];
	SOCKADDR_IN _addr;

private:
	class RecvEvent* _recvEvent;
	class  SendEvent* _sendEvent;

public:
	// IocpObject을(를) 통해 상속됨
	virtual HANDLE GetHandle(void) override;
	virtual void Dispatch() override;

protected:
	virtual void OnSend();
	virtual void OnRecv();

public:
	void Send();

private:
	void RegisterSend();
	void RegisterRecv();

	void ProcessSend();
	void ProvcessRecv();


public:
	void Init(SOCKET pSocket, SOCKADDR_IN pAddr);
};

