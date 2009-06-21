/************************************************************************
* @file      : ckswebshiledcallback.cpp
* @author    : ChenZhiQiang <chenzhiqiang@kingsoft.com>
* @date      : 17/12/2008 PM 4:40:29
* @brief     : 
*
* $Id: $
/************************************************************************/

#include "stdafx.h"
#include "ckswebshiledcallback.h"
#include <assert.h>
#include <shlwapi.h>
#include "kwsui.h"

#include "GetExploitURLs.h"

#include <sys/stat.h>
#include <time.h>
#include <iomanip>
#include <direct.h>
#include <locale.h>
#include <stdlib.h>
//#include <atlutil.h>

#include "definestringresource.h"
#include "../include/simpleipc/KwsIPCWarpper.h"
#include "../include/common/commonfun.h"
#include "../include/infocul/popreasondef.h"

#pragma comment(lib, "shlwapi.lib")



// -------------------------------------------------------------------------


int KwsShowPopo( int nPopoStyle, const wchar_t* pwszMsg, const wchar_t* pszBtnText, INT nBlockReason = -1)
{
	return ShowPopo2( nPopoStyle, pwszMsg, (pszBtnText != NULL) , nBlockReason);
}



// -------------------------------------------------------------------------

HRESULT STDMETHODCALLTYPE CKSWebShiledCallback::QueryInterface( 
	REFIID riid,
	void  **ppvObject)
{
	if (!ppvObject)
	{
		return E_POINTER;
	}
	if (riid == __uuidof(IUnknown))
	{
		*ppvObject = reinterpret_cast<IUnknown*>(this);
		return S_OK;
	}
	else if (riid == __uuidof(IKXWebShiledCallback))
	{
		*ppvObject = static_cast<IKXWebShiledCallback*>(this);
		return S_OK;
	}
    else if (riid == __uuidof(IKXWebShieldNotify))
    {
        *ppvObject = static_cast<IKXWebShieldNotify*>(this);
        return S_OK;
    }
	return E_NOINTERFACE;
}

ULONG STDMETHODCALLTYPE CKSWebShiledCallback::AddRef()
{
	assert(long(m_RefCnt) >= 0);
	long lRefCnt = ++m_RefCnt;
	return lRefCnt;
}

ULONG STDMETHODCALLTYPE CKSWebShiledCallback::Release()
{
	assert(long(m_RefCnt) > 0);   
	long lRefCnt = --m_RefCnt;
	if (0 == lRefCnt)  
	{
		delete this;
	}

	return lRefCnt;
}

CKSWebShiledCallback* CKSWebShiledCallback::GetInstance()
{
	//CKSWebShiledCallback *pIns = new CKSWebShiledCallback();
    static CKSWebShiledCallback Ins;
	Ins.AddRef();

	return &Ins;
}
// -------------------------------------------------------------------------


EXTERN_C IMAGE_DOS_HEADER __ImageBase;

CKSWebShiledCallback::CKSWebShiledCallback()
{
	m_RefCnt = 0;
	m_bInitFlag = FALSE;
    m_pBlockCallback = NULL;
}

CKSWebShiledCallback::~CKSWebShiledCallback()
{
	//Uninitialize();
}


HRESULT __stdcall CKSWebShiledCallback::Initialize()
{
	srand( (unsigned)time( NULL ) );

	//SetWindowStyle();

	WCHAR szFilePath[MAX_PATH] = {0};
	GetModuleFileName((HINSTANCE)&__ImageBase, szFilePath, MAX_PATH);
	PathRemoveFileSpec(szFilePath);
	lstrcat(szFilePath, L"\\KANTray.dll");

	TCHAR szPath[MAX_PATH] = {0};
	GetModuleFileName(NULL, szPath, MAX_PATH);

	WCHAR oldUserRunFilePath[MAX_PATH] = {0};
	GetModuleFileName((HINSTANCE)&__ImageBase, oldUserRunFilePath, MAX_PATH);
	PathRemoveFileSpec(oldUserRunFilePath);
	lstrcat(oldUserRunFilePath, L"\\userrunlog.log");

	// 日志文件放到用户目录下去
	WCHAR szUserRunFilePath[MAX_PATH] = {0};
	BOOL bRet = GetAllUserKWSLogPath(szUserRunFilePath, MAX_PATH);
	wcscat_s(szUserRunFilePath, MAX_PATH, L"userrunlog.log");

	// 临时代码，由于userrunlog更改了路径，如果新的文件不存在，老的文件存在，则拷贝过来
	/*if (IsFileExist(oldUserRunFilePath) && !IsFileExist(szUserRunFilePath))
	{
		CopyFile(oldUserRunFilePath, szUserRunFilePath, TRUE);
	}*/

	WCHAR szUrlFilePath[MAX_PATH] = {0};
	bRet = GetAllUserKWSLogPath(szUrlFilePath, MAX_PATH);
	wcscat_s(szUrlFilePath, MAX_PATH, L"urlindex.dat");

	/////////////////////////////////////////////////////////////////////////

	_wsetlocale( LC_ALL, L"chs" );

	m_userLog.Create(szUserRunFilePath);
	m_urlLog.Create(szUrlFilePath);


	//////////////////////////////////////////////////////////////////////////

	//////////////////////////////////////////////////////////////////////////

	LPWSTR pos = wcsrchr(szPath, L'\\');
	m_strCurrentProcessName = pos + 1;

    m_bInitFlag = TRUE;

	// 将当前被保护的进程通知服务
	WCHAR wcsProtectProgram[MAX_PATH] = {0};
	GetModuleFileName(NULL, wcsProtectProgram, MAX_PATH);
	DWORD dwPid = GetCurrentProcessId();
	// 进程间通讯
	KwsSetProgramPath(wcsProtectProgram, dwPid);

	return S_OK;
}


