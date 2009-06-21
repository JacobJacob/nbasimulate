// BlockNotifySample.cpp : Defines the entry point for the DLL application.
//

#include "stdafx.h"
#include "KsWebshieldNotify.h"
#include "KwsBlockCallbackImpl.h"

#ifdef _MANAGED
#pragma managed(push, off)
#endif

typedef HRESULT(__stdcall *GetClassObjectType)(	
    /*const*/ REFIID riid,
    void  **ppvObject
    );

VOID
TRACE_PRINT(
    PCHAR szFormat,
    ...
    )
{
    va_list  marker;
    char buf[500];
    int n;

    va_start( marker, szFormat );
    n = _vsnprintf_s(buf, 500, sizeof(buf), szFormat, marker );
    va_end( marker);

    if (n == -1) {
        buf[sizeof(buf)-1] = '\0';
    }

    OutputDebugStringA( buf );
    return;
}

BOOL RegisterBlockNotifyRoutine()
{
    BOOL               bRetCode          = TRUE;
    HMODULE            hModule           = NULL;
    GetClassObjectType pfnGetClassObject = NULL;
    IKXWebShieldNotify *piNotify         = NULL;
    HRESULT            hr                = S_OK;

    hModule = GetModuleHandleA("kwsui.dll");
    if (NULL == hModule)
    {
        TRACE_PRINT("Register: Unable to Get handle of module kwsui.dll.\n");
        bRetCode = FALSE;
        goto Exit0;
    }

    pfnGetClassObject = (GetClassObjectType)GetProcAddress(hModule, "GetClassObject");
    if (NULL == pfnGetClassObject)
    {
        TRACE_PRINT("Register: Unable to get address of function GetClassObject.\n");
        bRetCode = FALSE;
        goto Exit0;
    }

    hr = pfnGetClassObject(__uuidof(IKXWebShieldNotify), (VOID **)&piNotify);
    if (FAILED(hr) || NULL == piNotify)
    {
        TRACE_PRINT("Register: Unable to get interface IKXWebShieldNotify.\n");
        bRetCode = FALSE;
        goto Exit0;
    }

    bRetCode = piNotify->RegisterBlockCallback(CKwsBlockCallbackImpl::GetInstance());
    piNotify->Release();

Exit0:

    return bRetCode;
}

/*
BOOL APIENTRY DllMain( HMODULE hModule,
                       DWORD  ul_reason_for_call,
                       LPVOID lpReserved
					 )
{
    if (!RegisterBlockNotifyRoutine())
    {
        TRACE_PRINT("Sample: Register block notify routine failed.\n");
    }
    return TRUE;
}
*/
#ifdef _MANAGED
#pragma managed(pop)
#endif

