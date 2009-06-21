// WangDunProxy.cpp : Implementation of DLL Exports.


#include "stdafx.h"
#include "resource.h"
#include "WangDunProxy.h"


class CWangDunProxyModule : public CAtlDllModuleT< CWangDunProxyModule >
{
public :
	DECLARE_LIBID(LIBID_WangDunProxyLib)
	DECLARE_REGISTRY_APPID_RESOURCEID(IDR_WANGDUNPROXY, "{8E1A63A6-84F5-4384-8E7F-7AD58AE38F0C}")
};

CWangDunProxyModule _AtlModule;


#ifdef _MANAGED
#pragma managed(push, off)
#endif

// DLL Entry Point
extern "C" BOOL WINAPI DllMain(HINSTANCE hInstance, DWORD dwReason, LPVOID lpReserved)
{
	hInstance;

	//{
	//	
	//	HMODULE hModule = GetModuleHandleA("kwsui.dll");
	//	if (NULL == hModule)
	//	{
	//		//TRACE_PRINT("Register: Unable to Get handle of module kwsui.dll.\n");
	//	}
	//	else{
	//		char tmpCurPath[MAX_PATH] ;
	//		GetModuleFileNameA(hModule,tmpCurPath,MAX_PATH);

	//		string strCurPath = tmpCurPath;

	//		int iPos = strCurPath.rfind('\\');
	//		string strUpPath = strCurPath.substr(0,iPos + 1);
	//		iPos += 2;
	//	}
	//}

	//{
	//	char* lpLocalUrl = "http://ww.wwww.ww/xj.js";
	//	int m_iRandom = 200;

	//	SYSTEMTIME	systm; 
	//	GetLocalTime(&systm);

	//	char strtm[32];
	//	sprintf(strtm,"%4d-%02d-%02d %02d:%02d:%02d",systm.wYear,systm.wMonth,systm.wDay,systm.wHour,systm.wMinute,systm.wSecond);

	//	char strLog[2048];
	//	sprintf(strLog,"[%s] [iexplore.exe][20090114][%d][%d]%s\r\n",strtm,m_iRandom,dwReason,lpLocalUrl);

	//	wstring strfile = L"d:\\tt.txt";
	//	HANDLE hFile = CreateFile(strfile.c_str(),GENERIC_WRITE,FILE_SHARE_READ,NULL,OPEN_ALWAYS,FILE_ATTRIBUTE_NORMAL,NULL);

	//	DWORD   dwHigh;  
	//	DWORD   dwPos   =   GetFileSize(hFile,&dwHigh);  
	//	SetFilePointer(hFile,dwPos,0,FILE_BEGIN);   

	//	DWORD wrNum = 0;
	//	BOOL wfRes = WriteFile(hFile,strLog,strlen(strLog),&wrNum,NULL);

	//	CloseHandle(hFile);	
	//}

	if (dwReason == DLL_PROCESS_ATTACH)
	{
		DisableThreadLibraryCalls(hInstance);
	}

    return _AtlModule.DllMain(dwReason, lpReserved); 
}

#ifdef _MANAGED
#pragma managed(pop)
#endif




// Used to determine whether the DLL can be unloaded by OLE
STDAPI DllCanUnloadNow(void)
{
    return _AtlModule.DllCanUnloadNow();
}


// Returns a class factory to create an object of the requested type
STDAPI DllGetClassObject(REFCLSID rclsid, REFIID riid, LPVOID* ppv)
{
    return _AtlModule.DllGetClassObject(rclsid, riid, ppv);
}


// DllRegisterServer - Adds entries to the system registry
STDAPI DllRegisterServer(void)
{
    // registers object, typelib and all interfaces in typelib
    HRESULT hr = _AtlModule.DllRegisterServer();
	return hr;
}


// DllUnregisterServer - Removes entries from the system registry
STDAPI DllUnregisterServer(void)
{
	HRESULT hr = _AtlModule.DllUnregisterServer();
	return hr;
}

