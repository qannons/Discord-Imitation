#include "pch.h"

#pragma comment(lib, "ws2_32")
#include <winsock2.h>
#include <WS2tcpip.h>
#include <stdlib.h>
#include <stdio.h>

#include <thread>


#define SERVERPORT 7777
#define BUFSIZE    512

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

	SOCKET listenSocket = socket(AF_INET, SOCK_STREAM, 0);
	ASSERT_CRASH(listenSocket == INVALID_SOCKET);

	SOCKADDR_IN addr;
	addr.sin_family = AF_INET;
	addr.sin_port = htons(7777);
	addr.sin_addr.s_addr = htonl(INADDR_ANY);

	//Bind
	if (::bind(listenSocket, reinterpret_cast<SOCKADDR*>(&addr), sizeof(addr)) == SOCKET_ERROR)
		return 1;

	//Listen
	if (listen(listenSocket, SOMAXCONN) == SOCKET_ERROR)
		return 1;

	while (true)
	{
		SOCKADDR_IN clientAddr;
		int32 addrLen = sizeof(clientAddr);
		SOCKET clientSocket = accept(listenSocket, reinterpret_cast<SOCKADDR*>(&clientAddr), &addrLen);
		if (clientSocket == INVALID_SOCKET)
			return 2;
		
		char clientIP[32];
		cout << "연결 성공!" << inet_ntop(AF_INET, &clientAddr.sin_addr, clientIP, 32) << " " << clientAddr.sin_port << endl;
		
		while (true)
		{
			BYTE buf[512];
			int32 recvSize = recv(clientSocket, (char*)buf, 512, 0);

			if (recvSize <= 0)
				return 3;

			buf[recvSize] = '\0';
			cout << buf << endl;
			
			::send(clientSocket, (char*)buf, sizeof(buf) / sizeof(char), 0);
		}
	}
	WSACleanup();
	return 0;
}