#pragma once
#include "IocpCore.h"
#include "IocpEvent.h"
#include "RecvBuffer.h"

class Session : public IocpObject
{
	friend Listener;
public:
	// IocpObject을(를) 통해 상속됨
	virtual HANDLE GetHandle() override;
	virtual void Dispatch(IocpEvent* iocpEvent, INT32 numOfBytes) override;

public:
	Session();
	~Session();

	void SetAddr(SOCKADDR_IN addr) { _addr = addr; }
	void SetSocket(SOCKET socket) { _socket = socket; }


	void	Send(BYTE* buffer, INT32 len);
	void	Disconnect(const WCHAR* cause);

private:
	void RegisterSend(SendEvent* sendEvent);
	void ProcessSend(SendEvent* sendEvent, INT32 numOfBytes);

	void HandleError(INT32 errorCode);

	void RegisterRecv();
	void ProcessRecv(INT32 numOfBytes);

	void Connect();
	bool RegisterDisconnect();
	void ProcessDisconnect();

	INT32 OnRecv(BYTE* buffer, INT32 len) 
	{
		WCHAR tmp[1024];
		memcpy(tmp, buffer, len);
		std::wcout.imbue(std::locale("korean"));

		wcout << tmp;
		return len;
	}

private:
	SOCKET _socket;
	SOCKADDR_IN _addr;
	RecvEvent _recvEvent;
	SendEvent _sendEvent;
	DisconnectEvent _disconnectEvent;
	atomic<bool> _connected = false;
	mutex _mutex;
	RecvBuffer _recvBuffer;
	BYTE buf[1024] = "Hello World!";
	queue<SendBufferRef>	_sendQueue;
};

