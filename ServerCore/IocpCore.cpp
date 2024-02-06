#include "pch.h"
#include "IocpCore.h"
#include "Session.h"
#include "IocpEvent.h"

IocpCore GIocpCore;

IocpCore::IocpCore()
{
	_handle = ::CreateIoCompletionPort(INVALID_HANDLE_VALUE, 0, 0, 0);
	ASSERT_CRASH(_handle == INVALID_HANDLE_VALUE); 
}

IocpCore::~IocpCore()
{
	CloseHandle(_handle);
}

bool IocpCore::Register(IocpObjectRef pObject)
{
	auto a = pObject->GetHandle();
	return ::CreateIoCompletionPort(pObject->GetHandle(), _handle, 0, 0);
}

void IocpCore::Dispatch()
{
	DWORD numOfBytes = 0;
	
	ULONG_PTR key;
	IocpEvent* iocpEvent = nullptr;

	if (::GetQueuedCompletionStatus(_handle, &numOfBytes, &key, (LPOVERLAPPED*)&iocpEvent, INFINITE))
	{
		IocpObjectRef object = iocpEvent->owner;
		object->Dispatch(iocpEvent);
	}
}