HRESULT __stdcall CKSWebShiledCallback::Uninitialize()
{
	m_bInitFlag = FALSE;
	return S_OK;
}

void __stdcall CKSWebShiledCallback::NotifyInfo(
	LPCWSTR szMessage,
	LPCWSTR lpszCaption,
	int nReason
	)
{
	unsigned int nRandNumber = 0;
	if (!m_bInitFlag)
		return;
#if 0
    SetWindowRedStyle(nReason);
    //::Sleep(1000);

	CString strInfo;
	strInfo.Format(szMessage, m_strCurrentProcessName);

	m_userLog.Log(L"%s",strInfo.GetBuffer());
	strInfo.ReleaseBuffer();

    //if (m_pTrayPopService)
    {
		if (nReason == NET_PHISHING)
		{
			//m_pTrayPopService->PopoMessage(1, strInfo, POPMSG_CAPTION, NULL, NULL);
			KwsShowPopo( 1, strInfo, NULL );

		}
		else
		{
			//KwsShowPopo( 2, strInfo, NULL );
			KwsShowPopo( 2, strInfo, NULL );
		}
        
    }
	srand( (unsigned)time( NULL ) );
	nRandNumber = rand();

    _DoRecordURL(nReason, nRandNumber);
	if (nRandNumber == 0)
	{
		nRandNumber++;
	}
    int nReasonEx = 0;

    if (nReason == BLOCK_NET_ADRESS_SPITE || nReason == BLOCK_NET_ADRESS_SPITE_REGEX_MATCH)
    { 
        if (lpszCaption)
        {
            wstring veUrl;
            if (nReason == BLOCK_NET_ADRESS_SPITE)
            {
                nReasonEx = BLOCK_NET_ADRESS_SPITE_MATCH_SEND;
            }
            if (nReason == BLOCK_NET_ADRESS_SPITE_REGEX_MATCH)
            {
                nReasonEx = BLOCK_NET_ADRESS_SPITE_REGEX_MATCH_SEND;
            }
            veUrl = UrlDecode(lpszCaption);
            
			m_urlLog.Log(L"[%s][%s][%d][%d]%s",
                m_strCurrentProcessName,
                RUL_INDEX_VERSON, 
                nReasonEx, 
				nRandNumber,
                veUrl.c_str());
        }
    }
#endif

    CString strInfo;
    //strInfo.Format(szMessage, m_strCurrentProcessName);

    strInfo.Format(L"发现%s%s", m_strCurrentProcessName, szMessage);

    m_userLog.Log(L"%s",strInfo.GetBuffer());
    strInfo.ReleaseBuffer();

    DoNotify(strInfo, NULL, lpszCaption, nReason, RECORD_BUSY);
}


void __stdcall CKSWebShiledCallback::NotifyBlockProcessInfo(
	LPCWSTR lpszFileName,
	LPCWSTR lpszCommandLine,
	int nReason
	)
{
	if (!m_bInitFlag)
		return;
	if (!lpszFileName)
		return;
#if 0
	SetWindowRedStyle(nReason);

	CString strInfo;
	if (lpszCommandLine)
	{
		strInfo.Format(BLOCKPROCESS_CL, m_strCurrentProcessName, lpszFileName, lpszCommandLine);
	}
	else
	{
		strInfo.Format(BLOCKPROCESS, m_strCurrentProcessName, lpszFileName);
	}
	//if (m_pTrayPopService)
	{
		KwsShowPopo( 2, strInfo, NULL );
	}
	m_userLog.Log(L"%s", strInfo.GetBuffer());
	strInfo.ReleaseBuffer();

	_DoRecordURL(nReason, 0);
#endif

    CString strInfo;

    if (lpszCommandLine)
    {
        strInfo.Format(BLOCKPROCESS_CL, m_strCurrentProcessName, lpszFileName, lpszCommandLine);
    }
    else
    {
        strInfo.Format(BLOCKPROCESS, m_strCurrentProcessName, lpszFileName);
    }

    m_userLog.Log(L"%s", strInfo.GetBuffer());
    strInfo.ReleaseBuffer();

    DoNotify(strInfo, NULL, NULL, nReason, RECORD_BUSY);
}

