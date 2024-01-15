#include "pch.h"
#include "Service.h"
#include "IocpCore.h"
#include "Session.h"

SessionRef Service::CreateSession()
{
	SessionRef session = make_shared<Session>();
	GIocpCore.Register(session);

	return SessionRef();
}

bool Service::AddSession(SessionRef pSession)
{
	
	return false;
}

bool Service::EraseSession(SessionRef)
{
	return false;
}
