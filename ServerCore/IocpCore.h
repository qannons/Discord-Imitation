#pragma once

class IocpObject : public enable_shared_from_this<IocpObject>
{
public:
	virtual HANDLE GetHandle(void) abstract;
	virtual void Dispatch() abstract;
};

class IocpCore
{
private:
	HANDLE _handle;

public:
	IocpCore();
	~IocpCore();

public:
	bool Register(shared_ptr<IocpObject> pObject);
	void Dispatch();
};

extern IocpCore GIocpCore;