void __stdcall CKSWebShiledCallback::NotifyBlockDownloadFile(
	LPCWSTR lpszFileName,
	LPCWSTR lpszURL,
	int nReason
	)
{
	unsigned int nRandNumber = 0;
	if (!m_bInitFlag)
		return;
#if 0
	SetWindowRedStyle(nReason);

	CString strInfo;
	if (lpszFileName && lpszURL)
	{
		strInfo.Format(BLOCKPDOWNFILE, m_strCurrentProcessName, lpszURL, lpszFileName);
		//if (m_pTrayPopService)
		{
			KwsShowPopo( 2, strInfo, NULL );
		}
		m_userLog.Log(L"%s", strInfo.GetBuffer());
		strInfo.ReleaseBuffer();

		//m_urlLog.Log(L"%s",lpszURL);

		srand( (unsigned)time( NULL ) );
		nRandNumber = rand();
		if (nRandNumber == 0)
		{
			nRandNumber++;
		}
		m_urlLog.Log(L"[%s][%s][%d][%d]%s",
			m_strCurrentProcessName,
			RUL_INDEX_VERSON, 
			nReason, 
			nRandNumber,
			lpszURL);
	}
	_DoRecordURL(nReason, nRandNumber);
#endif
	
    CString strInfo;
    if (lpszFileName && lpszURL)
    {
        strInfo.Format(BLOCKPDOWNFILE, m_strCurrentProcessName, lpszURL, lpszFileName);
       
        m_userLog.Log(L"%s", strInfo.GetBuffer());
        strInfo.ReleaseBuffer();       
    }
    else
    {
        strInfo = "";
    }

    DoNotify(strInfo, NULL, lpszURL, nReason, RECORD_BUSY);
}

void __stdcall CKSWebShiledCallback::NotifyBlockInternetOpenUrl(
	LPCWSTR lpszUrl,
	int nReason
	)
{
	unsigned int nRandNumber = 0;
	if (!m_bInitFlag)
		return;
#if 0
	SetWindowRedStyle(nReason);

	CString strInfo;
	if (lpszUrl)
	{
		strInfo.Format(BLOCKINTERNETOPENURL, m_strCurrentProcessName,  lpszUrl);
		//if (m_pTrayPopService)
		{
			KwsShowPopo( 2, strInfo, NULL );
		}
		m_userLog.Log(L"%s", strInfo.GetBuffer());
		strInfo.ReleaseBuffer();

		srand( (unsigned)time( NULL ) );
		nRandNumber = rand();
		if (nRandNumber == 0)
		{
			nRandNumber++;
		}
		m_urlLog.Log(L"[%s][%s][%d][%d]%s",
			m_strCurrentProcessName,
			RUL_INDEX_VERSON, 
			nReason, 
			nRandNumber,
			lpszUrl);
	}

	_DoRecordURL(nReason, nRandNumber);
#endif

    CString strInfo;
    if (lpszUrl)
    {
        strInfo.Format(BLOCKINTERNETOPENURL, m_strCurrentProcessName,  lpszUrl);
        m_userLog.Log(L"%s", strInfo.GetBuffer());
        strInfo.ReleaseBuffer();
    }
    else
    {
        strInfo = "";
    }

    DoNotify(strInfo, NULL, lpszUrl, nReason, RECORD_BUSY);
}

void __stdcall CKSWebShiledCallback::NotifyBlockHttpOpenRequest(
	LPCWSTR lpszObjectName,
	int nReason
	)
{
	if (!m_bInitFlag)
		return;
#if 0
	SetWindowRedStyle(nReason);

	CString strInfo;
	if (lpszObjectName)
	{
		strInfo.Format(BLOCKHTTPOPENREQUEST, m_strCurrentProcessName, lpszObjectName);
		//if (m_pTrayPopService)
		{
			KwsShowPopo( 2, strInfo, NULL );
		}
	}
	m_userLog.Log(L"%s", strInfo.GetBuffer());
	strInfo.ReleaseBuffer();

	_DoRecordURL(nReason, 0);
#endif

    CString strInfo;
    if (lpszObjectName)
    {
        strInfo.Format(BLOCKHTTPOPENREQUEST, m_strCurrentProcessName, lpszObjectName);       
        m_userLog.Log(L"%s", strInfo.GetBuffer());
        strInfo.ReleaseBuffer();
    }
    else
    {
        strInfo = "";
    }

    DoNotify(strInfo, NULL, NULL, nReason, RECORD_BUSY);
}

