#pragma once

class SessionManager
{
private:
	set<SessionRef> _sessions;

public:
	void AddSession(SessionRef pSession);
};

extern SessionManager GSessionManager;
