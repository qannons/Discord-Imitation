#pragma once
#include "IocpCore.h"
#include "IocpEvent.h"
#include "RecvBuffer.h"
#include "Protocol.pb.h"
#include "PacketHeader.h"

class Session : public IocpObject
{
	friend Listener;
public:
	// IocpObject��(��) ���� ��ӵ�
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
		INT32 processLen = 0;

		while (true)
		{
			INT32 dataSize = len - processLen;
			 //�ּ��� ����� �Ľ��� �� �־�� �Ѵ�
			if (dataSize < sizeof(PacketHeader))
				break;

			PacketHeader header = *(reinterpret_cast<PacketHeader*>(&buffer[processLen]));
			// ����� ��ϵ� ��Ŷ ũ�⸦ �Ľ��� �� �־�� �Ѵ�
			if (dataSize < header.size)
				break;

			 //��Ŷ ���� ����
			HandlePacket(&buffer[processLen], header.size);

			processLen += header.size;
		}

		return processLen;
	}

	void HandlePacket(BYTE* buffer, INT32 len)
	{
		Protocol::ChatMessage pkt;

		if (pkt.ParseFromArray(buffer+sizeof(PacketHeader), len - sizeof(PacketHeader)))
		{
			cout << "Sender ID: " << pkt.sender().userid() << endl;
			cout << "Username: " << pkt.sender().username() << endl;
			cout << "Content: " << pkt.content() << endl;
			cout << "Timestamp: " << pkt.timestamp() << endl;
			cout << "Message Type: " << pkt.type() << endl;
			cout << "Room ID: " << pkt.roomid() << endl;
				
			//tmp;
			GIocpCore.Broadcast(buffer, len);
		}
		else
		{
			return;
		}
		//GIocpCore.Broadcast(pkt, 1);
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