void __stdcall CKSWebShiledCallback::NotifyBlockInternetConnect(
	IN LPCWSTR lpszServerName,
	IN WORD nServerPort,
	int nReason
	)
{
	if (!m_bInitFlag)
		return;
#if 0
	SetWindowRedStyle(nReason);

	CString strInfo;
	if (lpszServerName)
	{
		strInfo.Format(
			BLOCKINTERNETCONNECT, 
			m_strCurrentProcessName, 
			lpszServerName, 
			nServerPort);

		//if (m_pTrayPopService)
		{
			KwsShowPopo( 2, strInfo, NULL );
		}
	}
	m_userLog.Log(L"%s", strInfo.GetBuffer());
	strInfo.ReleaseBuffer();

	_DoRecordURL(nReason, 0);
#endif

    CString strInfo;
    if (lpszServerName)
    {
        strInfo.Format(
            BLOCKINTERNETCONNECT, 
            m_strCurrentProcessName, 
            lpszServerName, 
            nServerPort);

        m_userLog.Log(L"%s", strInfo.GetBuffer());
        strInfo.ReleaseBuffer();
    }
    else
    {
        strInfo = "";
    }

    DoNotify(strInfo, NULL, NULL, nReason, RECORD_BUSY);
}

void __stdcall CKSWebShiledCallback::NotifyBlockExCopyFile(
	LPCWSTR lpszSrcFile,
	LPCWSTR lpszDstFile,
	int nReason
	)
{
	//SetWindowRedStyle(nReason);
	if (!m_bInitFlag)
		return;

#if 0
	CString strInfo;
	if (lpszSrcFile && lpszDstFile)
	{
		strInfo.Format(
			BLOCKCOPYFILE, 
			m_strCurrentProcessName, 
			lpszSrcFile, 
			lpszDstFile
			);

		//if (m_pTrayPopService)
		{
			KwsShowPopo( 2, strInfo, NULL );
		}
		m_userLog.Log(L"%s", strInfo.GetBuffer());
		strInfo.ReleaseBuffer();
	}
	_DoRecordURL(nReason, 0);
#endif

    CString strInfo;
    if (lpszSrcFile && lpszDstFile)
    {
        strInfo.Format(
            BLOCKCOPYFILE, 
            m_strCurrentProcessName, 
            lpszSrcFile, 
            lpszDstFile
            );

        m_userLog.Log(L"%s", strInfo.GetBuffer());
        strInfo.ReleaseBuffer();
    }
    else
    {
        strInfo = "";
    }

    DoNotify(strInfo, NULL, NULL, nReason, RECORD_BUSY);
}

void __stdcall CKSWebShiledCallback::NotiryBlockLoadLibrary(
	LPCWSTR lpFileName,
	int nReason
	)
{
	if (!m_bInitFlag)
		return;
#if 0
	SetWindowRedStyle(nReason);

	CString strInfo;
	if (lpFileName)
	{
		strInfo.Format(BLOCKLOADLIBRARY, m_strCurrentProcessName, lpFileName);
//		if (m_pTrayPopService)
		{
			KwsShowPopo( 2, strInfo, NULL );
		}

		m_userLog.Log(strInfo.GetBuffer());
		strInfo.ReleaseBuffer();
	}

	_DoRecordURL(nReason, 0);
#endif

    CString strInfo;
    if (lpFileName)
    {
        strInfo.Format(BLOCKLOADLIBRARY, m_strCurrentProcessName, lpFileName);
        m_userLog.Log(strInfo.GetBuffer());
        strInfo.ReleaseBuffer();
    }
    else
    {
        strInfo = "";
    }

    DoNotify(strInfo, NULL, NULL, nReason, RECORD_BUSY);
}

void __stdcall CKSWebShiledCallback::NotifyBlockReadFile(
	BSTR FileName,
	int nReason
	)
{
	if (!m_bInitFlag)
		return;
#if 0
	SetWindowRedStyle(nReason);

	CString strInfo;
	if (FileName)
	{
		strInfo.Format(BLOCKREADFILE, m_strCurrentProcessName, FileName);
//		if (m_pTrayPopService)
		{
			KwsShowPopo( 2, strInfo, NULL );
		}
		m_userLog.Log(strInfo.GetBuffer());
		strInfo.ReleaseBuffer();
	}

	_DoRecordURL(nReason, 0);
#endif

    CString strInfo;
    if (FileName)
    {
        strInfo.Format(BLOCKREADFILE, m_strCurrentProcessName, FileName);
        
        m_userLog.Log(strInfo.GetBuffer());
        strInfo.ReleaseBuffer();
    }
    else
    {
        strInfo = "";
    }

    DoNotify(strInfo, NULL, NULL, nReason, RECORD_BUSY);
}

