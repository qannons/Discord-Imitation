#include "pch.h"
#include "IocpCore.h"
#include "IocpEvent.h"
#include "Session.h"

IocpCore GIocpCore;

IocpCore::IocpCore()
{
	_iocpHandle = ::CreateIoCompletionPort(INVALID_HANDLE_VALUE, 0, 0, 0);
	ASSERT_CRASH(_iocpHandle == INVALID_HANDLE_VALUE);
}

IocpCore::~IocpCore()
{
	::CloseHandle(_iocpHandle);
}


bool	IocpCore::Register(IocpObjectRef iocpObject)
{
	return CreateIoCompletionPort(iocpObject->GetHandle(), _iocpHandle, 0, 0);

}

bool IocpCore::Dispatch(UINT32 timeoutMs)
{
	DWORD numOfBytes = 0;
	ULONG_PTR key = 0;
	IocpEvent* iocpEvent = nullptr;

	if (::GetQueuedCompletionStatus(_iocpHandle, OUT & numOfBytes, OUT & key, OUT reinterpret_cast<LPOVERLAPPED*>(&iocpEvent), timeoutMs))
	{
		IocpObjectRef iocpObject = iocpEvent->owner;
		iocpObject->Dispatch(iocpEvent, numOfBytes);
	}
	else
	{
		INT32 errCode = ::WSAGetLastError();
		switch (errCode)
		{
		case WAIT_TIMEOUT:
			return false;
		default:
			// TODO : �α� ���
			IocpObjectRef iocpObject = iocpEvent->owner;
			iocpObject->Dispatch(iocpEvent, numOfBytes);
			break;
		}
	}

	return true;
}

void IocpCore::Broadcast(BYTE* buffer)
{
	lock_guard<mutex> lock(_mutex);
	for (SessionRef session : _sessions)
	{
		session->Send(buffer, sizeof(buffer));
	}
}

void IocpCore::Add(SessionRef session)
{
	lock_guard<mutex> lock(_mutex);
	_sessions.insert(session);
}

void IocpCore::Remove(SessionRef session)
{
	lock_guard<mutex> lock(_mutex);
	_sessions.erase(session);
}
