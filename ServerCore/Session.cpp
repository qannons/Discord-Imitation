#include "pch.h"
#include "Session.h"
#include "IocpEvent.h"

HANDLE Session::GetHandle(void)
{
    return HANDLE(_socket);
}

void Session::Dispatch(IocpEvent* pIocpEvent)
{
    switch (pIocpEvent->eventType)
    {
    case eEventType::Send:
        ProcessSend();
        break;
    case eEventType::Recv:
        ProcessRecv();
        break;

    default:
        break;
    }
}

void Session::Send()
{
    RegisterSend();
}

void Session::RegisterSend()
{
    _sendEvent.owner = shared_from_this();

    WSABUF wsaBuf;
    wsaBuf.buf = (char*)_sendEvent.sendBuffer;
    DWORD numOfBytes;
    DWORD flags = 0;
    ::WSASend(_socket, &wsaBuf, 1, &numOfBytes, flags, (LPOVERLAPPED)&_sendEvent, NULL);
}

void Session::RegisterRecv()
{
    _recvEvent.Init();
    _recvEvent.owner = shared_from_this();

    WSABUF wsaBuf;
    wsaBuf.buf = (char*)_recvBuf;
    wsaBuf.len = sizeof(_recvBuf);

    DWORD recvLen;
    DWORD flag = 0;
    ::WSARecv(_socket, &wsaBuf, 1, &recvLen, &flag, (LPOVERLAPPED)&_recvEvent, NULL);
}

void Session::ProcessSend()
{
}

void Session::ProcessRecv()
{
    cout << "recv: " << _recvBuf << endl;

    _recvEvent.owner = nullptr;

    RegisterRecv();

}

void Session::Init(SOCKET pSocket, SOCKADDR_IN pAddr)
{
    _socket = pSocket;
    _addr = pAddr;
}