void __stdcall CKSWebShiledCallback::NotifyBlockSaveFile(
	BSTR FileName,
	int nReason
	)
{
	if (!m_bInitFlag)
		return;
#if 0
	SetWindowRedStyle(nReason);

	CString strInfo;
	if (FileName)
	{
		strInfo.Format(BLOCKSAVEFILE, m_strCurrentProcessName, FileName);
//		if (m_pTrayPopService)
		{
			KwsShowPopo( 2, strInfo, NULL );
		}
		m_userLog.Log(strInfo.GetBuffer());
		strInfo.ReleaseBuffer();
	}

	_DoRecordURL(nReason, 0);
#endif

    CString strInfo;
    if (FileName)
    {
        strInfo.Format(BLOCKSAVEFILE, m_strCurrentProcessName, FileName);
        
        m_userLog.Log(strInfo.GetBuffer());
        strInfo.ReleaseBuffer();
    }
    else
    {
        strInfo = "";
    }

    DoNotify(strInfo, NULL, NULL, nReason, RECORD_BUSY);
}

void __stdcall CKSWebShiledCallback::NotifyBlockRegCreateKey(
	HKEY hKey,
	LPCWSTR lpSubKey,
	int nReason
	)
{
	if (!m_bInitFlag)
		return;
#if 0
	SetWindowRedStyle(nReason);

	CString strInfo;
	CString strtemp;

	if (lpSubKey)
	{
		if (_DoRegToString(hKey, strtemp))
		{
			strtemp += lpSubKey;
		}

		strInfo.Format(
			BLOCKREGCREATEKEY,
			m_strCurrentProcessName, 
			strtemp
			);

//		if (m_pTrayPopService)
		{
			KwsShowPopo( 2, strInfo, NULL );
		}
		m_userLog.Log(strInfo.GetBuffer());
		strInfo.ReleaseBuffer();
	}

	_DoRecordURL(nReason, 0);
#endif

    CString strInfo;
    CString strtemp;

    if (lpSubKey)
    {
        if (_DoRegToString(hKey, strtemp))
        {
            strtemp += lpSubKey;
        }

        strInfo.Format(
            BLOCKREGCREATEKEY,
            m_strCurrentProcessName, 
            strtemp
            );

        m_userLog.Log(strInfo.GetBuffer());
        strInfo.ReleaseBuffer();
    }
    else
    {
        strInfo = "";
    }

    DoNotify(strInfo, NULL, NULL, nReason, RECORD_BUSY);
}


void __stdcall CKSWebShiledCallback::NotifyBlockRegOpenKey(
	HKEY hKey,
	LPCWSTR lpSubKey,
	int nReason
	)
{
	if (!m_bInitFlag)
		return;
#if 0
	SetWindowRedStyle(nReason);

	CString strInfo;
	CString strtemp;

	if (lpSubKey)
	{
		if (_DoRegToString(hKey, strtemp))
		{
			strtemp += lpSubKey;
		}
		strInfo.Format(BLOCKREGOPENKEY, m_strCurrentProcessName, strtemp);
//		if (m_pTrayPopService)
		{
			KwsShowPopo( 2, strInfo, NULL );
		}
		m_userLog.Log(strInfo.GetBuffer());
		strInfo.ReleaseBuffer();
	}
	_DoRecordURL(nReason, 0);
#endif

    CString strInfo;
    CString strtemp;

    if (lpSubKey)
    {
        if (_DoRegToString(hKey, strtemp))
        {
            strtemp += lpSubKey;
        }
        strInfo.Format(BLOCKREGOPENKEY, m_strCurrentProcessName, strtemp);
        
        m_userLog.Log(strInfo.GetBuffer());
        strInfo.ReleaseBuffer();
    }
    else
    {
        strInfo = "";
    }

    DoNotify(strInfo, NULL, NULL, nReason, RECORD_BUSY);
}

void __stdcall CKSWebShiledCallback::NotifyBlockJS(
    LPCWSTR szTopUrl,
    LPCWSTR szLocalUrl,
    LPCWSTR szMessage,
    int nReason
    )
{
	unsigned int nRandNumber = 0;
    if (!m_bInitFlag)
        return;
#if 0
	srand( (unsigned)time( NULL ) );
	nRandNumber = rand();
	if (nRandNumber == 0)
	{
		nRandNumber++;
	}
    if ((!szTopUrl) || 0 == wcslen(szTopUrl))
    {
        _DoRecordURL(BLOCK_JS_TOP, nRandNumber);
    }
    else
    {
        m_urlLog.Log(L"[%s][%s][%d][%d]%s",
            m_strCurrentProcessName,
            RUL_INDEX_VERSON, 
            BLOCK_JS_TOP, 
			nRandNumber,
            szTopUrl);
    }

    if (szLocalUrl)
    {
        m_urlLog.Log(L"[%s][%s][%d][%d]%s",
            m_strCurrentProcessName,
            RUL_INDEX_VERSON, 
            nReason, 
			nRandNumber,
            szLocalUrl);
    }
#endif

    DoNotify(NULL, szTopUrl, szLocalUrl, nReason, RECORD_BUSY);
}

