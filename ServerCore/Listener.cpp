#include "pch.h"
#include "Listener.h"
#include "IocpCore.h"
#include "Session.h"
#include "IocpEvent.h"
#include "SessionManager.h"

Listener::Listener()
{
	_socket =  ::socket(AF_INET, SOCK_STREAM, 0);
	ASSERT_CRASH(_socket == INVALID_SOCKET)
		

}

HANDLE Listener::GetHandle(void)
{
	return HANDLE(_socket);
}

void Listener::Dispatch(IocpEvent* pIocpEvent)
{
	
}

void Listener::StartAccept()
{
	SOCKADDR_IN serverAddr; // IPv4
	::memset(&serverAddr, 0, sizeof(serverAddr));
	serverAddr.sin_family = AF_INET;
	serverAddr.sin_addr.s_addr = ::htonl(INADDR_ANY); //< 니가 알아서 해줘
	serverAddr.sin_port = ::htons(7777); // 80 : HTTP
	if (::bind(_socket, (SOCKADDR*)&serverAddr, sizeof(SOCKADDR_IN)) == SOCKET_ERROR)
	{
		int32 errCode = WSAGetLastError();

		cout << "Bind Fail : " << errCode;
		return;
	}

	if (::listen(_socket, SOMAXCONN) == SOCKET_ERROR)
	{
		cout << "Listen Fail";
		return;
	}

	while (true) 
	{
		SOCKADDR_IN addr;
		int32 addrLen = sizeof(addr);
		SOCKET clientSocket = ::accept(_socket, (SOCKADDR*)&addr, &addrLen);
		if (clientSocket == INVALID_SOCKET)
		{
			continue;
		}


		DWORD recvLen = 0;
		DWORD flags = 0;

		RecvEvent* recvEvent = new RecvEvent();

		SessionRef session = make_shared<Session>();
		session->Init(clientSocket, addr);
		recvEvent->owner = session;

		WSABUF wsaBuf;
		wsaBuf.buf = (char*)session->_recvBuf;
		wsaBuf.len = 100;

		GSessionManager.AddSession(session);
		
		::WSARecv(clientSocket, &wsaBuf, 1, &recvLen, &flags, recvEvent, NULL);
	}
}

void ProcessAccept()
{
	
}
