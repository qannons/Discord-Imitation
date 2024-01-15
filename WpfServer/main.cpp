#include "pch.h"
#include "Listener.h"
#include "IocpCore.h"
#include <string>
struct MyStruct
{
	int size;
	string data;
};


int main(void)
{
	// 윈속 초기화
	WSADATA wsa;
	if (WSAStartup(MAKEWORD(2, 2), &wsa) != 0)
		return 1;

	vector<thread> threads;
	Listener listener;
	

	for (int i = 0; i < 2; i += 1)
	{
		threads.push_back(thread([=]() 
			{
				while (true)
				{
					GIocpCore.Dispatch();
				}
			}));
	}


	listener.StartAccept();

	for (thread& t : threads)
		t.join();
	
	WSACleanup();
	return 0;
}