void __stdcall CKSWebShiledCallback::NotifyBlockJS_Pop(
	LPCWSTR szTopUrl,
	LPCWSTR szLocalUrl,
	LPCWSTR szMessage,
	int nReason
	)
{
	CString strInfo;
	unsigned int nRandNumber = 0;
#if 0
	if (!m_bInitFlag)
		return;

	SetWindowRedStyle(nReason);

	srand( (unsigned)time( NULL ) );
	nRandNumber = rand();
	if (nRandNumber == 0)
	{
		nRandNumber++;
	}

	if ((!szTopUrl) || 0 == wcslen(szTopUrl))
	{
		_DoRecordURL(BLOCK_JS_TOP, nRandNumber);
	}
	else
	{
		m_urlLog.Log(L"[%s][%s][%d][%d]%s",
			m_strCurrentProcessName,
			RUL_INDEX_VERSON, 
			BLOCK_JS_TOP, 
			nRandNumber,
			szTopUrl);
	}

	if (szLocalUrl)
	{
		m_urlLog.Log(L"[%s][%s][%d][%d]%s",
			m_strCurrentProcessName,
			RUL_INDEX_VERSON, 
			nReason, 
			nRandNumber,
			szLocalUrl);
	}

	if (/*m_pTrayPopService &&*/ szMessage && szLocalUrl)
	{
		strInfo.Format(BLOCKJSCRIPT, m_strCurrentProcessName, szLocalUrl, szMessage);
		KwsShowPopo( 2, strInfo, NULL );
	}

	m_userLog.Log(strInfo.GetBuffer());
	strInfo.ReleaseBuffer();
#endif

    if (szMessage && szLocalUrl)
    {
        strInfo.Format(BLOCKJSCRIPT, m_strCurrentProcessName, szLocalUrl, szMessage);
    }
    else
    {
        strInfo = "";
    }

    DoNotify(strInfo, szTopUrl, szLocalUrl, nReason, RECORD_BUSY);

    m_userLog.Log(strInfo.GetBuffer());
    strInfo.ReleaseBuffer();
}


void __stdcall CKSWebShiledCallback::NotifyBlockSWF(
	LPCWSTR szMessage,
	LPCWSTR lpszCaption,
	int nReason
	)
{
	if (!m_bInitFlag)
		return;
#if 0
	SetWindowRedStyle(nReason);

	_DoRecordURLSWF(nReason, 0);
#endif

    DoNotify(NULL, NULL, NULL, nReason, RECORD_ALL);
}


void __stdcall CKSWebShiledCallback::NotifyMessage(
	unsigned int uPriority,
	LPCWSTR szMessage,
	LPCWSTR lpszCommandBtn
	)
{
//	if (m_pTrayPopService)
	{
	//	m_pTrayPopService->PopoMessage(uPriority, szMessage, POPMSG_CAPTION, lpszCommandBtn, NULL);
		KwsShowPopo( uPriority, szMessage, L"Reserved", POP_REASON_FIRSTSHINE );
	}
}

void __stdcall CKSWebShiledCallback::LogURLWithCriticalDotCount(
    LPWSTR wszURL,
    INT nReason
    )
{
    UINT nRandNumber;

    if ( NULL ==nReason )
    {
        return;
    }

    srand( (unsigned)time( NULL ) );
    nRandNumber = rand();
    if (nRandNumber == 0)
    {
        nRandNumber++;
    }

    m_urlLog.Log(L"[%s][%s][%d][%d]%s",
        m_strCurrentProcessName,
        RUL_INDEX_VERSON, 
        nReason, 
        nRandNumber,
        wszURL
        );
}

BOOL __stdcall CKSWebShiledCallback::RegisterBlockCallback(
    IKwsBlockCallback *pCallback
    )
{
    if ( NULL == pCallback )
    {
        return FALSE;
    }

    m_pBlockCallback = pCallback;

    return TRUE;
}

// -------------------------------------------------------------------------

DWORD CKSWebShiledCallback::StartThread()
{

	DWORD dwRet = OPERATE_SUCCESSFUL;
	m_hThread = ::CreateThread(NULL, 0, CKSWebShiledCallback::ThreadLogFunc, (LPVOID)this, CREATE_SUSPENDED, 
		&m_dwThreadId);	

	if (m_hThread == NULL)
	{
		dwRet = CREATE_THREAD_ERROR;
	}
	else
	{
		m_bStopThread	= false;
		::ResumeThread(m_hThread);
	}
	return dwRet;
}

