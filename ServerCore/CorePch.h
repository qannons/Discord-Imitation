#pragma once

#include "Types.h"
#include "CoreMacro.h"
//#include "CoreTLS.h"
//#include "CoreGlobal.h"

#include <iostream>
using namespace std;

#include <winsock2.h>
#include <ws2tcpip.h>
#include <mswsock.h>
#pragma comment(lib, "ws2_32.lib")

#include <windows.h>
//�׳� pch�� �ٸ� ������Ʈ���� ������ �Ұ����ϴ�.
//�׷��� CorePch�� �����

#include <thread>
#include <vector>
#include <set>
#include <map>