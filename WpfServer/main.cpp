#include "pch.h"

#pragma comment(lib, "ws2_32")
#include <winsock2.h>
#include <WS2tcpip.h>
#include <stdlib.h>
#include <stdio.h>

#include <thread>


#define SERVERPORT 7777
#define BUFSIZE    512

// 소켓 함수 오류 출력 후 종료
void err_quit(const char* msg)
{
	LPVOID lpMsgBuf;
	FormatMessage(
		FORMAT_MESSAGE_ALLOCATE_BUFFER | FORMAT_MESSAGE_FROM_SYSTEM,
		NULL, WSAGetLastError(),
		MAKELANGID(LANG_NEUTRAL, SUBLANG_DEFAULT),
		(LPTSTR)&lpMsgBuf, 0, NULL);

	MessageBox(NULL, (LPCTSTR)lpMsgBuf, (LPTSTR)msg, MB_ICONERROR);
	LocalFree(lpMsgBuf);
	exit(1);
}

// 소켓 함수 오류 출력
void err_display(const char* msg)
{
	LPVOID lpMsgBuf;
	FormatMessage(
		FORMAT_MESSAGE_ALLOCATE_BUFFER | FORMAT_MESSAGE_FROM_SYSTEM,
		NULL, WSAGetLastError(),
		MAKELANGID(LANG_NEUTRAL, SUBLANG_DEFAULT),
		(LPTSTR)&lpMsgBuf, 0, NULL);
	printf("[%s] %s", msg, (char*)lpMsgBuf);
	LocalFree(lpMsgBuf);
}

void PacketProcess(SOCKET sock, char* pPacket)
{
	PktCalcuRes res;
	res.Id = (short)PACKET_ID::CALCU_RES;
	res.TotalSize = (short)sizeof(PktCalcuRes);
	res.num = 0;

	PktHeader* pHeader = (PktHeader*)pPacket;

	if (pHeader->Id == (short)PACKET_ID::CALCU_2_REQ)
	{
		PktCalcu2Req* req = (PktCalcu2Req*)pPacket;

		res.num = 1;
	}
	else if (pHeader->Id == (short)PACKET_ID::CALCU_3_REQ)
	{
		PktCalcu3Req* req = (PktCalcu3Req*)pPacket;

		res.num = 2;
	}

	send(sock, (char*)&res, res.TotalSize, 0);
}

int main(int argc, char* argv[])
{
	int retval;

	// 윈속 초기화
	WSADATA wsa;
	if (WSAStartup(MAKEWORD(2, 2), &wsa) != 0)
		return 1;

	// socket()
	SOCKET listen_sock = socket(AF_INET, SOCK_STREAM, 0);
	if (listen_sock == INVALID_SOCKET) err_quit("socket()");

	// bind()
	SOCKADDR_IN serveraddr;
	ZeroMemory(&serveraddr, sizeof(serveraddr));
	serveraddr.sin_family = AF_INET;
	serveraddr.sin_addr.s_addr = htonl(INADDR_ANY);
	serveraddr.sin_port = htons(SERVERPORT);
	retval = bind(listen_sock, (SOCKADDR*)&serveraddr, sizeof(serveraddr));
	if (retval == SOCKET_ERROR) err_quit("bind()");

	// listen()
	retval = listen(listen_sock, SOMAXCONN);
	if (retval == SOCKET_ERROR) {
		err_quit("listen()");
	}

	while (1) {
		SOCKADDR_IN clientaddr;
		int addrlen = sizeof(clientaddr);

		SOCKET client_sock = accept(listen_sock, (SOCKADDR*)&clientaddr, &addrlen);
		if (client_sock == INVALID_SOCKET) {
			err_display("accept()");
			break;
		}

		// 접속한 클라이언트 정보 출력
		char clientIP[33] = { 0, };
		inet_ntop(AF_INET, &(clientaddr.sin_addr), clientIP, 33 - 1);
		printf("\n[TCP 서버] 클라이언트 접속: IP 주소=%s, 포트 번호=%d\n", clientIP, ntohs(clientaddr.sin_port));


		// 클라이언트와 데이터 통신
		char buf[BUFSIZE + 1];

		while (1) {
			int recvSize = recv(client_sock, buf, BUFSIZE, 0);
			if (recvSize == SOCKET_ERROR) {
				err_display("recv()");
				break;
			}
			else if (recvSize == 0)
				break;
			

			// 받은 크기 출력
			printf("[recv:%d]\n", recvSize);

			int readPos = 0;
			while (recvSize >= PACKET_HEADER_SIZE)
			{
				PktHeader* pHeader = (PktHeader*)&buf[readPos];

				if (pHeader->TotalSize > recvSize)
				{
					break;
				}

				PacketProcess(client_sock, &buf[readPos]);

				readPos += pHeader->TotalSize;
				recvSize -= pHeader->TotalSize;
			}
		}

		closesocket(client_sock);
		printf("[TCP 서버] 클라이언트 종료: IP 주소=%s, 포트 번호=%d\n", clientIP, ntohs(clientaddr.sin_port));
	}

	closesocket(listen_sock);
	WSACleanup();
	return 0;
}