DWORD WINAPI CKSWebShiledCallback::ThreadLogFunc(LPVOID pParam)
{

#ifdef _DEBUG
	char szbuffer[1024] = {0};
#endif
	CKSWebShiledCallback* pTo = (CKSWebShiledCallback *)pParam;
	

	//pTo->m_urlLog.Log(L"ThreadLogFunc");

#ifdef _DEBUG
	_snprintf(szbuffer, 1024, "ThreadLogFunc Called %d", GetTickCount());
	OutputDebugStringA(szbuffer);
#endif

	vector<wstring> vecwstrUrl;
	WCHAR url[1024] = {0};


	GetExploitURLs(vecwstrUrl);

#ifdef _DEBUG
	_snprintf(szbuffer, 1024, "ThreadLogFunc leaved %d", GetTickCount());
	OutputDebugStringA(szbuffer);
#endif

	return 0;
}



// -------------------------------------------------------------------------

BOOL CKSWebShiledCallback::_DoRegToString(HKEY hKey, CString& str)
{
	switch((LONG)hKey)
	{
	case HKEY_CLASSES_ROOT:
		str = "HKEY_CLASSES_ROOT\\";
		break;
	case HKEY_CURRENT_CONFIG:
		str = "HKEY_CURRENT_CONFIG\\";
		break;
	case HKEY_CURRENT_USER:
		str = "HKEY_CURRENT_USER\\";
		break;
	case HKEY_LOCAL_MACHINE:
		str = "HKEY_LOCAL_MACHINE\\";
		break;
	case HKEY_USERS:
		str = "HKEY_USERS\\";
		break;
	default:
		return FALSE;
	}

	return TRUE;
}

BOOL CKSWebShiledCallback::_DoRecordURL(int nReason, unsigned int nRandNumber)
{
	vector<wstring> vecwstrUrl;
	//wstring veUrl;

	if (nRandNumber == 0)
	{
		srand( (unsigned)time( NULL ) );
		nRandNumber = rand();
	}

	GetExploitURLs(vecwstrUrl);

	int nsize = vecwstrUrl.size();

	for (int i = 0; i < nsize; i++ )
	{
        //veUrl = UrlDecode(vecwstrUrl[i]);
        m_urlLog.Log(L"[%s][%s][%08x][%d]%s",
            m_strCurrentProcessName,
            RUL_INDEX_VERSON, 
            nReason, 
			nRandNumber,
            /*veUrl.c_str()*/vecwstrUrl[i]);
	}

	return TRUE;
}

BOOL CKSWebShiledCallback::_DoRecordURLSWF(int nReason, unsigned int nRandNumber)
{
	vector<wstring> vecwstrUrl;
	//wstring veUrl;
	if (nRandNumber == 0)
	{
		srand( (unsigned)time( NULL ) );
		nRandNumber = rand();
	}

	GetExploitURLsSWF(vecwstrUrl);

	int nsize = vecwstrUrl.size();

	for (int i = 0; i < nsize; i++ )
	{
		//veUrl = UrlDecode(vecwstrUrl[i]);
		m_urlLog.Log(L"[%s][%s][%08x][%d]%s",
			m_strCurrentProcessName,
			RUL_INDEX_VERSON, 
			nReason, 
			nRandNumber,
			/*veUrl.c_str()*/vecwstrUrl[i]);
	}

	return TRUE;
}

