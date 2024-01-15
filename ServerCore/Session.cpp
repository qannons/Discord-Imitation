#include "pch.h"
#include "Session.h"
#include "IocpEvent.h"

HANDLE Session::GetHandle(void)
{
    return HANDLE(_socket);
}

void Session::Dispatch()
{
}

void Session::OnSend()
{
}

void Session::OnRecv()
{
}

void Session::Send()
{
    RegisterSend();
}

void Session::RegisterSend()
{
    WSABUF wsaBuf;
    wsaBuf.buf = (char*)_sendEvent->sendBuffer;
    DWORD numOfBytes;
    DWORD flags = 0;
    ::WSASend(_socket, &wsaBuf, 1, &numOfBytes, flags, _sendEvent, NULL);
}

void Session::RegisterRecv()
{
}

void Session::ProcessSend()
{
}

void Session::ProvcessRecv()
{
}

void Session::Init(SOCKET pSocket, SOCKADDR_IN pAddr)
{
    _socket = pSocket;
    _addr = pAddr;
}
