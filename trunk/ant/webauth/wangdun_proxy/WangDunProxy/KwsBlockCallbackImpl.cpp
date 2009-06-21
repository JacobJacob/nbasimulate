#include "stdafx.h"
#include "KwsBlockCallbackImpl.h"
#include "Reason.h"

VOID
TRACE_PRINT(
    PCHAR szFormat,
    ...
    );

IKwsBlockCallback *CKwsBlockCallbackImpl::GetInstance()
{
    static CKwsBlockCallbackImpl blockCallback;
    return static_cast<IKwsBlockCallback *>(&blockCallback);
}

CKwsBlockCallbackImpl::CKwsBlockCallbackImpl()
{
	srand( (unsigned)time( NULL ) );
	m_iRandom = rand() % 10000+1; 
	m_bFirst = false;

}
void CKwsBlockCallbackImpl::setCurUrl(string strUrl)
{
	m_strCurUrl = strUrl;
	m_bFirst = true;
	srand( (unsigned)time( NULL ) );
	m_iRandom = rand() % 10000+1; 

}

void  CKwsBlockCallbackImpl::writeLog(string strMsg, string strUrl,int uReason)
{
	uReason &= KWS_BLOCK_REASON_MASK;
	if (uReason== BLOCK_JS_TEST) return; //如果回调函数中收到的nBlockReason是BLOCK_JS_TEST（这个宏的值是        0x15），
	SYSTEMTIME	systm; 
	GetLocalTime(&systm);

	//组装日志信息
	char strtm[32];
	sprintf_s(strtm,32,"%4d-%02d-%02d %02d:%02d:%02d",systm.wYear,systm.wMonth,systm.wDay,systm.wHour,systm.wMinute,systm.wSecond);

	char strLog[4096];
	

	if (m_bFirst) // 这里的流程完全和 网盾 源代码保持一致 http://svn.rdev.kingsoft.net/KXEngine/WebShield/trunk/Src/kwsui/ckswebshiledcallback.cpp
	{
		char strLogFirst[2048];
		sprintf_s(strLogFirst,2048,"[%s] [iexplore.exe][%s][%d][%s][%d]%s\r\n",strtm,RUL_INDEX_VERSON_A,0,"top_url",m_iRandom,m_strCurUrl.c_str());

		if (BLOCK_NET_ADRESS_SPITE == uReason)
        {
            uReason = BLOCK_NET_ADRESS_SPITE_MATCH_SEND;
        }
        else if (BLOCK_NET_ADRESS_SPITE_REGEX_MATCH == uReason)
        {
            uReason = BLOCK_NET_ADRESS_SPITE_REGEX_MATCH_SEND;
        }

		char strLogShow[2048];
		sprintf_s(strLogShow,2048,"[%s] [iexplore.exe][%s][%d][%s][%d]%s\r\n",strtm,RUL_INDEX_VERSON_A,uReason & KWS_LOCAL_URL_MASK,strMsg.c_str(),m_iRandom,strUrl.c_str());
		sprintf_s(strLog,4096,"%s%s",strLogFirst,strLogShow);	
		m_bFirst = false;
	}
	else
	{
		if (BLOCK_NET_ADRESS_SPITE == uReason)
        {
            uReason = BLOCK_NET_ADRESS_SPITE_MATCH_SEND;
        }
        else if (BLOCK_NET_ADRESS_SPITE_REGEX_MATCH == uReason)
        {
            uReason = BLOCK_NET_ADRESS_SPITE_REGEX_MATCH_SEND;
        }
		sprintf_s(strLog,2048,"[%s] [iexplore.exe][%s][%d][%s][%d]%s\r\n",strtm,RUL_INDEX_VERSON_A,uReason & KWS_LOCAL_URL_MASK,strMsg.c_str(),m_iRandom,strUrl.c_str());
	}

	//判断打开日志文件是否成功.
	HANDLE hFile = NULL;
	while (hFile == NULL)
	{
		hFile = CreateFileA(m_strLogFile.c_str(),GENERIC_WRITE,FILE_SHARE_READ,NULL,OPEN_ALWAYS,FILE_ATTRIBUTE_NORMAL,NULL);

		if (hFile == INVALID_HANDLE_VALUE)
		{
			WaitForSingleObject(hFile,1000);
			hFile = NULL;
		}
	}


	if (hFile != INVALID_HANDLE_VALUE)
	{
		DWORD   dwHigh;  
		DWORD   dwPos   =   GetFileSize(hFile,&dwHigh);  
		SetFilePointer(hFile,dwPos,0,FILE_BEGIN);   

		DWORD wrNum = 0;
		BOOL wfRes = WriteFile(hFile,strLog,(DWORD)strlen(strLog),&wrNum,NULL);

		CloseHandle(hFile);	
	}
}