VOID CKSWebShiledCallback::DoNotify( LPCWSTR lpNotifyMessage,
                                     LPCWSTR lpHostUrl,
                                     LPCWSTR lpLocalUrl,
                                     INT nBlockReason,
                                     INT nRecordMethod
                                     )
{
    UINT nLen = 0;
    UINT nRandNumber = 0;

    //
    // 先调用回调函数
    //

    if (m_pBlockCallback)
    {
        m_pBlockCallback->BlockNotifyRoutine(lpLocalUrl, nBlockReason);
    }

    //
    // 闪烁浏览器边框
    //

    SetWindowRedStyle(nBlockReason);

    //DebugBreak();

    //
    // 如果提示消息不为NULL并且消息长度不为0，则弹出泡泡
    //

    nLen = wcslen(lpNotifyMessage);

    if ( (NULL != lpNotifyMessage) && nLen )
    {
        //
        // 将lpNotifyMessage里所有的'<'和'>'换成'['和']'
        //

        for (UINT i = 0; i < nLen; ++i)
        {
            if ('<' == lpNotifyMessage[i])
            {
                ((PWCHAR)lpNotifyMessage)[i] = '[';
            }
            else if ('>' == lpNotifyMessage[i])
            {
                ((PWCHAR)lpNotifyMessage)[i] = ']';
            }
        }

        if (nBlockReason == NET_PHISHING)
        {
            //m_pTrayPopService->PopoMessage(1, lpNotifyMessage, POPMSG_CAPTION, NULL, NULL);
            KwsShowPopo(1, lpNotifyMessage, NULL, nBlockReason);
        }
        else
        {
            //m_pTrayPopService->PopoMessage(2, lpNotifyMessage, POPMSG_CAPTION, NULL, NULL);
            KwsShowPopo(2, lpNotifyMessage, NULL, nBlockReason);
        }
    }

    // Debug

    //__asm int 3;

    srand( GetTickCount()/*(unsigned)time( NULL )*/ );
    nRandNumber = rand();

    if (0 == nRandNumber)
        ++nRandNumber;

    //
    // 如果lpHostUrl不为空并且长度不为0，则将其记录在日志中并忽略nRecordMethod参数
    //

    if ( (NULL != lpHostUrl) && wcslen(lpHostUrl) )
    {
#if 0
        if (BLOCK_JS_LOCAL == nBlockReason || BLOCK_JS_TOP == nBlockReason)
        {
            //
            // 如果恶意链接是被脚本检查拦截的就得区分是TOP还是是LOCAL。
            // N.B.这里修改了nBlockReason的值不会对下面造成影响。因为BLOCK_JS_TOP
            // 和BLOCK_JS_LOCAL都说明是被脚本检查拦截的，所以nBlockReason等于任意
            // 一个值都没影响。
            //

            nBlockReason = BLOCK_JS_TOP;
        }
#endif

        //wstring wstrURL = UrlDecode(lpHostUrl);

        m_urlLog.Log( L"[%s][%s][%08x][%d]%s",
                      m_strCurrentProcessName,
                      RUL_INDEX_VERSON, 
                      nBlockReason | KWS_TOP_URL_FLAG,
                      nRandNumber,
                      /*wstrURL.c_str()*/lpHostUrl
                      );
    }
    else 
    {
        //
        // 如果lpHostUrl为空或者其长度为0，则根据nRecordMethod指定的方法收集URL
        //
#if 0
        if (BLOCK_JS_LOCAL == nBlockReason || BLOCK_JS_TOP == nBlockReason)
        {
            nBlockReason = BLOCK_JS_TOP;
        }
#endif
        if (RECORD_ALL == nRecordMethod)
        {
            _DoRecordURLSWF(nBlockReason | KWS_TOP_URL_FLAG, nRandNumber);
        }
        else if (RECORD_BUSY == nRecordMethod)
        {
            _DoRecordURL(nBlockReason | KWS_TOP_URL_FLAG, nRandNumber);
        }
    }

    //
    // 如果lpLocalUrl不为空并且长度不为0，则将其记录到日志中
    //

    if ( (NULL != lpLocalUrl) && wcslen(lpLocalUrl) )
    {
#if 0
        if (BLOCK_JS_LOCAL == nBlockReason || BLOCK_JS_TOP == nBlockReason)
        {
            nBlockReason = BLOCK_JS_LOCAL;
        }
        else
#endif
        if (BLOCK_NET_ADRESS_SPITE == nBlockReason)
        {
            nBlockReason = BLOCK_NET_ADRESS_SPITE_MATCH_SEND;
        }
        else if (BLOCK_NET_ADRESS_SPITE_REGEX_MATCH == nBlockReason)
        {
            nBlockReason = BLOCK_NET_ADRESS_SPITE_REGEX_MATCH_SEND;
        }

        //wstring wstrURL = UrlDecode(lpLocalUrl);

        m_urlLog.Log( L"[%s][%s][%08x][%d]%s",
                      m_strCurrentProcessName,
                      RUL_INDEX_VERSON, 
                      nBlockReason & KWS_LOCAL_URL_MASK, 
                      nRandNumber,
                      /*wstrURL.c_str()*/lpLocalUrl
                      );
    }
}

WCHAR CKSWebShiledCallback::HexToWChar(
					   WCHAR first, 
					   WCHAR second)
{
	int digit;

	digit = (first >= 'A' ? ((first & 0xDF) - 'A') + 10 : (first - '0'));
	digit *= 16;
	digit += (second >= 'A' ? ((second & 0xDF) - 'A') + 10 : (second - '0'));
	return static_cast<WCHAR>(digit);
}

//std::wstring CKSWebShiledCallback::UrlDecode(const std::wstring& src)
//{
//	WCHAR szTmp[128] = {0};
//	DWORD dwReLen = 0;
//	std::wstring strResult = src;
//
//	if(!AtlUnescapeUrl(src.c_str(), szTmp, &dwReLen, 127))
//	{
//		WCHAR *pszTmp = new WCHAR [dwReLen + 1];
//		if (pszTmp != NULL)
//		{
//			if(AtlUnescapeUrl(src.c_str(), pszTmp, &dwReLen, dwReLen))
//			{
//				strResult = pszTmp;
//			}
//			delete [] pszTmp;
//		}		
//	}
//	else
//	{
//		strResult = szTmp;
//	}
//	
//	return strResult;
//}

// -------------------------------------------------------------------------
// $Log: $
