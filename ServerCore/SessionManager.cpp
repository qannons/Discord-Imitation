#include "pch.h"
#include "SessionManager.h"
#include "Session.h"

SessionManager GSessionManager;

void SessionManager::AddSession(SessionRef pSession)
{
	_sessions.insert(pSession);

	if (GIocpCore.Register(pSession) == false)
	{
		int32 errCode = WSAGetLastError();
		cout << "Register Fail" << errCode;
		return;
	}

}
