#pragma once
#define WIN32_LEAN_AND_MEAN // ���� ������ �ʴ� ������ Windows ������� �����մϴ�.
#include <Windows.h>
#include <winsock2.h>
#include <mswsock.h>
#include <ws2tcpip.h>
#pragma comment(lib, "ws2_32.lib")

#include <memory>
#include <iostream>
#include <vector>
#include <set>
#include <atomic>
#include <array>
#include <queue>
#include <mutex>

#include "TLS.h"
#include "Types.h"
#include "Macro.h"

using namespace std;