VOID __stdcall CKwsBlockCallbackImpl::BlockNotifyRoutine(LPCWSTR lpNotifyMessage, LPCWSTR lpLocalUrl, UINT nReason)
{
    TRACE_PRINT("%s is blocked, reason: %d.\n", lpLocalUrl, nReason);
	USES_CONVERSION;
	if (lpLocalUrl != NULL)
	{
		if (lpNotifyMessage!=NULL)
			writeLog(W2A(lpNotifyMessage),W2A(lpLocalUrl),nReason);
		else
			writeLog("no_message",W2A(lpLocalUrl),nReason);
	}
	else{
		if (lpNotifyMessage!=NULL)
			writeLog(W2A(lpNotifyMessage),"no_url_error",nReason);
		else
			writeLog("no_message","no_url_error",nReason);
	}

	

/*	USES_CONVERSION;
	SYSTEMTIME	systm; 
	GetLocalTime(&systm);

	//组装日志信息
	char strtm[32];
	sprintf_s(strtm,32,"%4d-%02d-%02d %02d:%02d:%02d",systm.wYear,systm.wMonth,systm.wDay,systm.wHour,systm.wMinute,systm.wSecond);

	char strLog[2048];
	sprintf_s(strLog,2048,"[%s] [iexplore.exe][20090114][%d][%d]%s\r\n",strtm,nReason,m_iRandom,W2A(lpLocalUrl));

	//判断打开日志文件是否成功.
	HANDLE hFile = NULL;
	while (hFile == NULL)
	{
		hFile = CreateFileA(m_strLogFile.c_str(),GENERIC_WRITE,FILE_SHARE_READ,NULL,OPEN_ALWAYS,FILE_ATTRIBUTE_NORMAL,NULL);
		
		if (hFile == INVALID_HANDLE_VALUE)
		{
			WaitForSingleObject(hFile,1000);
			hFile = NULL;
		}
	}
		

	if (hFile != INVALID_HANDLE_VALUE)
	{
		DWORD   dwHigh;  
		DWORD   dwPos   =   GetFileSize(hFile,&dwHigh);  
		SetFilePointer(hFile,dwPos,0,FILE_BEGIN);   

		DWORD wrNum = 0;
		BOOL wfRes = WriteFile(hFile,strLog,(DWORD)strlen(strLog),&wrNum,NULL);

		CloseHandle(hFile);	
	}
	*/
}

void CKwsBlockCallbackImpl::setLogFile(string strFile)
{
	m_strLogFile = strFile;
}


//=============================================================
IKwsBlockCallback *CKwsBlockCallbackImplW::GetInstance()
{
	static CKwsBlockCallbackImplW blockCallback;
	return static_cast<IKwsBlockCallback *>(&blockCallback);
}

CKwsBlockCallbackImplW::CKwsBlockCallbackImplW()
{
	srand( (unsigned)time( NULL ) );
	m_iRandom = rand() % 10000+1; 
	m_bFirst = false;

}
void CKwsBlockCallbackImplW::setCurUrl(CAtlString strUrl)
{
	m_strCurUrl = strUrl;
	m_bFirst = true;
	srand( (unsigned)time( NULL ) );
	m_iRandom = rand() % 10000+1; 

}

