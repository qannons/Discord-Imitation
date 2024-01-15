#pragma once

class Service : public enable_shared_from_this<Service>
{
protected:
	set<Session> _sessions;

public:
	SessionRef CreateSession();
	bool AddSession(SessionRef);
	bool EraseSession(SessionRef);
};

class ClientService : public Service
{

};

class ServerService : public Service
{
private:
	shared_ptr<Listener> _listener;
};