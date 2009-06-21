#ifndef _KWSBLOCKCALLBACKIMPL_H
#define _KWSBLOCKCALLBACKIMPL_H

#include "KwsBlockCallback.h"

class CKwsBlockCallbackImpl : public IKwsBlockCallback
{
public:
	CKwsBlockCallbackImpl();
    static IKwsBlockCallback *GetInstance();
    virtual VOID __stdcall BlockNotifyRoutine(LPCWSTR lpNotifyMessage, LPCWSTR lpLocalUrl, UINT nReason);


	void setLogFile(string strFile);
	void writeLog(string strMsg, string strUrl,int uReason);
	void setCurUrl(string strUrl);

private:
	string m_strLogFile;
	int		m_iRandom;
	string m_strCurUrl;	//当前浏览的URL
	bool	m_bFirst;	//对于m_strCurUrl是否第一次发现错误.
};

class CKwsBlockCallbackImplW : public IKwsBlockCallback
{
public:
	CKwsBlockCallbackImplW();
	static IKwsBlockCallback *GetInstance();
	virtual VOID __stdcall BlockNotifyRoutine(LPCWSTR lpNotifyMessage, LPCWSTR lpLocalUrl, UINT nReason);


	void setLogFile(CAtlString strFile);
	void writeLog(CAtlString strMsg, CAtlString strUrl,int uReason);
	void setCurUrl(CAtlString strUrl);

private:
	CAtlString m_strLogFile;
	int		m_iRandom;
	CAtlString m_strCurUrl;	//当前浏览的URL
	bool	m_bFirst;	//对于m_strCurUrl是否第一次发现错误.
};

#endif