void  CKwsBlockCallbackImplW::writeLog(CAtlString strMsg,CAtlString strUrl,int uReason)
{
	uReason &= KWS_BLOCK_REASON_MASK;
	if (uReason== BLOCK_JS_TEST) return; //如果回调函数中收到的nBlockReason是BLOCK_JS_TEST（这个宏的值是        0x15），
	SYSTEMTIME	systm; 
	GetLocalTime(&systm);

	//组装日志信息

	CAtlString strtm;
	strtm.Format(L"%4d-%02d-%02d %02d:%02d:%02d",systm.wYear,systm.wMonth,systm.wDay,systm.wHour,systm.wMinute,systm.wSecond);
	//char strtm[32];
	//sprintf_s(strtm,32,"%4d-%02d-%02d %02d:%02d:%02d",systm.wYear,systm.wMonth,systm.wDay,systm.wHour,systm.wMinute,systm.wSecond);

	//char strLog[4096];
	CAtlString strLog;

	if (m_bFirst)
	{
		CAtlString strLogFirst;
		strLogFirst.Format(L"[%s] [iexplore.exe][%s][%d][%s][%d]%s\r\n",strtm,RUL_INDEX_VERSON,0,L"top_url",m_iRandom,m_strCurUrl);

		if (BLOCK_NET_ADRESS_SPITE == uReason)
        {
            uReason = BLOCK_NET_ADRESS_SPITE_MATCH_SEND;
        }
        else if (BLOCK_NET_ADRESS_SPITE_REGEX_MATCH == uReason)
        {
            uReason = BLOCK_NET_ADRESS_SPITE_REGEX_MATCH_SEND;
        }

		strLog.Format(L"[%s] [iexplore.exe][%s][%d][%s][%d]%s\r\n",strtm,RUL_INDEX_VERSON,uReason & KWS_LOCAL_URL_MASK,strMsg,m_iRandom,strUrl);
		strLog = strLogFirst + strLog; 
	
		m_bFirst = false;
	} else {
		if (BLOCK_NET_ADRESS_SPITE == uReason)
        {
            uReason = BLOCK_NET_ADRESS_SPITE_MATCH_SEND;
        }
        else if (BLOCK_NET_ADRESS_SPITE_REGEX_MATCH == uReason)
        {
            uReason = BLOCK_NET_ADRESS_SPITE_REGEX_MATCH_SEND;
        }
		strLog.Format(L"[%s] [iexplore.exe][%s][%d][%s][%d]%s\r\n",strtm,RUL_INDEX_VERSON,uReason & KWS_LOCAL_URL_MASK,strMsg,m_iRandom,strUrl);
	}


	//判断打开日志文件是否成功.
	HANDLE hFile = NULL;
	while (hFile == NULL)
	{
		hFile = CreateFile(m_strLogFile,GENERIC_WRITE,FILE_SHARE_READ,NULL,OPEN_ALWAYS,FILE_ATTRIBUTE_NORMAL,NULL);

		if (hFile == INVALID_HANDLE_VALUE)
		{
			WaitForSingleObject(hFile,1000);
			hFile = NULL;
		}
	}


	if (hFile != INVALID_HANDLE_VALUE)
	{
		DWORD   dwHigh;  
		DWORD   dwPos   =   GetFileSize(hFile,&dwHigh);  
		SetFilePointer(hFile,dwPos,0,FILE_BEGIN);   

		DWORD wrNum = 0;
		BOOL wfRes = WriteFile(hFile,strLog.GetBuffer(strLog.GetLength()),(DWORD)strLog.GetLength(),&wrNum,NULL);
		strLog.ReleaseBuffer();

		CloseHandle(hFile);	
	}
}


VOID __stdcall CKwsBlockCallbackImplW::BlockNotifyRoutine(LPCWSTR lpNotifyMessage, LPCWSTR lpLocalUrl, UINT nReason)
{
	TRACE_PRINT("%s is blocked, reason: %d.\n", lpLocalUrl, nReason);
	USES_CONVERSION;
	if (lpLocalUrl != NULL)
	{
		if (lpNotifyMessage!=NULL)
			writeLog(lpNotifyMessage, lpLocalUrl,nReason);
		else
			writeLog(L"no_message", lpLocalUrl,nReason);
	}
	else {
		if (lpNotifyMessage!=NULL)
			writeLog(lpNotifyMessage, L"no_url_error",nReason);
		else
			writeLog(L"no_message", L"no_url_error",nReason);
	}



	/*	USES_CONVERSION;
	SYSTEMTIME	systm; 
	GetLocalTime(&systm);

	//组装日志信息
	char strtm[32];
	sprintf_s(strtm,32,"%4d-%02d-%02d %02d:%02d:%02d",systm.wYear,systm.wMonth,systm.wDay,systm.wHour,systm.wMinute,systm.wSecond);

	char strLog[2048];
	sprintf_s(strLog,2048,"[%s] [iexplore.exe][20090114][%d][%d]%s\r\n",strtm,nReason,m_iRandom,W2A(lpLocalUrl));

	//判断打开日志文件是否成功.
	HANDLE hFile = NULL;
	while (hFile == NULL)
	{
	hFile = CreateFileA(m_strLogFile.c_str(),GENERIC_WRITE,FILE_SHARE_READ,NULL,OPEN_ALWAYS,FILE_ATTRIBUTE_NORMAL,NULL);

	if (hFile == INVALID_HANDLE_VALUE)
	{
	WaitForSingleObject(hFile,1000);
	hFile = NULL;
	}
	}


	if (hFile != INVALID_HANDLE_VALUE)
	{
	DWORD   dwHigh;  
	DWORD   dwPos   =   GetFileSize(hFile,&dwHigh);  
	SetFilePointer(hFile,dwPos,0,FILE_BEGIN);   

	DWORD wrNum = 0;
	BOOL wfRes = WriteFile(hFile,strLog,(DWORD)strlen(strLog),&wrNum,NULL);

	CloseHandle(hFile);	
	}
	*/
}

void CKwsBlockCallbackImplW::setLogFile(CAtlString strFile)
{
	m_strLogFile = strFile;
}


