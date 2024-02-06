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
//그냥 pch는 다른 프로젝트에서 참조가 불가능하다.
//그래서 CorePch를 사용함

#include <thread>
#include <vector>
#include <set>
#include <map>